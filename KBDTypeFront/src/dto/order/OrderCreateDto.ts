// OrderCreateDto.ts
import type { OrderProfileDto } from "./OrderProfileDto";
import type { OrderItemDto } from "./OrderItemDto";

export interface CreateOrderDto {
  profile: OrderProfileDto;
  addressId: number;
  items: OrderItemDto[];
}