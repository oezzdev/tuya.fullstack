export interface CreditCard {
    id: string;
    userId: string;
    number: string;
    holderName: string;
    expirationDate: string;
    balance: number;
}

export interface CreateCardRequest {
    number: string;
    holderName: string;
    expirationDate: string;
}