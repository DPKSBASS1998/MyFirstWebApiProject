import { useCallback, useState } from "react";
import { createOrder, getOrder} from "../api/Order";
import { OrderCreateDto } from "../dto/order/OrderCreateDto";
import type { OrderShowDto } from "../dto/order/OrderShowDto";

// 1. Створюємо інтерфейс для того, що повертає хук
interface UseOrderHook {
  loading: boolean;
  error: string | null;
  success: boolean;
  sendOrder: (orderDto: OrderCreateDto) => Promise<void>;
  getAllOrders: () => Promise<OrderShowDto[]>; // Повертаємо масив замовлень
}

// 2. Застосовуємо тип до функції хука
export function useOrder(): UseOrderHook {
  // Створює змінну стану щоб знати чи триває запит на сервер
  const [loading, setLoading] = useState(false);
  // Створює змінну стану для зберігання помилки, якщо вона виникне
  const [error, setError] = useState<string | null>(null);
  // Створює змінну стану для зберігання успішного результату
  const [success, setSuccess] = useState<boolean>(false);

  // Функція для відправки замовлення на сервер
  const sendOrder = useCallback(async (orderDto: OrderCreateDto): Promise<void> => {
    setLoading(true); // Встановлюємо стан завантаження
    setError(null); // Скидаємо попередню помилку
    setSuccess(false); // Скидаємо попередній успіх
    try{
      // Викликаємо API для створення замовлення
      await createOrder(orderDto);
      // Якщо запит успішний, встановлюємо стан успіху
      setSuccess(true);
   } catch (e: any) {
      setError(e.message || "Сталася помилка"); // Записуємо помилку
    } finally {
      setLoading(false); // Завершуємо завантаження
    }
  },[]);

  // 3. Виправляємо логіку та типізацію getAllOrders
  const getAllOrders = useCallback(async (): Promise<OrderShowDto[]> => {
    setLoading(true); // Встановлюємо стан завантаження
    setError(null); // Скидаємо попередню помилку
    try {
      // Викликаємо API для отримання всіх замовлень
      const orders = await getOrder();
      if (!Array.isArray(orders)) {
        throw new Error("Невірний формат відповіді сервера");
      }
      // Повертаємо отримані замовлення
      return orders;
    } catch (e: any) {
      setError(e.message || "Сталася помилка при отриманні замовлень");
      return []; // В разі помилки повертаємо порожній масив
    } finally {
      setLoading(false); // Завершуємо завантаження
    }
},[]);
  //повертаємо об'єкт хука зі змінними
    return {  loading, error, success, sendOrder, getAllOrders};
}

