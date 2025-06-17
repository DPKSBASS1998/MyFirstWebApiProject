import type { Product } from "./Product";

export interface CartItem {
  id: number;
  userId: string;
  productId: number;
  product: Product;
  quantity: number;
}