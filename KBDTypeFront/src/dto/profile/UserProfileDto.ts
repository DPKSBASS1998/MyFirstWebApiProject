// dto/UserProfileDto.ts

export class UserProfileDto {
  firstName: string = "";
  lastName: string = "";
  phoneNumber: string = "";
  email?: string | null = null;
  gender?: string | null = null;
  dateOfBirth?: string | null = null;

  constructor(init?: Partial<UserProfileDto>) {
    Object.assign(this, init);
  }
}