export interface Product {
  id: number;
  name: string;
  description?: string;
  imageUrl?: string;
  price: number;
  stockQuantity: number;
  manufacturer?: string;
  inStock: boolean; // зручно для фронту, можна обчислювати: stockQuantity > 0
}