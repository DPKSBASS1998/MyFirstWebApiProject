import React, { useState, useMemo } from "react";
import ProductFilter from "../components/ProductFilter";
import ProductGrid from "../components/ProductGrid";
import { useProducts } from "../hooks/useProducts";
import type { SwitchFilterDto } from "../dto/SwitchFilterDto";
import "../styles/Catalog.css";

export default function Catalog() {
  const [filter, setFilter] = useState<SwitchFilterDto>({});
  const [pendingFilter, setPendingFilter] = useState<SwitchFilterDto>({});
  const { products, loading } = useProducts(filter);

  const getNumericFields = (products: any[]) => {
    if (!products.length) return [];
    return Object.keys(products[0]).filter(
      (key) => typeof products[0][key] === "number"
    );
  };

  // Розрахунок min/max значень для фільтрів  
  const minValues = useMemo(() => {
    if (!products.length) return {};
    const fields = getNumericFields(products);
    const result: Record<string, number> = {};
    for (const key of fields) {
      result[key] = Math.min(...products.map((p) => p[key]));
    }
    return result;
  }, [products]);

  const maxValues = useMemo(() => {
    if (!products.length) return {};
    const fields = getNumericFields(products);
    const result: Record<string, number> = {};
    for (const key of fields) {
      result[key] = Math.max(...products.map((p) => p[key]));
    }
    return result;
  }, [products]);

  const handleFilterChange = (
    e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>
  ) => {
    const { name, value, type } = e.target;
    setPendingFilter((prev) => ({
      ...prev,
      [name]:
        type === "checkbox"
          ? (e.target as HTMLInputElement).checked
          : value,
    }));
  };

  const handleFilterSubmit = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    setFilter(pendingFilter); // Оновлюємо filter лише по сабміту
  };

  // При першому завантаженні pendingFilter = filter
  React.useEffect(() => {
    setPendingFilter(filter);
  }, [filter]);

  const numericFields = React.useMemo(() => {
    if (!products.length) return [];
    return Object.keys(products[0]).filter(
      (key) => typeof products[0][key] === "number"
    );
  }, [products]);

  return (
    <div className="catalog-page">
      <aside className="catalog-sidebar">
        <h3>Фільтри</h3>
        <ProductFilter
          filter={filter}
          onChange={handleFilterChange}
          onSubmit={handleFilterSubmit}
          minValues={minValues}
          maxValues={maxValues}
          numericFields={numericFields}
        />
      </aside>

      <main className="catalog-main">
        <ProductGrid filter={filter} />
      </main>
    </div>
  );
}