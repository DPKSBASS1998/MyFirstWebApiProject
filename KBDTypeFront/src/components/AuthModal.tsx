// src/components/AuthModal.tsx
import React, { useState } from "react";
import { useLogin, useRegister } from "../hooks/useAuth";
import type { LoginDto } from "../dto/auth/LoginDto";
import type { RegisterDto } from "../dto/auth/RegistrationDto/";
import "../styles/AuthModal.css";

// Оновлені початкові стани згідно з DTO
const initialLoginState: LoginDto = { identifier: "", password: "", rememberMe: false };
const initialRegisterState: RegisterDto = {
  firstName: "",
  lastName: "",
  phoneNumber: "",
  email: "",
  password: "",
};

type Mode = "login" | "register";

type AuthModalProps = {
  onClose: () => void;
};

export default function AuthModal({ onClose }: AuthModalProps) {
  const login = useLogin();
  const register = useRegister();
  const [mode, setMode] = useState<Mode>("login");
  const [form, setForm] = useState<LoginDto | (RegisterDto & { confirmPassword?: string })>(initialLoginState);
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
        const regForm = form as RegisterDto & { confirmPassword?: string };

        const phoneRegex = /^380\d{9}$/;
        if (!phoneRegex.test(regForm.phoneNumber)) {
          return setError("Телефон у форматі 380XXXXXXXXX");
        }
        if (regForm.password !== regForm.confirmPassword) {
          return setError("Паролі не співпадають");
        }

        // Відправляємо тільки поля RegisterDto
        const { confirmPassword, ...registerDto } = regForm;
        await register(registerDto as RegisterDto);
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

        {/* --- Блок перемикача вкладок (Вхід/Реєстрація) --- */}
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
          {/* --- Блок реєстрації --- */}
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
                placeholder="Телефон (380XXXXXXXXX)"
                value={(form as RegisterDto).phoneNumber}
                onChange={handleChange}
                required
              />
              <input
                name="email"
                type="email"
                placeholder="Email"
                value={(form as RegisterDto).email ?? ""}
                onChange={handleChange}
              />
            </>
          )}

          {/* --- Блок входу --- */}
          {mode === "login" && (
            <input
              name="identifier"
              type="text"
              placeholder="Email або номер телефону"
              value={(form as LoginDto).identifier}
              onChange={handleChange}
              required
            />
          )}

          {/* --- Блок пароля (загальний для обох режимів) --- */}
          <div className="password-wrapper">
            <input
              name="password"
              type={showPassword ? "text" : "password"}
              placeholder="Пароль"
              value={mode === "register"
                ? (form as RegisterDto).password
                : (form as LoginDto).password}
              onChange={handleChange}
              required
              autoComplete={mode === "register" ? "new-password" : "current-password"}
            />
            <i
              className={`bi ${showPassword ? "bi-eye" : "bi-eye-slash"} password-toggle-icon`}
              onClick={() => setShowPassword((prev) => !prev)}
              role="button"
              aria-label={showPassword ? "Приховати пароль" : "Показати пароль"}
            />
          </div>

          {/* --- Чекбокс "Запам'ятати мене" тільки для входу --- */}
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

          {/* --- Підтвердження пароля та підказка тільки для реєстрації --- */}
          {mode === "register" && (
            <>
              <p className="password-hint">
                Мінімум 6 символів, принаймні 1 велика й 1 мала літера, 1 цифра та 1 спецсимвол.
              </p>
              <input
                name="confirmPassword"
                type="password"
                placeholder="Підтвердження пароля"
                value={(form as any).confirmPassword ?? ""}
                onChange={handleChange}
                required
                autoComplete="new-password"
              />
            </>
          )}

          {/* --- Відображення помилки --- */}
          {error && <p className="form-error">{error}</p>}

          {/* --- Кнопка підтвердження --- */}
          <button type="submit" className="submit-btn">
            {mode === "login" ? "Увійти" : "Зареєструватися"}
          </button>
        </form>
      </div>
    </div>
  );
}
