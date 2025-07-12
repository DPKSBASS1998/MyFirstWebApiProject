// src/components/ProductCard.jsx
import "../styles/ProductCard.css";
import { useCart } from "../hooks/useCart";
import { useWishlist } from "../hooks/useWishlist";

// Оновлений тип продукту
type Product = {
  id: number;
  name: string;
  productType: number;
  description: string;
  imageUrl: string;
  price: number;
  stockQuantity: number;
  manufacturer: string;
};

type ProductCardProps = {
  product: Product;
};

export default function ProductCard({ product }: ProductCardProps) {
  const { isInCart, addToCart, removeFromCart } = useCart();
  const { isInWishlist, addToWishlist, removeFromWishlist } = useWishlist();
  const inCart = isInCart(product.id);
  const inWishlist = isInWishlist(product.id);

  return (
    <div className={`product-card ${product.stockQuantity === 0 ? "out-of-stock" : ""}`}>
      <img
        src={product.imageUrl ?? "/no-image.png"}
        alt={product.name}
        className="product-image"
      />

      <div className="product-basic">
        <h3 className="product-name">{product.name}</h3>
        <p className="product-price">
          {typeof product.price === "number" ? product.price.toFixed(2) : "—"} ₴
          <span className="unit-label">/ 10 шт</span>
        </p>
      </div>

      <div className="product-buttons">
        {product.stockQuantity > 0 && (
          <button
            className="btn"
            onClick={() =>
              inCart
                ? removeFromCart(product.id)
                : addToCart({ ...product, productId: product.id, quantity: 1 })
            }
            aria-label={inCart ? "Видалити з кошика" : "Додати в кошик"}
          >
            <i className={`bi ${inCart ? "bi-cart-check-fill" : "bi-cart"}`} />
          </button>
        )}

        <button
          className="btn"
          onClick={() =>
            inWishlist
              ? removeFromWishlist(product.id)
              : addToWishlist({
                  productId: product.id,
                  name: product.name,
                  price: product.price,
                  imagePath: product.imageUrl ?? "/no-image.png",
                })
          }
          aria-label={inWishlist ? "Видалити з бажаного" : "Додати в бажане"}
        >
          <i className={`bi ${inWishlist ? "bi-heart-fill" : "bi-bag-heart"}`} />
        </button>
      </div>

      <div className="product-extra">
        <p className="product-details">Тип: {product.productType}</p>
        <p className="product-details">Виробник: {product.manufacturer}</p>
        <p className="product-details">Опис: {product.description}</p>
        <p className="product-details">В наявності: {product.stockQuantity} шт</p>
        {product.stockQuantity === 0 && (
          <h3 className="product-details">Немає в наявності</h3>
        )}
      </div>
    </div>
  );
}
