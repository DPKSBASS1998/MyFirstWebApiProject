import { useState, useEffect } from "react";
import { useOrder } from "../hooks/useOrder";
import type { OrderShowDto } from "../dto/order/OrderShowDto";
import OrderCard from "../components/OrderCard"; // Імпортуємо новий компонент
import "../styles/Orders.css"; // Додамо стилі для сторінки

export default function Orders() {
  const { loading, error, getAllOrders } = useOrder();
  const [orders, setOrders] = useState<OrderShowDto[]>([]);

  useEffect(() => {
    const fetchOrders = async () => {
      const data = await getAllOrders();
      setOrders(data);
    };
    fetchOrders();
  }, [getAllOrders]); // Додаємо getAllOrders в залежності

  if (loading)
    return (
      <div className="orders-status">Завантаження замовлень...</div>
    );
  if (error)
    return (
      <div className="orders-status error">Помилка: {error}</div>
    );
  if (!orders.length)
    return (
      <div className="orders-status">У вас ще немає замовлень.</div>
    );

  return (
    <div className="orders-page">
      <h1>Мої замовлення</h1>
      <div className="orders-list">
        {[...orders]
          .reverse()
          .map((order) => <OrderCard key={order.id} order={order} />)}
      </div>
    </div>
  );
}