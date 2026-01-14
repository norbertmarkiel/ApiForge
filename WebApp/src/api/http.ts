import axios from "axios";

export const api = axios.create({
  baseURL: "https://localhost:7016",
  headers: {
    "Content-Type": "application/json",
    Accept: "application/json",
  },
});