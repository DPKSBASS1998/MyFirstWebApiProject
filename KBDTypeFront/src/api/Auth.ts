// src/api/auth.ts
import axios from "axios";
import type { LoginDto} from "../dto/auth/LoginDto";
import type { RegisterDto } from "../dto/auth/RegisterDto";
import type { MeResponse } from "../dto/auth/MeResponse";

// Вказати адресу до бекенду, якщо треба — змінити
const API = "/api/Auth";

export async function getMe(): Promise<MeResponse> {
  const response = await axios.get<MeResponse>(`${API}/me`, { withCredentials: true });
  return response.data;
}

// Логін — встановить кукі на сервері
export async function loginApi(data: LoginDto): Promise<void> {
  await axios.post(`${API}/login`, data, { withCredentials: true });
}

// Реєстрація — теж поставить кукі
export async function registerApi(data: RegisterDto): Promise<void> {
  await axios.post(`${API}/register`, data, { withCredentials: true });
}

// Вийти — сервер очистить кукі
export async function logoutApi(): Promise<void> {
  await axios.post(`${API}/logout`, {}, { withCredentials: true });
}

