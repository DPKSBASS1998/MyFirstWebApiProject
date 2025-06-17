// src/api/auth.ts
import axios from "axios";
import type { ProfileDto } from "../dto/profile/ProfileDto";

// Вказати адресу до бекенду, якщо треба — змінити
const API = "/api/Profile";

// Отримати профіль користувача
export async function getProfile(): Promise<ProfileDto> {
  const response = await axios.get<ProfileDto>(`${API}/me`, { withCredentials: true });
    if (!response.data) {
        throw new Error("Profile not found");
    }
  return response.data;
}

// Оновити профіль користувача
export async function updateProfile(data: Partial<ProfileDto>): Promise<void> {
  await axios.put(`${API}/update`, data);
}