import { useCallback, useState } from "react";
import { createOrder, getOrder} from "../api/Order";
import type { OrderCreateDto } from "../dto/order/OrderCreateDto";

export function useCreateOrder() {
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  

  return {  loading, error };
}