// src/components/ProductCard.jsx
import "../styles/ProductCard.css"; 
import type { Switches } from "../models/Switches";
import { useCart } from "../hooks/useCart";
import { useWishlist } from "../hooks/useWishlist";

type ProductCardProps = {
  product: Switches;
};

export default function ProductCard({ product }: ProductCardProps) {
  const { isInCart, addToCart, removeFromCart } = useCart();
  const { isInWishlist, addToWishlist, removeFromWishlist } = useWishlist();
  const inCart = isInCart(product.id);
  const inWishlist = isInWishlist(product.id);

  return (
    <div className={`product-card ${!product.inStock ? "out-of-stock" : ""}`}>
      <img src={product.imagePath} alt={product.name} className="product-image" />

      <div className="product-basic">
        <h3 className="product-name">{product.name}</h3>
        <p className="product-price">
          {product.price.toFixed(2)} ₴
          <span className="unit-label">/ 10 шт</span>
        </p>
      </div>

      <div className="product-buttons">
        {product.inStock && (
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
              : addToWishlist({ productId: product.id, name: product.name, price: product.price, imagePath: product.imagePath })
          }
          aria-label={inWishlist ? "Видалити з бажаного" : "Додати в бажане"}
        >
          <i className={`bi ${inWishlist ? "bi-heart-fill" : "bi-bag-heart"}`} />
        </button>
      </div>

      <div className="product-extra">
        <p className="product-details">Тип: {product.type}</p>
        <p className="product-details">Сила натискання: {product.operatingForce} gf</p>
        <p className="product-details">Повний хід: {product.totalTravel.toFixed(1)} мм</p>
        <p className="product-details">Хід до спрацьовування: {product.preTravel.toFixed(1)} мм</p>
        <p className="product-details">Тактильний хід: {product.tactilePosition.toFixed(1)} мм</p>
        <p className="product-details">Тактильна сила: {product.tactileForce} gf</p>
        {!product.inStock && <h3 className="product-details">Немає в наявності</h3>}
      </div>
    </div>
  );
}
