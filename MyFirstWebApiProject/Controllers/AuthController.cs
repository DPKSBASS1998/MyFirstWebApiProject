using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyFirstWebApiProject.Models.Users;
using System.Threading.Tasks;

namespace MyFirstWebApiProject
{
    [Route("Account")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        // Вставимо залежності через конструктор
        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpGet("me")]
        public IActionResult GetCurrentUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Ok(new
                {
                    isAuthenticated = true,
                    username = User.Identity.Name
                });
            }
            return Ok(new { isAuthenticated = false });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            Console.WriteLine($"Отримано запит на логін: {model.Email}");

            if (!ModelState.IsValid)
            {
                Console.WriteLine(" ModelState НЕ валідний!");
                return BadRequest(new { message = "Невірні дані для входу" });
            }

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                Console.WriteLine(" Користувача не знайдено!");
                return Unauthorized(new { message = "Невірні дані для входу" });
            }
            else
            {
                Console.WriteLine($" Користувач знайдений: {user.Email}");
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

            if (!result.Succeeded)
            {
                Console.WriteLine(" Пароль невірний!");
                return Unauthorized(new { message = "Невірні дані для входу" });
            }

            Console.WriteLine(" Логін успішний!");
            return Ok(new { message = "Успішний вхід", username = user.UserName });
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Некоректні дані для реєстрації" });
            }

            try
            {
                var user = new User
                {
                    UserName = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (!result.Succeeded)
                {
                    return BadRequest(new { errors = result.Errors.Select(e => e.Description) });
                }

                string assignedRole = "User";

                if (User.Identity.IsAuthenticated && User.IsInRole("Manager"))
                {
                    Console.WriteLine($"Менеджер {User.Identity.Name} створює нового менеджера: {model.Email}");
                    assignedRole = "Manager";
                }

                // Додаємо роль користувачеві
                var roleResult = await _userManager.AddToRoleAsync(user, assignedRole);
                if (!roleResult.Succeeded)
                {
                    return BadRequest(new { message = $"Не вдалося призначити роль '{assignedRole}'" });
                }

                Console.WriteLine($"Роль '{assignedRole}' успішно призначено для {model.Email}");

                // Якщо це звичайний користувач, автоматично входимо в систему
                if (assignedRole == "User")
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                }

                return Ok(new { message = $"Користувач '{model.Email}' успішно зареєстрований", role = assignedRole });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Виникла помилка: {ex.Message}");
                return StatusCode(500, new { message = "Сталася внутрішня помилка сервера" });
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            // Вихід з системи
            await _signInManager.SignOutAsync();

            return Ok(new { message = "Успішно вийшли з системи" });
        }

    }
}
