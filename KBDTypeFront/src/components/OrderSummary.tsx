// src/components/Checkout/OrderSummary.jsx
import { useCart } from "../hooks/useCart";
import type { CartItem } from "../store/cartSlice";

/**
 * Показує список товарів із кошика та загальну суму.
 */
export default function OrderSummary() {
  const { items: cartItems } = useCart();
  const totalPrice = cartItems.reduce(
    (sum: number, item: CartItem) => sum + item.price * (item.quantity || 1),
    0
  );

  return (
    <section className="order-summary">
      <h2>Ваше замовлення</h2>
      {cartItems.map((item) => (
        <div key={item.productId} className="checkout-item">
          <span>
            {item.name} × {item.quantity}
          </span>
          <span>{(item.price * item.quantity).toFixed(2)} ₴</span>
        </div>
      ))}
      <div className="checkout-total">Разом: {totalPrice.toFixed(2)} ₴</div>
    </section>
  );
}
