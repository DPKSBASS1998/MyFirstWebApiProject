// ===== Імпорти =====
import { useState, useEffect } from "react";
import ProfileForm from "../components/ProfileForm";
import AddressSection from "../components/AddressSection";
import OrderItemSection from "../components/OrderItemSection";
import "../styles/AccountShared.css";
import { OrderCreateDto } from "../dto/order/OrderCreateDto";
import { useProfileData } from "../hooks/useProfile";
import { useAddresses, useSelectedAddressId } from "../hooks/useAddresses";
import { useCart } from "../hooks/useCart";
import { useOrder } from "../hooks/useOrder";

// ===== Головний компонент сторінки =====

export default function Checkout() {
  const profile = useProfileData();
  const addresses = useAddresses();
  const selectedAddressId = useSelectedAddressId();
  const { items, clearCart } = useCart();

  const [comment, setComment] = useState<string>("");

  const selectedAddress = addresses.find(a => a.id === selectedAddressId);

  // ––––– Функція обробки сабміту замовлення –––––
  const {sendOrder, loading, error, success} = useOrder();

  useEffect(() => {
    if (success) {
      clearCart();
    }
  }, [success, clearCart]); // Залежності для useEffect

  const handleSubmit = () => {
    if (!profile || !selectedAddress || items.length === 0) {
      alert("Будь ласка, заповніть всі поля перед підтвердженням замовлення.");
      return;
    }
    const order: OrderCreateDto = {
      firstName: profile.firstName,
      lastName: profile.lastName,
      email: profile.email ?? undefined,
      phoneNumber: profile.phoneNumber,
      region: selectedAddress.region,
      city: selectedAddress.city,
      street: selectedAddress.street,
      building: selectedAddress.building,
      apartment: selectedAddress.apartment ?? undefined,
      postalCode: selectedAddress.postalCode,
      comment: comment || undefined,
      items: items.map(i => ({
        productId: i.productId,
        quantity: i.quantity,
        price: i.price,
      })),
    };
    // Викликаємо функцію для відправки замовлення на сервер
    sendOrder(order);
  };

  if (success) {
    
    // Повертаємо повідомлення про успішне замовлення
    return (
      <div className="account-page">
        <h1>Замовлення успішно створено!</h1>
        <p>Дякуємо. Наш менеджер зв'яжеться з вами найближчим часом.</p>
      </div>
    );
  }

  return (
    <div className="account-page">
      <h1>Оформлення замовлення</h1>
      <ProfileForm />
      <AddressSection />
      <OrderItemSection />

      <div className="order-comment-section">
        <label htmlFor="order-comment">Коментар до замовлення:</label>
        <textarea
          id="order-comment"
          value={comment}
          onChange={e => setComment(e.target.value)}
          rows={3}
          style={{ width: "100%" }}
        />
      </div>

      <button onClick={handleSubmit} disabled={loading}>
        {loading ? "Обробка..." : "Підтвердити замовлення"}
      </button>

      {error && <p style={{ color: 'red', marginTop: '10px' }}>Помилка: {error}</p>}
    </div>
  );
}
