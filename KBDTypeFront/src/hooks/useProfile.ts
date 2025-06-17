import { useSelector, useDispatch } from "react-redux";
import type { RootState, AppDispatch } from "../store/store";
import { fetchProfile, updateProfileThunk } from "../store/profileSlice";
import { logout } from "../store/authSlice"; // <-- Ось тут правильний імпорт
import { useIsAuthenticated, useLoading as useAuthLoading } from "./useAuth";

// Хук для отримання профілю користувача (замість контексту)
export function useUserProfile() {
  const isAuthenticated = useIsAuthenticated();
  const loading = useAuthLoading();
  const dispatch = useDispatch<AppDispatch>();

  return {
    isAuthenticated,
    loading,
    logout: () => dispatch(logout()),
  };
}

export function useProfileData() {
  return useSelector((state: RootState) => state.profile.data);
}

export function useProfileLoading() {
  return useSelector((state: RootState) => state.profile.loading);
}

export function useFetchProfile() {
  const dispatch = useDispatch<AppDispatch>();
  return () => dispatch(fetchProfile());
}

export function useUpdateProfile() {
  const dispatch = useDispatch<AppDispatch>();
  return (data: any) => dispatch(updateProfileThunk(data));
}
