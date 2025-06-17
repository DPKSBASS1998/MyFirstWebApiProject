import React, { useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { useCart } from "../hooks/useCart";
import { useProfileData, useProfileLoading, useFetchProfile, useUpdateProfile } from "../hooks/useProfile";
import { 
  useSelectedAddressId, 
  useAddresses,
  useFetchAddresses, 
  useAddAddress, 
  useUpdateAddress, 
  useRemoveAddress, 
  useSelectAddress 
} from "../hooks/useAddresses";
import ProfileForm from "../components/ProfileForm";
import AddressSection from "../components/AddressSection";
import OrderSummary from "../components/OrderSummary";
import "../styles/AccountShared.css";   
import type { ProfileDto } from "../dto/profile/ProfileDto";
import type { AddressDto } from "../dto/address/AddressDto";
import type { CartItem } from "../store/cartSlice";
import { useIsAuthenticated } from "../hooks/useAuth";
import { useCreateOrder } from "../hooks/useOrder";
import type { CreateOrderDto } from "../dto/order/OrderCreateDto";

export default function Checkout() {
  const navigate = useNavigate();
  const { items: cartItems, clearCart } = useCart();

    // Перевірка авторизації
    const isAuthenticated = useIsAuthenticated();

  // Профіль
  const profile = useProfileData() as ProfileDto | null;
  const profileLoading = useProfileLoading();
  const fetchProfile = useFetchProfile();
  const updateProfile = useUpdateProfile();

  // Адреси
  const selectedAddressId = useSelectedAddressId() as number | null;
  const addresses = useAddresses();
  const fetchAddresses = useFetchAddresses();
  const addAddress = useAddAddress();
  const updateAddress = useUpdateAddress();
  const removeAddress = useRemoveAddress();
  const selectAddress = useSelectAddress();

  // Локальний стан для форми профілю
  const [form, setForm] = React.useState<ProfileDto>(
    profile || {
      firstName: "",
      lastName: "",
      phoneNumber: "",
      email: "",
      addresses: [],
    }
  );
  const [editingAddress, setEditingAddress] = React.useState<AddressDto | null>(null);
  const [message, setMessage] = React.useState<string | null>(null);
  const [saving, setSaving] = React.useState(false);

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

  const handleSaveAddress = async (address: AddressDto) => {
    setSaving(true);
    setMessage(null);
    try {
      if (address.id) {
        await updateAddress(address);
      } else {
        await addAddress(address);
      }
      setEditingAddress(null);
      setMessage("Адресу збережено!");
    } catch (err) {
      setMessage("Помилка збереження адреси");
    }
    setSaving(false);
  };

  const { create } = useCreateOrder();

  // Сабміт замовлення
  const handleSubmitOrder = async (e: React.FormEvent) => {
    e.preventDefault();
    setSaving(true);
    setMessage(null);
    try {
      await handleSaveProfile();
      if (editingAddress) {
        await handleSaveAddress(editingAddress);
      }
      const payload: CreateOrderDto = {
        profile: {
          firstName: form.firstName,
          lastName: form.lastName,
          phoneNumber: form.phoneNumber,
          email: form.email,
        },
        addressId: selectedAddressId!,
        items: cartItems.map((i: CartItem) => ({
          productId: i.productId,
          quantity: i.quantity,
        })),
      };
      await create(payload); // Використовуємо create з хука
      clearCart();
      navigate("/thank-you");
    } catch (err) {
      setMessage("Помилка оформлення замовлення");
    }
    setSaving(false);
  };

  if (profileLoading) {
    return <div className="account-status">Завантаження даних…</div>;
  }

  return (
    <div className="account-page">
      <h1>Оформлення замовлення</h1>

      {message && <p className="form-error">{message}</p>}

      <form className="account-form" onSubmit={handleSubmitOrder}>
        {/* --- 1. ПРОФІЛЬ --- */}
        <fieldset>
          <legend>Ваші дані</legend>
          <ProfileForm form={form} handleChange={handleChange} />
          <div className="fieldset-actions">
            <button
              type="button"
              className="save-btn"
              onClick={handleSaveProfile}
              disabled={saving}
            >
              {saving ? "Збереження…" : "Зберегти зміни"}
            </button>
          </div>
        </fieldset>

        {/* --- 2. АДРЕСИ --- */}
        <section className="address-section">
          <fieldset>
            <legend>Оберіть адресу доставки</legend>
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

        {/* --- 3. ПІДСУМКИ І ОФОРМЛЕННЯ --- */}
        <OrderSummary />

        {/* --- 4. КНОПКА ОФОРМЛЕННЯ --- */}
        <button type="submit" className="submit-btn" disabled={saving}>
          {saving ? "Зачекайте…" : "Підтвердити та оплатити"}
        </button>
      </form>
    </div>
  );
}