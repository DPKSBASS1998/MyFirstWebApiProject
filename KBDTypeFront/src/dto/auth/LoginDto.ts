// dto/LoginDto.ts
export class LoginDto {
  identifier: string = "";
  password: string = "";
  rememberMe?: boolean = false;

  constructor(init?: Partial<LoginDto>) {
    Object.assign(this, init);
  }
}