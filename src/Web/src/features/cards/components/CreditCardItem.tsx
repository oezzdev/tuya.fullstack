import { Trash, CreditCard as CardIcon } from 'lucide-react';
import type { CreditCard } from '../types/card.types';

interface CreditCardItemProps {
  card: CreditCard;
  onDelete: (id: string) => void;
}

const CreditCardItem = ({ card, onDelete }: CreditCardItemProps) => {
  const formattedDate = (expirationDate: string) => {
    try {
      const date = new Date(expirationDate);
      const month = String(date.getMonth() + 1).padStart(2, '0');
      const year = String(date.getFullYear()).slice(-2);
      return `${month}/${year}`;
    } catch {
      return "MM/YY";
    }
  };

  return (
    <div className="relative bg-gradient-to-br from-gray-800 to-gray-900 text-white p-6 rounded-xl shadow-lg w-full max-w-sm overflow-hidden">
      <div className="absolute -right-4 -top-4 w-24 h-24 bg-white/10 rounded-full z-0"></div>

      <div className="relative z-10 flex justify-between items-start mb-8">
        <CardIcon size={32} className="text-yellow-500" />

        <button
          onClick={() => onDelete(card.id)}
          className="text-gray-400 hover:text-red-400 transition-colors cursor-pointer p-2"
        >
          <Trash size={20} />
        </button>
      </div>

      <div className="space-y-4">
        <p className="text-xl tracking-widest font-mono">
          **** **** **** {card.number.slice(-4)}
        </p>
        <div className="flex justify-between items-end">
          <div>
            <p className="text-xs text-gray-400 uppercase">Titular</p>
            <p className="font-medium">{card.holderName}</p>
          </div>
          <div className="text-right">
            <p className="text-xs text-gray-400 uppercase">Expira</p>
            <p className="font-medium">{formattedDate(card.expirationDate)}</p>
          </div>
        </div>
      </div>
    </div>
  );
};

export default CreditCardItem;