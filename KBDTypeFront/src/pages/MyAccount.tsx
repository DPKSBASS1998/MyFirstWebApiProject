// src/pages/MyAccount.tsx
// ===== Імпорти =====
import ProfileForm from "../components/ProfileForm";
import AddressSection from "../components/AddressSection";
import "../styles/AccountShared.css";
// ===== Головний компонент сторінки =====
export default function MyAccount() {
  // --- Основний інтерфейс сторінки ---
  return (
    <div className="account-page">
      <h1>Мій акаунт</h1>

      {/* Автономна форма профілю */}
      <ProfileForm />

      {/* Автономна секція адрес */}
      <AddressSection />
    </div>
  );
}
