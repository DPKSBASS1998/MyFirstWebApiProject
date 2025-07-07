import { useEffect, useCallback } from "react";
import { useSelector, useDispatch } from "react-redux";
import type { RootState, AppDispatch } from "../store/store";
import { 
  addToCart as addToCartAction, 
  removeFromCart as removeFromCartAction, 
  changeQuantity as changeQuantityAction, 
  clearCart as clearCartAction, 
  type CartItem 
} from "../store/cartSlice";

export function useCart() {
  const items = useSelector((state: RootState) => state.cart.items);
  const dispatch = useDispatch<AppDispatch>();

  // Зберігати кошик у localStorage при кожній зміні
  useEffect(() => {
    localStorage.setItem("cart", JSON.stringify(items));
  }, [items]);

  const addToCart = useCallback((item: CartItem) => {
    dispatch(addToCartAction(item));
  }, [dispatch]);

  const removeFromCart = useCallback((productId: number) => {
    dispatch(removeFromCartAction(productId));
  }, [dispatch]);

  const changeQuantity = useCallback((productId: number, delta: number) => {
    dispatch(changeQuantityAction({ productId, delta }));
  }, [dispatch]);

  const clearCart = useCallback(() => {
    dispatch(clearCartAction());
  }, [dispatch]);

  const isInCart = useCallback((productId: number) => {
    return items.some(i => i.productId === productId);
  }, [items]); // Залежить від `items`, тому `items` у масиві залежностей

  const totalPrice = items.reduce((sum, item) => sum + item.price * item.quantity, 0);
  const totalCount = items.reduce((sum, item) => sum + item.quantity, 0);

  return {
    items,
    addToCart,
    removeFromCart,
    changeQuantity,
    clearCart,
    isInCart,
    totalPrice,
    totalCount,
  };
}