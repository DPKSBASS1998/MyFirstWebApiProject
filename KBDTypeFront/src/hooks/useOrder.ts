import { useCallback, useState } from "react";
import { createOrder, getOrder, updateOrder, deleteOrder } from "../api/Order";
import type { CreateOrderDto } from "../dto/order/OrderCreateDto";

export function useCreateOrder() {
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const create = useCallback(async (orderData: CreateOrderDto) => {
    setLoading(true);
    setError(null);
    try {
      const result = await createOrder(orderData);
      return result;
    } catch (e: any) {
      setError(e?.message || "Помилка створення замовлення");
      throw e;
    } finally {
      setLoading(false);
    }
  }, []);

  return { create, loading, error };
}

export function useGetOrder() {
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const get = useCallback(async (orderId: number) => {
    setLoading(true);
    setError(null);
    try {
      const result = await getOrder(orderId);
      return result;
    } catch (e: any) {
      setError(e?.message || "Помилка отримання замовлення");
      throw e;
    } finally {
      setLoading(false);
    }
  }, []);

  return { get, loading, error };
}

export function useUpdateOrder() {
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const update = useCallback(async (orderId: number, orderData: any) => {
    setLoading(true);
    setError(null);
    try {
      const result = await updateOrder(orderId, orderData);
      return result;
    } catch (e: any) {
      setError(e?.message || "Помилка оновлення замовлення");
      throw e;
    } finally {
      setLoading(false);
    }
  }, []);

  return { update, loading, error };
}

export function useDeleteOrder() {
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const remove = useCallback(async (orderId: number) => {
    setLoading(true);
    setError(null);
    try {
      const result = await deleteOrder(orderId);
      return result;
    } catch (e: any) {
      setError(e?.message || "Помилка видалення замовлення");
      throw e;
    } finally {
      setLoading(false);
    }
  }, []);

  return { remove, loading, error };
}