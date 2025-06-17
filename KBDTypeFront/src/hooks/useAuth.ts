
import { useSelector, useDispatch } from "react-redux";
import type { RootState, AppDispatch } from "../store/store";
import { login, register, logout } from "../store/authSlice";

export function useIsAuthenticated() {
  return useSelector((state: RootState) => state.auth.isAuthenticated);
}

export function useUser() {
  return useSelector((state: RootState) => state.auth.user);
}

export function useLogin() {
  const dispatch = useDispatch<AppDispatch>();
  return (data: any) => dispatch(login(data));
}

export function useRegister() {
  const dispatch = useDispatch<AppDispatch>();
  return (data: any) => dispatch(register(data));
}

export function useLogout() {
  const dispatch = useDispatch<AppDispatch>();
  return () => dispatch(logout());
}

export function useLoading() {
  return useSelector((state: RootState) => state.auth.loading);
}
