import type { Product } from "./Product";

export interface WishlistItem {
  id: number;
  userId: string;
  productId: number;
  product: Product;
}