// src/components/CartModal.jsx
import { useCart } from "../hooks/useCart";
import { useNavigate } from "react-router-dom";
import "../styles/CartModal.css";

export default function CartModal({ onClose }: { onClose: () => void }) {
  const navigate = useNavigate();
  const { items, removeFromCart, changeQuantity, totalPrice } = useCart();

  return (
    <div className="modal-overlay" onClick={onClose}>
      <div className="cart-modal" onClick={(e) => e.stopPropagation()}>
        <button className="close-btn" onClick={onClose}>
          <i className="bi bi-x-lg"></i>
        </button>
        <h2>Ваш кошик</h2>

        {items.length === 0 ? (
          <p className="empty-cart">Кошик порожній</p>
        ) : (
          <>
            <div className="cart-items">
              {items.map((item) => (
                <div className="cart-item" key={item.productId}>
                  <img src={item.imageUrl} />
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
              <button
                className="checkout-btn"
                onClick={() => {
                  onClose();
                  setTimeout(() => navigate("/checkout"), 0); // Дати React закрити модалку
                }}
              >
                Оформити замовлення
              </button>
            </div>
          </>
        )}
      </div>
    </div>
  );
}
