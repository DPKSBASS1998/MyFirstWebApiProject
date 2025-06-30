import axios from "axios";
import { OrderCreateDto } from "../dto/order/OrderCreateDto";

const API_URL = "/api/orders";

export async function createOrder(orderData: OrderCreateDto) {
  const response = await axios.post(`${API_URL}`, orderData);
  return response.data;
}

export async function getOrder() {
  const response = await axios.get(`${API_URL}`);
  return response.data;
}