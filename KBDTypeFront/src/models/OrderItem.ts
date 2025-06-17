import type { Product } from "./Product";

export interface OrderItem {
  id: number;
  orderId: number;
  productId: number;
  product: Product;
  quantity: number;
  price: number; // На момент покупки
}