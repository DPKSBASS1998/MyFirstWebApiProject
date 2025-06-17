import axios from "axios";
import type { CreateOrderDto } from "../dto/order/OrderCreateDto";

const API_URL = "/api/order";

export async function createOrder(orderData: CreateOrderDto) {
  const response = await axios.post(`${API_URL}/create`, orderData);
  return response.data;
}

export async function getOrder(orderId: number) {
  const response = await axios.get(`${API_URL}/${orderId}`);
  return response.data;
}

export async function updateOrder(orderId: number, orderData: any) {
  const response = await axios.put(`${API_URL}/update/${orderId}`, orderData);
  return response.data;
}

export async function deleteOrder(orderId: number) {
  const response = await axios.delete(`${API_URL}/delete/${orderId}`);
  return response.data;
}