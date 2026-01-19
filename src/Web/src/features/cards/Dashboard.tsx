import { useState, useEffect } from 'react';
import CreditCardItem from './components/CreditCardItem';
import { Plus, History, CreditCard as CardIcon, DollarSign } from 'lucide-react';
import type { CreateCardRequest, CreditCard } from './types/card.types';
import { cardService } from './services/cardService';
import { paymentService } from '../payments/services/paymentService';
import CardModal from './components/CardModal';
import type { CreatePaymentRequest, Payment } from '../payments/types/payment.types';
import PaymentList from '../payments/components/PaymentList';
import PaymentModal from '../payments/components/PaymentModal';

const Dashboard = () => {
    const [cards, setCards] = useState<CreditCard[]>([]);
    const [loading, setLoading] = useState<boolean>(true);
    const [_error, setError] = useState<string | null>(null);
    const [view, setView] = useState<string>('cards');
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [isPaymentModalOpen, setIsPaymentModalOpen] = useState(false);
    const [payments, setPayments] = useState<Payment[]>([]);

    useEffect(() => {
        loadCards();
    }, []);

    useEffect(() => {
        if (view === 'history') {
            loadPayments();
        }
    }, [view]);

    const loadCards = async () => {
        try {
            setLoading(true);
            const data = await cardService.getCards();
            setCards(data);
        } catch (err) {
            setError('No se pudieron cargar las tarjetas. Intente más tarde.');
        } finally {
            setLoading(false);
        }
    };

    const loadPayments = async () => {
        try {
            setLoading(true);
            const data = await paymentService.getPayments();
            setPayments(data);
        } catch (err) {
            setError('Error al cargar el historial.');
        } finally {
            setLoading(false);
        }
    };

    const handleDelete = async (id: string) => {
        if (window.confirm('¿Estás seguro de eliminar esta tarjeta?')) {
            try {
                await cardService.deleteCard(id);
                setCards(prev => prev.filter(c => c.id !== id));
            } catch (err) {
                alert('Error al eliminar la tarjeta');
            }
        }
    };

    const handleAddCard = async (newCardData: CreateCardRequest) => {
        try {
            setLoading(true);
            const newCard = await cardService.createCard(newCardData);
            setCards(prev => [...prev, newCard]);
            setIsModalOpen(false);
        } catch (err) {
            alert('Error al guardar la tarjeta');
        } finally {
            setLoading(false);
        }
    };

    const handleProcessPayment = async (paymentData: CreatePaymentRequest) => {
        try {
            setLoading(true);
            await paymentService.pay(paymentData);
            await loadPayments();
            setIsPaymentModalOpen(false);
        } catch (err) {
            alert('Error al procesar el pago. Verifique su cupo disponible.');
        } finally {
            setLoading(false);
        }
    };

    return (
        <div className="min-h-screen bg-gray-50 flex">
            <nav className="w-64 bg-white border-r border-gray-200 p-6 space-y-4">
                <div className="flex items-center gap-2 mb-10 text-blue-600 font-bold text-xl">
                    <CardIcon /> <span>Tuya App</span>
                </div>
                <button
                    onClick={() => setView('cards')}
                    className={`w-full flex items-center gap-3 px-4 py-2 rounded-lg transition-colors ${view === 'cards' ? 'bg-blue-50 text-blue-600' : 'text-gray-600 hover:bg-gray-100'}`}
                >
                    <CardIcon size={20} /> Mis Tarjetas
                </button>
                <button
                    onClick={() => setView('history')}
                    className={`w-full flex items-center gap-3 px-4 py-2 rounded-lg transition-colors ${view === 'history' ? 'bg-blue-50 text-blue-600' : 'text-gray-600 hover:bg-gray-100'}`}
                >
                    <History size={20} /> Pagos
                </button>
            </nav>

            <main className="flex-1 p-10">
                <header className="flex justify-between items-center mb-8">
                    <h1 className="text-2xl font-bold text-gray-800">
                        {view === 'cards' ? 'Mis Tarjetas' : 'Historial de Pagos'}
                    </h1>

                    {view === 'cards' ? (
                        <button
                            onClick={() => setIsModalOpen(true)}
                            className="bg-blue-600 hover:bg-blue-700 text-white px-4 py-2 rounded-lg flex items-center gap-2 transition-all shadow-md"
                        >
                            <Plus size={20} /> Agregar Tarjeta
                        </button>
                    ) : (
                        <button
                            onClick={() => setIsPaymentModalOpen(true)}
                            className="bg-green-600 hover:bg-green-700 text-white px-4 py-2 rounded-lg flex items-center gap-2 transition-all shadow-md"
                        >
                            <DollarSign size={20} /> Nuevo Pago
                        </button>
                    )}
                </header>

                {view === 'cards' ? (
                    <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
                        {cards.map(card => (
                            <CreditCardItem key={card.id} card={card} onDelete={handleDelete} />
                        ))}
                    </div>
                ) : (
                    <div className="bg-white rounded-xl shadow-sm border border-gray-200 overflow-hidden">
                        <PaymentList payments={payments} loading={loading} />
                    </div>
                )}

                {isModalOpen && (
                    <CardModal
                        onClose={() => setIsModalOpen(false)}
                        onSubmit={handleAddCard}
                    />
                )}

                {isPaymentModalOpen && (
                    <PaymentModal
                        cards={cards}
                        onClose={() => setIsPaymentModalOpen(false)}
                        onSubmit={handleProcessPayment}
                    />
                )}
            </main>
        </div>
    );
};

export default Dashboard;