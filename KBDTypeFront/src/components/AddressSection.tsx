// src/components/AddressSection.jsx
import React, { useState, useEffect } from "react";
import type { AddressDto } from "../dto/address/AddressDto";
import {
  getAddresses,
  addAddress as apiAddAddress,
  updateAddress as apiUpdateAddress,
  removeAddress as apiRemoveAddress,
} from "../api/Address";
import "../styles/AddressSection.css";

// AddressCard — картка адреси
type AddressCardProps = {
  addr: AddressDto;
  onEdit: (id: number) => void;
  onDelete: (id: number) => void;
  isSelected: boolean;
  onSelect: () => void;
};
function AddressCard({ addr, onEdit, onDelete, isSelected, onSelect }: AddressCardProps) {
  return (
    <div
      className={`address-block${isSelected ? " selected" : ""}`}
      onClick={onSelect}
    >
      <div className="address-info">
        <p>{`${addr.region}, ${addr.city}`}</p>
        <p>
          {`${addr.street}, буд. ${addr.building}${addr.apartment ? `, кв. ${addr.apartment}` : ""}`}
        </p>
        <p>{addr.postalCode}</p>
      </div>
      <div className="address-actions">
        <button
          type="button"
          className="delete-btn"
          onClick={(e) => {
            e.stopPropagation();
            onDelete(addr.id);
          }}
          aria-label="Видалити адресу"
        >
          <i className="bi bi-x-lg"></i>
        </button>
        <button
          type="button"
          className="edit-btn"
          onClick={(e) => {
            e.stopPropagation();
            onEdit(addr.id);
          }}
          aria-label="Редагувати адресу"
        >
          <i className="bi bi-pencil"></i>
        </button>
      </div>
    </div>
  );
}

// AddressEditor — редактор адреси (модалка)
type AddressEditorProps = {
  address: Partial<AddressDto>;
  onSave: (address: Partial<AddressDto>) => void;
  onCancel: () => void;
};
function AddressEditor({ address, onSave, onCancel }: AddressEditorProps) {
  const [form, setForm] = useState<Partial<AddressDto>>(address || {});

  useEffect(() => {
    setForm(address || {});
  }, [address]);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setForm((prev) => ({ ...prev, [name]: value }));
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    onSave(form);
  };

  return (
    <div className="modal-backdrop">
      <div className="modal-content">
        <form className="account-form" onSubmit={handleSubmit}>
          <fieldset>
            <legend>Редагування адреси</legend>
            <label>
              Область
              <input
                name="region"
                value={form.region || ""}
                onChange={handleChange}
                placeholder="Область"
              />
            </label>
            <label>
              Місто
              <input
                name="city"
                value={form.city || ""}
                onChange={handleChange}
                placeholder="Місто"
              />
            </label>
            <label>
              Вулиця
              <input
                name="street"
                value={form.street || ""}
                onChange={handleChange}
                placeholder="Вулиця"
              />
            </label>
            <label>
              Будинок
              <input
                name="building"
                value={form.building || ""}
                onChange={handleChange}
                placeholder="Будинок"
              />
            </label>
            <label>
              Квартира
              <input
                name="apartment"
                value={form.apartment || ""}
                onChange={handleChange}
                placeholder="Квартира"
              />
            </label>
            <label>
              Індекс
              <input
                name="postalCode"
                value={form.postalCode || ""}
                onChange={handleChange}
                placeholder="Індекс"
              />
            </label>
            <div className="fieldset-actions">
              <button type="submit" className="save-btn">
                Зберегти
              </button>
              <button
                type="button"
                className="logout-btn"
                onClick={onCancel}
              >
                Скасувати
              </button>
            </div>
          </fieldset>
        </form>
      </div>
    </div>
  );
}

// ======= АВТОНОМНИЙ AddressSection =======
export default function AddressSection() {
  const [addresses, setAddresses] = useState<AddressDto[]>([]);
  const [selectedAddressId, setSelectedAddressId] = useState<number | null>(null);
  const [showAddresses, setShowAddresses] = useState(true);
  const [editingAddress, setEditingAddress] = useState<Partial<AddressDto> | null>(null);
  const [loading, setLoading] = useState(false);

  // Завантажити адреси при монтуванні
  useEffect(() => {
    loadAddresses();
    // eslint-disable-next-line
  }, []);

  const loadAddresses = async () => {
    setLoading(true);
    try {
      const data = await getAddresses();
      setAddresses(data);
      if (data.length > 0) setSelectedAddressId(data[0].id);
    } finally {
      setLoading(false);
    }
  };

  const handleAddAddress = () => {
    setEditingAddress({
      region: "",
      city: "",
      street: "",
      apartment: "",
      postalCode: "",
      userId: 0,
    });
  };

  const handleEditAddress = (id: number) => {
    const addr = addresses.find((a) => a.id === id);
    if (addr) setEditingAddress(addr);
  };

  const handleDeleteAddress = async (id: number) => {
    setLoading(true);
    await apiRemoveAddress(id);
    await loadAddresses();
    setLoading(false);
  };

  const handleSave = async (address: Partial<AddressDto>) => {
    setLoading(true);
    if (address.id) {
      await apiUpdateAddress(address as AddressDto);
    } else {
      await apiAddAddress(address as AddressDto);
    }
    setEditingAddress(null);
    await loadAddresses();
    setLoading(false);
  };

  const handleSelect = (id: number) => {
    setSelectedAddressId(id);
  };

  return (
    <div className="address-section">
      <div className="address-header">
        <span className="address-label">Мої адреси</span>
        <button
          type="button"
          className="add-address-btn"
          onClick={handleAddAddress}
          aria-label="Додати нову адресу"
        >
          <i className="bi bi-plus-lg"></i>
        </button>
        <button
          type="button"
          className="toggle-addresses-btn"
          onClick={() => setShowAddresses((v) => !v)}
          aria-label={showAddresses ? "Приховати адреси" : "Показати адреси"}
        >
          <i
            className={`bi ${
              showAddresses ? "bi-chevron-up" : "bi-chevron-down"
            }`}
          ></i>
        </button>
      </div>

      {showAddresses ? (
        <div className="addresses-dropdown">
          {editingAddress && (
            <AddressEditor
              address={editingAddress}
              onSave={handleSave}
              onCancel={() => setEditingAddress(null)}
            />
          )}

          {addresses.length === 0 && !editingAddress ? (
            <p className="no-address-msg">У вас ще немає адрес</p>
          ) : (
            addresses.map((addr) => (
              <AddressCard
                key={addr.id}
                addr={addr}
                onEdit={handleEditAddress}
                onDelete={handleDeleteAddress}
                isSelected={addr.id === selectedAddressId}
                onSelect={() => handleSelect(addr.id)}
              />
            ))
          )}
        </div>
      ) : (
        selectedAddressId && addresses.length > 0 ? (
          <div className="addresses-dropdown">
            <AddressCard
              key={selectedAddressId}
              addr={addresses.find(a => a.id === selectedAddressId)!}
              onEdit={handleEditAddress}
              onDelete={handleDeleteAddress}
              isSelected={true}
              onSelect={() => handleSelect(selectedAddressId)}
            />
          </div>
        ) : null
      )}
      {loading && <div className="loading-overlay">Завантаження...</div>}
    </div>
  );
}
