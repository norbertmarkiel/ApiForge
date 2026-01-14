import { api } from "./http";

export interface OrderRequest {
  orderId?: string;
  amount?: number;
  isPaid?: boolean;
}
export const createOrder = async (payload: OrderRequest) => {
  const res = await api.post("/orders/create", payload);
  return res.data;
};