// dto/ProfileDto.ts
import type { AddressDto } from "../address/AddressDto";

export interface ProfileDto {
  firstName: string;
  lastName: string;
  phoneNumber: string;
  email: string;
  addresses: AddressDto[];
  selectedAddressId?: number;
}