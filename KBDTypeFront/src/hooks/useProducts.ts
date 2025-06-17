import { useState, useEffect } from "react";
import { getProducts, filterProducts } from "../api/Product";
import type { SwitchFilterDto } from "../dto/SwitchFilterDto";
import type { Switches } from "../models/Switches";

export function useProducts(filter?: SwitchFilterDto) {
  const [products, setProducts] = useState<Switches[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    setLoading(true);
    const fetch = async () => {
      try {
        let data;
        if (filter) {
          data = await filterProducts(filter);
        } else {
          data = await getProducts();
        }
        setProducts(data);
      } finally {
        setLoading(false);
      }
    };
    fetch();
  }, [JSON.stringify(filter)]);

  return { products, loading };
}