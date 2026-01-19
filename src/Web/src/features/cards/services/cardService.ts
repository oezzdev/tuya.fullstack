import api from '../../../services/api';
import type { CreditCard, CreateCardRequest } from '../types/card.types';

export const cardService = {
  getCards: async (): Promise<CreditCard[]> => {
    const response = await api.get<CreditCard[]>('/cards');
    return response.data;
  },

  deleteCard: async (id: string): Promise<void> => {
    await api.delete(`/cards/${id}`);
  },

  createCard: async (card: CreateCardRequest): Promise<CreditCard> => {
    const response = await api.post<CreditCard>('/cards', card);
    return response.data;
  }
};