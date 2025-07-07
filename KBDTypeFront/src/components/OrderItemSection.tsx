// src/components/OrderItemSection.tsx
import { useCart } from "../hooks/useCart";
import { useNavigate } from "react-router-dom";
import "../styles/CartModal.css";

export default function OrderItemSection() {
  const navigate = useNavigate();
  const { items, removeFromCart, changeQuantity, totalPrice } = useCart();

  return (
    <div className="order-items-container"> 
        <h3>Товари в замовленні</h3>
        {items.length === 0 ? (
          <p className="empty-cart">Кошик порожній</p>
          ) : (
          <>
            <div className="cart-items">
              {items.map((item) => (
                <div className="cart-item" key={item.productId}>
                  <img src={item.imageUrl} alt={item.name} />
                  <div className="item-info">
                    <h4>{item.name}</h4>
                    <p>{item.price.toFixed(2)} ₴</p>
                  </div>
                  <div className="cart-actions">
                    <div className="quantity-control">
                      <p>
                        {(item.price * item.quantity).toFixed(2)} ₴
                      </p>
                      <button
                        onClick={() => changeQuantity(item.productId, -1)}
                        aria-label="Зменшити кількість"
                      >
                        −
                      </button>
                      <span>{item.quantity}</span>
                      <button
                        onClick={() => changeQuantity(item.productId, +1)}
                        aria-label="Збільшити кількість"
                      >
                        +
                      </button>
                    </div>
                    <button
                      className="remove-btn"
                      onClick={() => removeFromCart(item.productId)}
                      aria-label="Видалити товар"
                    >
                      <i className="bi bi-x-lg"></i>
                    </button>
                  </div>
                </div>
              ))}
            </div>

            <div className="cart-footer">
              <p className="total">Разом: {totalPrice.toFixed(2)} ₴</p>
            </div>
            </>

        )}
    </div>
    );
  }
