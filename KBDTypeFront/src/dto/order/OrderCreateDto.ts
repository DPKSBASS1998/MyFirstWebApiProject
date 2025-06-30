export class OrderCreateDto {
    firstName: string = '';
    lastName: string = '';
    email?: string;
    phoneNumber: string = '';
    region: string = '';
    city: string = '';
    street: string = '';
    house: string = '';
    apartment?: string;
    postalCode: string = '';
    comment?: string;
    items: {
        productId: number;
        quantity: number;
    }[] = [];

    constructor(init?: Partial<OrderCreateDto>) {
        Object.assign(this, init);
    }
}