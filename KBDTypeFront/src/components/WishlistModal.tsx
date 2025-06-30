import { useWishlist } from "../hooks/useWishlist";
import { useCart } from "../hooks/useCart";
import { useNavigate } from "react-router-dom";
import "../styles/WishlistModal.css";

export default function WishlistModal({ onClose }: { onClose: () => void }) {
  const navigate = useNavigate();
  const { items, removeFromWishlist } = useWishlist();
  const { addToCart, removeFromCart, isInCart } = useCart();

  return (
    <div className="modal-overlay" onClick={onClose}>
      <div className="wishlist-modal" onClick={e => e.stopPropagation()}>
        <button className="close-btn" onClick={onClose}>
          <i className="bi bi-x-lg"></i>
        </button>
        <h2>Список бажаного</h2>

        {items.length === 0 ? (
          <p className="empty-cart">Список бажаного порожній</p>
        ) : (
          <>
            <div className="wishlist-items">
              {items.map(item => (
                <div className="wishlist-item" key={item.productId}>
                  <img src={item.imagePath} alt={item.name} />
                  <div className="item-info">
                    <h4>{item.name}</h4>
                    <p>{item.price.toFixed(2)} ₴</p>
                  </div>
                  <div className="wishlist-actions">
                    <button
                      className="add-to-cart-btn"
                      onClick={() =>
                        isInCart(item.productId)
                          ? removeFromCart(item.productId)
                          : addToCart({ ...item, quantity: 1 })
                      }
                      aria-label={
                        isInCart(item.productId)
                          ? "Видалити з кошика"
                          : "Додати в кошик"
                      }
                    >
                      <i
                        className={`bi ${
                          isInCart(item.productId)
                            ? "bi-cart-check-fill"
                            : "bi-cart"
                        } wishlist-cart-icon`}
                      />
                    </button>
                    <button
                      className="remove-btn"
                      onClick={() => removeFromWishlist(item.productId)}
                      aria-label="Видалити з бажаного"
                    >
                      <i className="bi bi-x-lg"></i>
                    </button>
                  </div>
                </div>
              ))}
            </div>
          </>
        )}
      </div>
    </div>
  );
}