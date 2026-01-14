import { api } from "./http";
import type { AdminEndpoint } from "../types/endpoint";

export const getEndpoints = async () => {
  const res = await api.get<AdminEndpoint[]>("/admin/endpoints");
  return res.data;
};

export const createEndpoint = async (payload: AdminEndpoint) => {
  const res = await api.post("/admin/endpoints", payload);
  return res.data;
};

export const updateEndpoint = async (id: string, payload: AdminEndpoint) => {
  const res = await api.put(`/admin/endpoints/${id}`, payload);
  return res.data;
};

export const deleteEndpoint = async (id: string) => {
  const res = await api.delete(`/admin/endpoints/${id}`);
  return res.data;
};