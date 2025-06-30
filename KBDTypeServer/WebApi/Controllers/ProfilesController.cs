using Microsoft.AspNetCore.Mvc;
using KBDTypeServer.Application;
using Microsoft.AspNetCore.Identity;
using KBDTypeServer.Application.Services.UserServices;
using KBDTypeServer.Domain.Entities.UserEntity;
using KBDTypeServer.Application.DTOs.UserDtos;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KBDTypeServer.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilesController : BaseController
    {
        private readonly IUserProfileService _userProfileService;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        public ProfilesController(IUserProfileService userProfileService, UserManager<User> userManager, IMapper mapper)
        {
            _userProfileService = userProfileService ?? throw new ArgumentNullException(nameof(userProfileService));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            var userId = GetСurrentUserId();
            if (userId == null)
                return Unauthorized("User is not authenticated.");
            var userProfile = await _userManager.FindByIdAsync(userId.Value.ToString());
            if (userProfile == null)
                return NotFound("User profile not found.");
            var result = _mapper.Map<UserProfileDto>(userProfile);
            return Ok(result);
        }
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] UserProfileDto userProfileDto)
        {
            if (userProfileDto == null)
                return BadRequest("User profile data cannot be null.");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var userId = GetСurrentUserId();
            if (userId == null)
                return Unauthorized("User is not authenticated.");
            var updatedProfile = await _userProfileService.UpdateUserAsync(userProfileDto, userId.Value);
            if (updatedProfile == null)
                return NotFound("User profile not found.");
            return Ok(updatedProfile);
        }

    }
}
