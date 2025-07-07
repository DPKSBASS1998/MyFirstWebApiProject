import React, { useEffect, useState } from "react";
import { UserProfileDto } from "../dto/profile/UserProfileDto";
import { useProfileData, useUpdateProfile, useFetchProfile } from "../hooks/useProfile";
import "../styles/AccountShared.css";

export default function ProfileForm() {
  // Тепер profile — це або UserProfileDto, або null
  const profile = useProfileData() as UserProfileDto | null;
  const fetchProfile = useFetchProfile();
  const updateProfile = useUpdateProfile();

  const [form, setForm] = useState<UserProfileDto | null>(null);
  const [readOnly, setReadOnly] = useState(true);
  const [message, setMessage] = useState<string | null>(null);

  useEffect(() => {
    fetchProfile();
  }, []);

  useEffect(() => {
    // Якщо profile є і ми не редагуємо, створюємо новий екземпляр класу
    if (profile && (readOnly || form === null)) setForm(new UserProfileDto(profile));
  }, [profile, readOnly]);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setForm((prev) =>
      prev ? new UserProfileDto({ ...prev, [name]: value }) : prev
    );
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!form) return;
    setMessage(null);
    try {
      await updateProfile(form);
      setMessage("Профіль збережено!");
      setReadOnly(true);
    } catch {
      setMessage("Помилка збереження профілю");
    }
  };

  if (!form) return <div className="account-status">Завантаження…</div>;

  return (
    <div>
      <form className="account-form" onSubmit={handleSubmit}>
        <fieldset>
          <legend className="profile-legend">
            <span className="profile-legend-title">Ваші дані</span>
            {readOnly && (
              <button
                type="button"
                className="edit-btn"
                onClick={() => setReadOnly(false)}
                aria-label="Редагувати"
              >
                <i className="bi bi-pencil"></i>
              </button>
            )}
          </legend>
          <label>
            Ім'я
            <input
              name="firstName"
              value={form.firstName}
              onChange={handleChange}
              required
              readOnly={readOnly}
            />
          </label>
          <label>
            Прізвище
            <input
              name="lastName"
              value={form.lastName}
              onChange={handleChange}
              required
              readOnly={readOnly}
            />
          </label>
          <label>
            Email
            <input
              name="email"
              type="email"
              value={form.email ?? ""}
              onChange={handleChange}
              readOnly={readOnly}
            />
          </label>
          <label>
            Телефон
            <input
              name="phoneNumber"
              value={form.phoneNumber}
              onChange={handleChange}
              required
              readOnly={readOnly}
            />
          </label>
          {message && (
            <div className={message.includes("помилка") ? "form-error" : "form-message"}>
              {message}
            </div>
          )}

          {!readOnly && (
            <div className="profile-actions">
              <button type="submit" className="save-btn">
                Зберегти
              </button>
            </div>
          )}
        </fieldset>
      </form>
    </div>
  );
}