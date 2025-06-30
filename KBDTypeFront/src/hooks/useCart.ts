import { useEffect } from "react";
import { useSelector, useDispatch } from "react-redux";
import type { RootState } from "../store/store";
import { addToCart, removeFromCart, changeQuantity, clearCart, type CartItem } from "../store/cartSlice";

export function useCart() {
  const items = useSelector((state: RootState) => state.cart.items);
  const dispatch = useDispatch();

  // Зберігати кошик у localStorage при кожній зміні
  useEffect(() => {
    localStorage.setItem("cart", JSON.stringify(items));
  }, [items]);

  return {
    items,
    addToCart: (item: CartItem) => dispatch(addToCart(item)),
    removeFromCart: (productId: number) => dispatch(removeFromCart(productId)),
    changeQuantity: (productId: number, delta: number) => dispatch(changeQuantity({ productId, delta })),
    clearCart: () => dispatch(clearCart()),
    isInCart: (productId: number) => items.some(i => i.productId === productId),
    totalPrice: items.reduce((sum, item) => sum + item.price * item.quantity, 0),
    totalCount: items.reduce((sum, item) => sum + item.quantity, 0),
  };
}