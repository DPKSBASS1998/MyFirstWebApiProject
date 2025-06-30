// dto/RegistrationDto.ts
export class RegistrationDto {
  firstName: string = "";      // Ім'я (обов'язково, не менше 1 символу)
  lastName: string = "";       // Прізвище (обов'язково, не менше 1 символу)
  phoneNumber: string = "";    // Телефон (обов'язково, формат ^380\d{9}$)
  email?: string | null = null;  // Email (необов'язково)
  password: string = "";       // Пароль (обов'язково, не менше 1 символу)

  constructor(init?: Partial<RegistrationDto>) {
    Object.assign(this, init);
  }
}