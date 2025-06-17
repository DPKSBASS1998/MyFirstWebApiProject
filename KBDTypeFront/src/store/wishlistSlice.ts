import { createSlice, type PayloadAction } from "@reduxjs/toolkit";

export type WishlistItem = {
  productId: number;
  name: string;
  price: number;
  imagePath: string;
};

type WishlistState = {
  items: WishlistItem[];
};

const initialState: WishlistState = {
  items: JSON.parse(localStorage.getItem("wishlist") || "[]"),
};

const wishlistSlice = createSlice({
  name: "wishlist",
  initialState,
  reducers: {
    addToWishlist(state, action: PayloadAction<WishlistItem>) {
      if (!state.items.find(i => i.productId === action.payload.productId)) {
        state.items.push(action.payload);
      }
    },
    removeFromWishlist(state, action: PayloadAction<number>) {
      state.items = state.items.filter(i => i.productId !== action.payload);
    },
    clearWishlist(state) {
      state.items = [];
    },
  },
});

export const { addToWishlist, removeFromWishlist, clearWishlist } = wishlistSlice.actions;
export default wishlistSlice.reducer;