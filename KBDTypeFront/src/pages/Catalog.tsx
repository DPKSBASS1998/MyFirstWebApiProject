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

  // Розрахунок min/max значень для фільтрів
  const minValues = useMemo(() => {
    if (!products.length) {
      return {
        operatingForce: 0,
        totalTravel: 0,
        preTravel: 0,
        tactilePos: 0,
        tactileForce: 0,
        pinCount: 0,
        price: 0,
      };
    }
    return {
      operatingForce: Math.min(...products.map((p) => p.operatingForce)),
      totalTravel: Math.min(...products.map((p) => p.totalTravel)),
      preTravel: Math.min(...products.map((p) => p.preTravel)),
      tactilePos: Math.min(...products.map((p) => p.tactilePosition)),
      tactileForce: Math.min(...products.map((p) => p.tactileForce)),
      pinCount: Math.min(...products.map((p) => p.pinCount)),
      price: Math.min(...products.map((p) => p.price)),
    };
  }, [products]);

  const maxValues = useMemo(() => {
    if (!products.length) {
      return {
        operatingForce: 0,
        totalTravel: 0,
        preTravel: 0,
        tactilePos: 0,
        tactileForce: 0,
        pinCount: 0,
        price: 0,
      };
    }
    return {
      operatingForce: Math.max(...products.map((p) => p.operatingForce)),
      totalTravel: Math.max(...products.map((p) => p.totalTravel)),
      preTravel: Math.max(...products.map((p) => p.preTravel)),
      tactilePos: Math.max(...products.map((p) => p.tactilePosition)),
      tactileForce: Math.max(...products.map((p) => p.tactileForce)),
      pinCount: Math.max(...products.map((p) => p.pinCount)),
      price: Math.max(...products.map((p) => p.price)),
    };
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

  return (
    <div className="catalog-page">
      <aside className="catalog-sidebar">
        <h3>Фільтри</h3>
        <ProductFilter
          filter={pendingFilter}
          onChange={handleFilterChange}
          onSubmit={handleFilterSubmit}
          minValues={minValues}
          maxValues={maxValues}
        />
      </aside>

      <main className="catalog-main">
        <ProductGrid filter={filter} />
      </main>
    </div>
  );
}