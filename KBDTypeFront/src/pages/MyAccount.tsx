// src/pages/MyAccount.tsx
import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import {
  useProfileData,
  useProfileLoading,
  useFetchProfile,
  useUpdateProfile,
} from "../hooks/useProfile";
import {
  useAddresses,
  useFetchAddresses,
  useAddAddress,
  useUpdateAddress,
  useRemoveAddress,
  useSelectAddress,
  useSelectedAddressId,
} from "../hooks/useAddresses";
import ProfileForm from "../components/ProfileForm";
import AddressSection from "../components/AddressSection";
import "../styles/AccountShared.css";
import type { ProfileDto } from "../dto/profile/ProfileDto";
import type { AddressDto } from "../dto/address/AddressDto";

export default function MyAccount() {
  const navigate = useNavigate();

  // Профіль
  const profile = useProfileData() as ProfileDto | null;
  const profileLoading = useProfileLoading();
  const fetchProfile = useFetchProfile();
  const updateProfile = useUpdateProfile();

  // Адреси
  const addresses = useAddresses();
  const fetchAddresses = useFetchAddresses();
  const addAddress = useAddAddress();
  const updateAddress = useUpdateAddress();
  const removeAddress = useRemoveAddress();
  const selectAddress = useSelectAddress();
  const selectedAddressId = useSelectedAddressId();

  // Локальний стан для форми профілю
  const [form, setForm] = useState<ProfileDto>(
    profile || {
      firstName: "",
      lastName: "",
      phoneNumber: "",
      email: "",
      addresses: [],
    }
  );
  const [message, setMessage] = useState<string | null>(null);
  const [saving, setSaving] = useState(false);

  // Підтягнути профіль та адреси при монтуванні
  useEffect(() => {
    fetchProfile();
    fetchAddresses();
    // eslint-disable-next-line
  }, []);

  // Оновлювати форму при зміні профілю
  useEffect(() => {
    if (profile) setForm(profile);
  }, [profile]);

  // Якщо не авторизовані, редірект назад
  useEffect(() => {
    if (profileLoading) return;
    if (!profile) {
      navigate("/", { replace: true });
    }
  }, [profileLoading, profile, navigate]);

  // Зміна полів профілю
  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setForm((prev) => ({ ...prev, [name]: value }));
  };

  // Зберегти профіль
  const handleSaveProfile = async (e?: React.FormEvent) => {
    if (e) e.preventDefault();
    setSaving(true);
    setMessage(null);
    try {
      await updateProfile(form);
      setMessage("Профіль збережено!");
    } catch (err) {
      setMessage("Помилка збереження профілю");
    }
    setSaving(false);
  };

  if (profileLoading) {
    return <div className="account-status">Завантаження даних…</div>;
  }

  return (
    <div className="account-page">
      <h1>Мій акаунт</h1>

      {message && <p className="form-error">{message}</p>}

      <form className="account-form" onSubmit={handleSaveProfile}>
        <fieldset>
          <legend>Ваші дані</legend>
          <ProfileForm form={form} handleChange={handleChange} />
          <div className="fieldset-actions">
            <button type="submit" className="save-btn" disabled={saving}>
              {saving ? "Збереження…" : "Зберегти зміни"}
            </button>
          </div>
        </fieldset>

        <section className="address-section">
          <fieldset>
            <legend>Ваші адреси</legend>
            <AddressSection
              addresses={addresses}
              addAddress={addAddress}
              updateAddress={updateAddress}
              removeAddress={removeAddress}
              selectAddress={selectAddress}
              selectedAddressId={selectedAddressId}
            />
          </fieldset>
        </section>
      </form>
    </div>
  );
}
