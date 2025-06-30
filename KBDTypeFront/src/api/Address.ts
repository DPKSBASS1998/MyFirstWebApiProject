import axios from "axios";
import type { AddressDto } from "../dto/address/AddressDto";

const API = "/api/Addresses";

// Отримати всі адреси користувача
export async function getAddresses(): Promise<AddressDto[]> {
  const response = await axios.get<AddressDto[]>(API);
  if (!Array.isArray(response.data)) {
    throw new Error("Expected an array of addresses");
  }
  return response.data;
}

// Додати нову адресу
export async function addAddress(address: AddressDto): Promise<AddressDto> {
  const response = await axios.post<AddressDto>(API, address);
  if (!response.data ) {
    throw new Error("Failed to add address");
  }
  return response.data;
}

// Отримати адресу за id
export async function getAddress(id: number): Promise<AddressDto> {
  const response = await axios.get<AddressDto>(`${API}/${id}`);
  return response.data;
}

// Оновити адресу
export async function updateAddress(address: AddressDto): Promise<AddressDto> {
  const response = await axios.put<AddressDto>(`${API}/${address.id}`, address);
  if (!response.data) {
    throw new Error("Failed to update address");
  }
  return response.data;
}

// Видалити адресу
export async function removeAddress(id: number): Promise<boolean> {
  const response = await axios.delete<boolean>(`${API}/${id}`);
  if (!response.data) {
    throw new Error("Failed to delete address");
  }
  return response.data;
}