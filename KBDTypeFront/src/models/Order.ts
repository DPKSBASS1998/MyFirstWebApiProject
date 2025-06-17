import type { Address } from "./Address";
import type { OrderItem } from "./OrderItem";

export interface Order {
  id: number;
  userId: string;
  addressId: number;
  address: Address;
  createdAt: string;
  items: OrderItem[];
}