// src/api/auth.ts
import axios from "axios";
import type { LoginDto} from "../dto/auth/LoginDto";
import type { RegistrationDto } from "../dto/auth/RegistrationDto";

// Вказати адресу до бекенду, якщо треба — змінити
const API = "/api/Auth";

export async function getMe(): Promise<{ isAuthenticated: boolean; id?: number }> {
  try {
    const response = await axios.get<number>(`${API}/me`, { withCredentials: true });
    return { isAuthenticated: true, id: response.data };
  } catch (error) {
    return { isAuthenticated: false };
  }
}

// Логін — встановить кукі на сервері
export async function loginApi(data: LoginDto): Promise<void> {
  await axios.post(`${API}/login`, data, { withCredentials: true });
}

// Реєстрація — теж поставить кукі
export async function registerApi(data: RegistrationDto): Promise<void> {
  await axios.post(`${API}/register`, data, { withCredentials: true });
}

// Вийти — сервер очистить кукі
export async function logoutApi(): Promise<void> {
  await axios.post(`${API}/logout`, {}, { withCredentials: true });
}

