export class AddressDto {
  id: number = 0;
  userId: number = 0;
  region: string = "";
  city: string = "";
  street: string = "";
  building: string = "";
  apartment?: string | null = null;
  postalCode: string = "";

  constructor(init?: Partial<AddressDto>) {
    Object.assign(this, init);
  }
}