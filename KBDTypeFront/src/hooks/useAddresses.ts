import { useSelector, useDispatch } from "react-redux";
import type { RootState } from "../store/store";
import type { AppDispatch } from "../store/store";
import {
  fetchAddresses,
  addAddressThunk,
  updateAddressThunk,
  removeAddressThunk,
  selectAddress,
} from "../store/addressesSlice";

// Хук для отримання списку адрес
export function useAddresses() {
  return useSelector((state: RootState) => state.addresses.list);
}

export function useFetchAddresses() {
  const dispatch: AppDispatch = useDispatch();
  return () => dispatch(fetchAddresses());
}


// Хук для додавання адреси
export function useAddAddress() {
  const dispatch = useDispatch<AppDispatch>();
  return (address: any) => dispatch(addAddressThunk(address));
}

// Хук для оновлення адреси
export function useUpdateAddress() {
  const dispatch = useDispatch<AppDispatch>();
  return (address: any) => dispatch(updateAddressThunk(address));
}

// Хук для видалення адреси
export function useRemoveAddress() {
  const dispatch = useDispatch<AppDispatch>();
  return (id: number) => dispatch(removeAddressThunk(id));
}

// Хук для вибору адреси
export function useSelectAddress() {
  const dispatch = useDispatch<AppDispatch>();
  return (id: number) => dispatch(selectAddress(id));
}

// Хук для отримання id вибраної адреси
export function useSelectedAddressId() {
  return useSelector((state: RootState) => state.addresses.selectedId);
}

// Хук для статусу завантаження адрес
export function useAddressesLoading() {
  return useSelector((state: RootState) => state.addresses.loading);
}