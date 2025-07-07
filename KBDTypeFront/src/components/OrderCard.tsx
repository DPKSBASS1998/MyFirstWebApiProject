import type { OrderShowDto } from "../dto/order/OrderShowDto";

// Оновлена функція для перетворення статусу з числа в текст
const getStatusText = (status: number) => {
  switch (status) {
    case 0: return "Створено";
    case 1: return "Очікує на оплату";
    case 2: return "Оплачено";
    case 3: return "Очікує на відправку";
    case 4: return "Відправлено";
    case 5: return "Доставлено";
    case 6: return "Очікує повернення";
    case 7: return "Повернено";
    case 8: return "Скасовано";
    case 9: return "Успішно";
    case 10: return "Помилка";
    default: return "Невідомий статус";
  }
};

interface OrderCardProps {
  order: OrderShowDto;
}

export default function OrderCard({ order }: OrderCardProps) {
  const handleCancelOrder = () => {
    alert(`Ви намагаєтесь скасувати замовлення №${order.id}`);
  };

  async function proceedToCheckout(orderId: number) {
    try {
        const response = await fetch('/api/payment/create-checkout-session', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({ orderId: orderId }), // Передаємо ID замовлення
        });

        if (!response.ok) {
            // Обробка помилок, якщо сервер повернув не 2xx статус
            console.error('Failed to create checkout session');
            return;
        }

        const session = await response.json();

        // Перенаправляємо користувача на сторінку оплати Stripe
        window.location.href = session.url;

    } catch (error) {
        console.error('Error:', error);
    }
  }
  
  // Нова функція для обробки натискання кнопки "Сплатити"
  const handlePayOrder = () => {
    // Викликаємо функцію proceedToCheckout, передаючи ID поточного замовлення
    proceedToCheckout(order.id);
  };

  return (
    <div className="order-card">
      <div className="order-card-header">
      <div className="order-metadata">
        <h2>ID: {order.id}</h2>
        <span>від {new Date(order.createdAt).toLocaleDateString()}</span>
      </div>
        <div className="order-status">
          <span>Статус: {getStatusText(order.status)}</span>
        </div>
      </div>

      <div className="order-card-body">
        <div className="order-section recipient-info">
          <h4>Отримувач та адреса доставки</h4>
          {/* Цей div тепер буде grid-контейнером */}
          <div className="recipient-details">
            {/* Рядок 1: Ім'я */}
            <span>Ім'я:</span>
            <span>{order.firstName} {order.lastName}</span>

            {/* Рядок 2: Телефон */}
            <span>Телефон:</span>
            <span>{order.phoneNumber}</span>

            {/* Рядок 3: Адреса */}
            <span>Адреса:</span>
            <span>
              {order.region}<br />
              м. {order.city}<br />
              вул. {order.street}<br />
              буд. {order.building}{order.apartment ? `, кв. ${order.apartment}` : ''}
            </span>
            {/* Рядок 4: Індекс */}
            <span>Індекс:</span>
            <span>{order.postalCode}</span>

            {/* Рядок 5: Коментар (умовний) */}
            {order.comment && (
              <>
                <span>Коментар:</span>
                <span>{order.comment}</span>
              </>
            )}
          </div>
        </div>

        <div className="order-section items-info">
          <h4>Товари в замовленні</h4>
          <div className="items-list">
            {order.items.map(item => (
              <div key={item.productId} className="order-item">
                <div className="order-item-image">
                  <img src={item.imageUrl} />
                </div>
                <div className="order-item-details">
                  <span className="item-id">ID: {item.productId}</span>
                  <span className="item-name">{item.productName || "Назва товару"}</span>
                  <span className="item-quantity">{item.quantity} x {item.price.toFixed(2)} грн</span>
                </div>
              </div>
            ))}
          </div>
        </div>
      </div>

      <div className="order-card-footer">
        <div className="order-total">
          <strong>Разом до сплати: {order.totalPrice.toFixed(2)} грн</strong>
        </div>
        <div className="order-actions">
          {/* Кнопка "Сплатити" з'являється, якщо статус "Очікує на оплату" (1) */}
          {order.status === 1 && (
            <button onClick={handlePayOrder} className="pay-button">
              <span>Сплатити </span>
              <i className="bi bi-credit-card"></i>
            </button> 
            
          )}

          {/* Кнопка "Скасувати замовлення" */}
          {(order.status === 0 || order.status === 1) && (
            <button onClick={handleCancelOrder} className="cancel-button">
              Скасувати замовлення
            </button>
          )}
        </div>
      </div>
    </div>
  );
}