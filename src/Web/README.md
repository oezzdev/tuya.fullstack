## Estructura del Frontend

Este proyecto es un frontend de administración de tarjetas de crédito y pagos desarrollado con React, TypeScript y Vite.

### Carpetas principales

- **`src/features/`** - Módulos principales de la aplicación
  - **`auth/`** - Módulo de autenticación
    - `Login.tsx` - Componente de login
    - `components/` - ProtectedRoute para rutas protegidas
    - `services/` - authService para lógica de autenticación
  
  - **`cards/`** - Módulo de gestión de tarjetas de crédito
    - `Dashboard.tsx` - Vista principal de tarjetas
    - `components/` - CardModal (modal para crear/editar tarjetas), CreditCardItem (componente individual de tarjeta)
    - `services/` - cardService para operaciones CRUD
    - `types/` - Definiciones TypeScript de tarjetas
  
  - **`payments/`** - Módulo de pagos
    - `components/` - PaymentList (lista de pagos), PaymentModal (modal de pagos)
    - `services/` - paymentService para operaciones de pagos
    - `types/` - Definiciones TypeScript de pagos

- **`src/services/`** - Servicios compartidos
  - `api.ts` - Configuración de cliente HTTP para comunicación con el backend

### Archivos principales

- **`main.tsx`** - Punto de entrada de la aplicación
- **`App.tsx`** - Componente raíz
- **`index.css` y `App.css`** - Estilos globales y de la aplicación

### Tecnologías

- **React 18** - Librería UI
- **TypeScript** - Tipado estático
- **Vite** - Build tool y dev server
- **ESLint** - Linting de código
