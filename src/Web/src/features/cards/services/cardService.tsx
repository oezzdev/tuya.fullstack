import axios from 'axios';
import type { CreditCard, CreateCardRequest } from '../types/card.types';

const API_URL = 'https://localhost:7034/api/cards';

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

export const cardService = {
  getCards: async (): Promise<CreditCard[]> => {
    const response = await api.get<CreditCard[]>('/');
    return response.data;
  },

  deleteCard: async (id: string): Promise<void> => {
    await api.delete(`/${id}`);
  },

  createCard: async (card: CreateCardRequest): Promise<CreditCard> => {
    const response = await api.post<CreditCard>('/', card);
    return response.data;
  }
};