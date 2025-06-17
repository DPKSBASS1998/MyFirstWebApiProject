import React from "react";

interface ProfileFormProps {
  form: {
    firstName: string;
    lastName: string;
    phoneNumber: string;
    email: string;
  };
  handleChange: (e: React.ChangeEvent<HTMLInputElement>) => void;
}

export default function ProfileForm({ form, handleChange }: ProfileFormProps) {
  return (
    <>
      <label>
        Ім'я
        <input
          name="firstName"
          value={form.firstName}
          onChange={handleChange}
          required
        />
      </label>

      <label>
        Прізвище
        <input
          name="lastName"
          value={form.lastName}
          onChange={handleChange}
          required
        />
      </label>

      <label>
        Email
        <input
          name="email"
          type="email"
          value={form.email}
          readOnly
        />
      </label>

      <label>
        Телефон
        <input
          name="phoneNumber"
          value={form.phoneNumber}
          onChange={handleChange}
          required
        />
      </label>
    </>
  );
}