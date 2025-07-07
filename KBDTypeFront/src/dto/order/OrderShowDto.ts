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
        price: number;
        productName: string | null; // Optional, can be null if product not found
        imageUrl: string; // URL to the product image
    }[] = [];
    paymentId?: number;
    trackingNumber?: string;
    status: number = 0;
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
