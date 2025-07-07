import { useState, useEffect } from "react";
import { getAllProducts } from "../api/Product";
import type { Switches } from "../models/Switches";

export function useProducts() {
  const [products, setProducts] = useState<Switches[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    setLoading(true);
    const fetch = async () => {
      try {
        const data = await getAllProducts();
        setProducts(data);
      } finally {
        setLoading(false);
      }
    };
    fetch();
  }, []);

  return { products, loading };
}