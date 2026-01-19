import { useState } from 'react';
import { X } from 'lucide-react';
import type { CreditCard as ICreditCard } from '../../cards/types/card.types';

interface PaymentModalProps {
    cards: ICreditCard[];
    onClose: () => void;
    onSubmit: (paymentData: any) => Promise<void>;
}

const PaymentModal = ({ cards, onClose, onSubmit }: PaymentModalProps) => {
    const [formData, setFormData] = useState({
        cardId: '',
        amount: '',
        description: ''
    });
    const [isSubmitting, setIsSubmitting] = useState(false);

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        setIsSubmitting(true);
        try {
            await onSubmit({
                ...formData,
                amount: parseFloat(formData.amount)
            });
        } finally {
            setIsSubmitting(false);
        }
    };

    return (
        <div className="fixed inset-0 bg-black/50 backdrop-blur-sm flex items-center justify-center z-50 p-4">
            <div className="bg-white rounded-2xl shadow-xl w-full max-w-md overflow-hidden animate-in fade-in zoom-in duration-200">
                <div className="flex justify-between items-center p-6 border-b border-gray-100">
                    <h2 className="text-xl font-bold text-gray-800">Realizar Nuevo Pago</h2>
                    <button onClick={onClose} className="text-gray-400 hover:text-gray-600 transition-colors">
                        <X size={24} />
                    </button>
                </div>

                <form onSubmit={handleSubmit} className="p-6 space-y-4">
                    <div>
                        <label className="block text-sm font-medium text-gray-700 mb-1">Seleccionar Tarjeta</label>
                        <select
                            required
                            className="w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-green-500 focus:border-transparent outline-none transition-all"
                            value={formData.cardId}
                            onChange={(e) => setFormData({ ...formData, cardId: e.target.value })}
                        >
                            <option value="">Selecciona una tarjeta...</option>
                            {cards.map(card => (
                                <option key={card.id} value={card.id}>
                                    {card.holderName} - **** {card.number.slice(-4)}
                                </option>
                            ))}
                        </select>
                    </div>

                    <div>
                        <label className="block text-sm font-medium text-gray-700 mb-1">Monto del Pago</label>
                        <div className="relative">
                            <span className="absolute left-3 top-2.5 text-gray-400">$</span>
                            <input
                                type="number"
                                required
                                min="1"
                                placeholder="0.00"
                                className="w-full pl-8 pr-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-green-500 outline-none transition-all"
                                value={formData.amount}
                                onChange={(e) => setFormData({ ...formData, amount: e.target.value })}
                            />
                        </div>
                    </div>

                    <div>
                        <label className="block text-sm font-medium text-gray-700 mb-1">Concepto o Descripción</label>
                        <input
                            type="text"
                            required
                            placeholder="Ej. Pago de servicios públicos"
                            className="w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-green-500 outline-none transition-all"
                            value={formData.description}
                            onChange={(e) => setFormData({ ...formData, description: e.target.value })}
                        />
                    </div>

                    <div className="flex gap-3 mt-8">
                        <button
                            type="button"
                            onClick={onClose}
                            className="flex-1 px-4 py-2 text-gray-700 bg-gray-100 hover:bg-gray-200 rounded-lg font-medium transition-colors"
                        >
                            Cancelar
                        </button>
                        <button
                            type="submit"
                            disabled={isSubmitting}
                            className="flex-1 px-4 py-2 bg-green-600 hover:bg-green-700 text-white rounded-lg font-medium shadow-lg shadow-green-200 transition-all disabled:opacity-50"
                        >
                            {isSubmitting ? 'Procesando...' : 'Confirmar Pago'}
                        </button>
                    </div>
                </form>
            </div>
        </div>
    );
};

export default PaymentModal;