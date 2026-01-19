import axios from 'axios';
import type { CreatePaymentRequest, Payment } from '../types/payment.types';

const API_URL = 'https://localhost:7034/api/payments';

const api = axios.create({
  baseURL: API_URL,
});

api.interceptors.request.use((config) => {
  const token = localStorage.getItem('user_token');
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

export const paymentService = {
  getPayments: async (): Promise<Payment[]> => {
    const response = await api.get<Payment[]>('/');
    return response.data;
  },

  pay: async (payment: CreatePaymentRequest): Promise<Payment> => {
    const response = await api.post<Payment>('/', payment);
    return response.data;
  }
};