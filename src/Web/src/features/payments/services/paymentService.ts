import api from "../../../services/api";
import type { CreatePaymentRequest, Payment } from '../types/payment.types';


export const paymentService = {
  getPayments: async (): Promise<Payment[]> => {
    const response = await api.get<Payment[]>('/payments');
    return response.data;
  },

  pay: async (payment: CreatePaymentRequest): Promise<Payment> => {
    const response = await api.post<Payment>('/payments', payment);
    return response.data;
  }
};