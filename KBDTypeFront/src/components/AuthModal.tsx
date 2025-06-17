// src/components/AuthModal.tsx
import React, { useState } from "react";
import { useLogin, useRegister } from "../hooks/useAuth";
import type { LoginDto } from "../dto/auth/LoginDto";
import type { RegisterDto } from "../dto/auth/RegisterDto";
import "../styles/AuthModal.css";

const initialLoginState: LoginDto = { email: "", password: "", rememberMe: false };
const initialRegisterState: RegisterDto = {
  firstName: "",
  lastName: "",
  phoneNumber: "",
  email: "",
  password: "",
  confirmPassword: "",
  assignAsManager: false,
};

type Mode = "login" | "register";

type AuthModalProps = {
  onClose: () => void;
};

export default function AuthModal({ onClose }: AuthModalProps) {
  const login = useLogin();
  const register = useRegister();
  const [mode, setMode] = useState<Mode>("login");
  const [form, setForm] = useState<LoginDto | RegisterDto>(initialLoginState);
  const [showPassword, setShowPassword] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value, type, checked } = e.target;
    setForm((prev) => ({
      ...prev,
      [name]: type === "checkbox" ? checked : value,
    }));
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError(null);

    try {
      if (mode === "register") {
        const regForm = form as RegisterDto;

        const phoneRegex = /^\+380\d{9}$/;
        if (!phoneRegex.test(regForm.phoneNumber)) {
          return setError("Телефон у форматі +380XXXXXXXXX");
        }
        if (regForm.password !== regForm.confirmPassword) {
          return setError("Паролі не співпадають");
        }

        await register(regForm);
      } else {
        const loginForm = form as LoginDto;
        await login(loginForm);
      }

      onClose();
    } catch (err: any) {
      setError("Невірна пошта або пароль");
    }
  };

  return (
    <div className="modal-overlay" onClick={onClose}>
      <div className="auth-modal" onClick={(e) => e.stopPropagation()}>
        <button className="close-btn" onClick={onClose}>&times;</button>

        <div className="tabs">
          <button
            className={mode === "login" ? "active" : ""}
            onClick={() => {
              setMode("login");
              setForm(initialLoginState);
              setError(null);
            }}
          >
            Увійти
          </button>
          <button
            className={mode === "register" ? "active" : ""}
            onClick={() => {
              setMode("register");
              setForm(initialRegisterState);
              setError(null);
            }}
          >
            Зареєструватися
          </button>
        </div>

        <form className="auth-form" onSubmit={handleSubmit}>
          {mode === "register" && (
            <>
              <input
                name="firstName"
                type="text"
                placeholder="Ім'я"
                value={(form as RegisterDto).firstName}
                onChange={handleChange}
                required
              />
              <input
                name="lastName"
                type="text"
                placeholder="Прізвище"
                value={(form as RegisterDto).lastName}
                onChange={handleChange}
                required
              />
              <input
                name="phoneNumber"
                type="tel"
                placeholder="Телефон (+380XXXXXXXXX)"
                value={(form as RegisterDto).phoneNumber}
                onChange={handleChange}
                required
              />
            </>
          )}

          <input name="email" type="email" placeholder="Email" value={form.email} onChange={handleChange} required />

          <div className="password-wrapper">
            <input
              name="password"
              type={showPassword ? "text" : "password"}
              placeholder="Пароль"
              value={form.password}
              onChange={handleChange}
              required
              autoComplete={mode === "register" ? "new-password" : "current-password"}
              pattern="(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*\W).{6,}"
              title="Мінімум 6 символів, одна велика й одна мала літера, цифра, спецсимвол"
            />
            <i
              className={`bi ${showPassword ? "bi-eye" : "bi-eye-slash"} password-toggle-icon`}
              onClick={() => setShowPassword((prev) => !prev)}
              role="button"
              aria-label={showPassword ? "Приховати пароль" : "Показати пароль"}
            />
          </div>

          {/* Чекбокс для rememberMe тільки для логіну */}
          {mode === "login" && (
            <label className="remember-me-label" style={{ display: "flex", alignItems: "center", gap: 8, margin: "8px 0" }}>
              <input
                type="checkbox"
                name="rememberMe"
                checked={(form as LoginDto).rememberMe}
                onChange={handleChange}
              />
              Запам'ятати мене
            </label>
          )}

          {mode === "register" && (
            <>
              <p className="password-hint">
                Мінімум 6 символів, принаймні 1 велика й 1 мала літера, 1 цифра та 1 спецсимвол.
              </p>
              <input
                name="confirmPassword"
                type="password"
                placeholder="Підтвердження пароля"
                value={(form as RegisterDto).confirmPassword}
                onChange={handleChange}
                required
                autoComplete="new-password"
              />
            </>
          )}

          {error && <p className="form-error">{error}</p>}

          <button type="submit" className="submit-btn">
            {mode === "login" ? "Увійти" : "Зареєструватися"}
          </button>
        </form>
      </div>
    </div>
  );
}
