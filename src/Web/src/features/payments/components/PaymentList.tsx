import { Calendar, CreditCard, DollarSign } from 'lucide-react';
import type { Payment } from '../types/payment.types';

interface PaymentListProps {
    payments: Payment[];
    loading: boolean;
}

const PaymentList = ({ payments, loading }: PaymentListProps) => {
    if (loading) {
        return <div className="p-10 text-center text-gray-500">Cargando transacciones...</div>;
    }

    if (payments.length === 0) {
        return (
            <div className="bg-white rounded-xl shadow-sm border border-gray-200 p-12 text-center">
                <div className="bg-gray-50 w-16 h-16 rounded-full flex items-center justify-center mx-auto mb-4">
                    <DollarSign className="text-gray-400" size={32} />
                </div>
                <h3 className="text-lg font-medium text-gray-900">Sin movimientos</h3>
                <p className="text-gray-500">Aún no has realizado pagos con tus tarjetas.</p>
            </div>
        );
    }

    return (
        <div className="bg-white rounded-xl shadow-sm border border-gray-200 overflow-hidden">
            <div className="overflow-x-auto">
                <table className="w-full text-left">
                    <thead className="bg-gray-50 border-b border-gray-200">
                        <tr>
                            <th className="px-6 py-4 text-sm font-semibold text-gray-600">Fecha</th>
                            <th className="px-6 py-4 text-sm font-semibold text-gray-600">Tarjeta</th>
                            <th className="px-6 py-4 text-sm font-semibold text-gray-600 text-right">Monto</th>
                        </tr>
                    </thead>
                    <tbody className="divide-y divide-gray-100">
                        {payments.map((payment) => (
                            <tr key={payment.id} className="hover:bg-gray-50 transition-colors">
                                <td className="px-6 py-4 text-sm text-gray-600">
                                    <div className="flex items-center gap-2">
                                        <Calendar size={14} className="text-gray-400" />
                                        {new Date(payment.date).toLocaleDateString()}
                                    </div>
                                </td>
                                <td className="px-6 py-4 text-sm text-gray-500">
                                    <div className="flex items-center gap-2">
                                        <CreditCard size={14} />
                                        **** {payment.cardId.slice(-4)}
                                    </div>
                                </td>
                                <td className="px-6 py-4 text-right">
                                    <span className="text-sm font-bold text-gray-900">
                                        ${payment.amount.toLocaleString('es-CO')}
                                    </span>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>
        </div>
    );
};

export default PaymentList;