// src/components/Catalog/ProductGrid.jsx
import ProductCard from "./ProductCard";
import { useProducts } from "../hooks/useProducts";
import type { Switches } from "../models/Switches";

export default function ProductGrid({ filter }: { filter: any }) {
  const { products, loading } = useProducts(filter);

  if (loading) return <div>Завантаження...</div>;

  return (
    <div className="catalog-grid">
      {products
        .slice()
        .sort((a, b) => (b.inStock ? 1 : 0) - (a.inStock ? 1 : 0))
        .map((p: Switches) => (
          <ProductCard
            key={p.id}
            product={p}
          />
        ))}
    </div>
  );
}
