export interface Payment {
    id: string;
    cardId: string;
    amount: number;
    date: string;
}

export interface CreatePaymentRequest {
    cardId: string;
    amount: number;
}