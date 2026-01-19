import React, { useState } from 'react';
import { X } from 'lucide-react';

interface CardModalProps {
    onClose: () => void;
    onSubmit: (data: any) => void;
}

const CardModal = ({ onClose, onSubmit }: CardModalProps) => {
    const [formData, setFormData] = useState({
        number: '',
        holderName: '',
        expirationDate: '',
    });

    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();
        onSubmit(formData);
    };

    return (
        <div className="fixed inset-0 bg-black/50 flex items-center justify-center z-50 p-4">
            <div className="bg-white rounded-2xl w-full max-w-md p-6 shadow-2xl">
                <div className="flex justify-between items-center mb-6">
                    <h2 className="text-xl font-bold text-gray-800">Nueva Tarjeta</h2>
                    <button onClick={onClose} className="text-gray-400 hover:text-gray-600">
                        <X size={24} />
                    </button>
                </div>

                <form onSubmit={handleSubmit} className="space-y-4">
                    <div>
                        <label className="block text-sm font-medium text-gray-700 mb-1">Número de Tarjeta</label>
                        <input
                            required
                            className="w-full border border-gray-300 rounded-lg p-2.5 focus:ring-2 focus:ring-blue-500 outline-none"
                            placeholder="0000 0000 0000 0000"
                            onChange={(e) => setFormData({...formData, number: e.target.value})}
                        />
                    </div>
                    <div>
                        <label className="block text-sm font-medium text-gray-700 mb-1">Nombre del Titular</label>
                        <input
                            required
                            className="w-full border border-gray-300 rounded-lg p-2.5 focus:ring-2 focus:ring-blue-500 outline-none"
                            placeholder="EJ. JUAN PEREZ"
                            onChange={(e) => setFormData({...formData, holderName: e.target.value})}
                        />
                    </div>
                    <div className="grid grid-cols-2 gap-4">
                        <div>
                            <label className="block text-sm font-medium text-gray-700 mb-1">Vencimiento</label>
                            <input
                                required
                                type='date'
                                placeholder="MM/YY"
                                className="w-full border border-gray-300 rounded-lg p-2.5 focus:ring-2 focus:ring-blue-500 outline-none"
                                onChange={(e) => setFormData({...formData, expirationDate: e.target.value})}
                            />
                        </div>
                    </div>
                    <button
                        type="submit"
                        className="w-full bg-blue-600 text-white py-3 rounded-lg font-semibold mt-4 hover:bg-blue-700 transition-colors"
                    >
                        Guardar Tarjeta
                    </button>
                </form>
            </div>
        </div>
    );
};

export default CardModal;