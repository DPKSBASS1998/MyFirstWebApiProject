// src/api/auth.ts
import axios from "axios";
import type { UserProfileDto } from "../dto/profile/UserProfileDto";

// Вказати адресу до бекенду, якщо треба — змінити
const API = "/api/Profiles";

// Отримати профіль користувача
export async function getProfile(): Promise<UserProfileDto> {
  const response = await axios.get<UserProfileDto>(API, { withCredentials: true });
  if (!response.data) {
    throw new Error("Profile not found");
  }
  return response.data;
}

// Оновити профіль користувача
export async function updateProfile(data: Partial<UserProfileDto>): Promise<UserProfileDto> {
  const response = await axios.put<UserProfileDto>(API, data, { withCredentials: true });
  if (!response.data) {
    throw new Error("Failed to update profile");
  }
  return response.data;
}