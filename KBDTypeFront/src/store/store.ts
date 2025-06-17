// This file configures the Redux store for the application.
// також імплементує типи для RootState та AppDispatch,
// які використовуються для типізації селекторів та диспатчів в додатку.
import { configureStore } from "@reduxjs/toolkit";
import authReducer from "./authSlice";
import profileReducer from "./profileSlice";
import addressesReducer from "./addressesSlice";
import cartReducer from "./cartSlice";
import wishlistReducer from "./wishlistSlice";
// Цей файл налаштовує Redux store для додатку.
export const store = configureStore({
  reducer: {
    auth: authReducer,
    profile: profileReducer,
    addresses: addressesReducer,
    cart: cartReducer, // додай цей рядок
    wishlist: wishlistReducer,
  },
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;