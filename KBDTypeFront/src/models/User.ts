import type { Address } from './Address';
import type { CartItem } from './CartItem';
import type { WishlistItem } from './WishlistItem';
import type { Order } from './Order.ts';
export interface User {
  id: string;
  email: string;
  userName: string;
  firstName: string;
  lastName: string;
  addresses: Address[];
  cartItems: CartItem[];
  wishlistItems: WishlistItem[];
  orders: Order[];
}