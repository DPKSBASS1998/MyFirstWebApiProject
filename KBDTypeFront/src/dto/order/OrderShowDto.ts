export class OrderShowDto {
    userId: number = 0;
    id: number = 0;    
    firstName: string = '';
    lastName: string = '';
    email?: string;
    phoneNumber: string = '';
    region: string = '';
    city: string = '';
    street: string = '';
    building: string = '';
    apartment?: string;
    postalCode: string = '';
    comment?: string;
    items: {
        productId: number;
        quantity: number;
    }[] = [];
    paymentId?: number;
    trackingNumber?: string;
    status: string = '';
    subtotal: number = 0;
    totalPrice: number = 0;
    createdAt: Date = new Date();
    paidAt?: Date;
    shippedAt?: Date;
    deliveredAt?: Date;
    cancelledAt?: Date;
    refundedAt?: Date;
    
    constructor(init?: Partial<OrderShowDto>) {
        Object.assign(this, init);
    }
}
