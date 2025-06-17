import axios from "axios";
import type { AddressDto } from "../dto/address/AddressDto";

const API = "/api/Address";

// Отримати всі адреси користувача
export async function getAddresses(): Promise<AddressDto[]> {
  const response = await axios.get<AddressDto[]>(`${API}/list`);
  return response.data;
}

// Додати нову адресу
export async function addAddress(address: AddressDto): Promise<void> {
  await axios.post(`${API}/save`, address);
}

// Оновити адресу
export async function updateAddress(address: AddressDto): Promise<void> {
  await axios.put(`${API}/update/${address.id}`, address);
}

// Видалити адресу
export async function removeAddress(id: number): Promise<void> {
  await axios.delete(`${API}/delete/${id}`);
}