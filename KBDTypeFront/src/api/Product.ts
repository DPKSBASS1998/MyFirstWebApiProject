import axios from "axios";
import type { SwitchFilterDto } from "../dto/SwitchFilterDto";


// Вказати адресу до бекенду, якщо треба — змінити
const API = "/api/product";

export async function getProducts(): Promise<any[]> {
  const response = await axios.get<any[]>(`${API}`);
  return response.data;
}
export async function getProductById(id: number): Promise<any> {
  const response = await axios.get<any>(`${API}/${id}`);
  return response.data;
}

function cleanFilter(filter: any) {
  const cleaned: any = {};
  for (const key in filter) {
    const value = filter[key];
    if (
      value !== "" &&
      value !== null &&
      value !== undefined &&
      !(typeof value === "number" && isNaN(value))
    ) {
      cleaned[key] = value;
    }
  }
  return cleaned;
}

export async function filterProducts(filters: SwitchFilterDto): Promise<any[]> {
  const response = await axios.post<any[]>(
    `${API}/filter`,
    cleanFilter(filters)
  );
  return response.data;
}
// Uncomment and implement these functions if needed
// export async function addProduct(product: any): Promise<void> {
//   await axios.post(`${API}/add`, product);
// }
// export async function updateProduct(product: any): Promise<void> {
//   await axios.put(`${API}/update`, product);
// }
// export async function removeProduct(id: number): Promise<void> {
//   await axios.delete(`${API}/remove/${id}`);
// }
// export async function searchProducts(query: string): Promise<any[]> {
//   const response = await axios.get<any[]>(`${API}/search`, { params: { q: query } });
//   return response.data;
// }