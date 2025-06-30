import { useSelector, useDispatch } from "react-redux";
import type { RootState } from "../store/store";
import { addToWishlist, removeFromWishlist, clearWishlist, type WishlistItem } from "../store/wishlistSlice";
import { useEffect } from "react";

export function useWishlist() {
  const items = useSelector((state: RootState) => state.wishlist.items);
  const dispatch = useDispatch();

  // Синхронізація з localStorage
  useEffect(() => {
    localStorage.setItem("wishlist", JSON.stringify(items));
  }, [items]);

  return {
    items,
    addToWishlist: (item: WishlistItem) => dispatch(addToWishlist(item)),
    removeFromWishlist: (productId: number) => dispatch(removeFromWishlist(productId)),
    clearWishlist: () => dispatch(clearWishlist()),
    isInWishlist: (productId: number) => items.some(i => i.productId === productId),
  };
}