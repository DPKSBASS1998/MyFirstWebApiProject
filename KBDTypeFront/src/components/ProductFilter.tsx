// src/components/Product/ProductFilters.tsx
import React from "react";

type ProductFilterProps = {
  filter: Record<string, any>;
  onChange: (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => void;
  onSubmit: (e: React.FormEvent<HTMLFormElement>) => void;
  minValues: Record<string, number>;
  maxValues: Record<string, number>;
  numericFields: string[];
};

/**
 * CatalogFilters — компонент, який відображає блок фільтрів
 */
export default function ProductFilter({
  filter,
  onChange,
  onSubmit,
  minValues,
  maxValues,
  numericFields,
}: ProductFilterProps) {
  return (
    <form className="catalog-form" onSubmit={onSubmit}>
      {numericFields.map((field) => (
        <div className="catalog-block" key={field}>
          <span>{field}:</span>
          <input
            name={`min${field.charAt(0).toUpperCase() + field.slice(1)}`}
            type="number"
            className="catalog-input"
            value={filter[`min${field.charAt(0).toUpperCase() + field.slice(1)}`] ?? ""}
            onChange={onChange}
            placeholder={`від ${minValues[field] ?? ""}`}
          />
          <input
            name={`max${field.charAt(0).toUpperCase() + field.slice(1)}`}
            type="number"
            className="catalog-input"
            value={filter[`max${field.charAt(0).toUpperCase() + field.slice(1)}`] ?? ""}
            onChange={onChange}
            placeholder={`до ${maxValues[field] ?? ""}`}
          />
        </div>
      ))}

      <button type="submit" className="catalog-button">
        Застосувати
      </button>
    </form>
  );
}
