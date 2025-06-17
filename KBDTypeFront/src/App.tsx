// src/App.tsx
import { createBrowserRouter, RouterProvider } from "react-router-dom";
import { useEffect } from "react";
import { useDispatch } from "react-redux";
import type { AppDispatch } from "./store/store";
import MainLayout from "./layouts/MainLayout";
import Home from "./pages/Home";
import MyAccount from "./pages/MyAccount";
import Catalog from "./pages/Catalog";
import { fetchMe } from "./store/authSlice";
import Checkout from "./pages/Checkout";

const router = createBrowserRouter([
  {
    path: "/",
    element: <MainLayout />,
    children: [
      { index: true, element: <Home /> },
      { path: "my-account", element: <MyAccount /> },
      { path: "catalog", element: <Catalog /> },
      { path: "checkout", element: <Checkout /> },
    ],
  },
]);

export default function App() {
  const dispatch = useDispatch<AppDispatch>();

  useEffect(() => {
    dispatch(fetchMe());
  }, [dispatch]);

  return <RouterProvider router={router} />;
}
