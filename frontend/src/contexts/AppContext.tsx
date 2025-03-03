import React, { createContext, useContext, useReducer, ReactNode } from 'react';

// Define types
type Review = {
  user: string;
  comment: string;
  rating: number;
};

export type Product = {
  id: number;
  name: string;
  price: number;
  image: string;
  rating: number;
  description?: string;
  reviews?: Review[];
};

type CartItem = {
  id: number;
  product: Product;
  quantity: number;
};

type State = {
  products: Product[];
  cart: CartItem[];
  wishlist: Product[];
  user: {
    email: string;
    name: string;
    isPremium: boolean;
  } | null;
};

type Action =
  | { type: 'SET_PRODUCTS'; payload: Product[] }
  | { type: 'ADD_TO_CART'; payload: Product }
  | { type: 'REMOVE_FROM_CART'; payload: number }
  | { type: 'UPDATE_CART_ITEM'; payload: { id: number; quantity: number } }
  | { type: 'TOGGLE_WISHLIST'; payload: Product }  
  | { type: 'SET_USER'; payload: State['user'] }
  | { type: 'ADD_REVIEW'; payload: { id: number; review: Review } };

// Define initial state
const initialState: State = {
  products: [],
  cart: [],
  wishlist: [],
  user: null,
};

// Reducer function
const reducer = (state: State, action: Action): State => {
  switch (action.type) {
    case 'SET_PRODUCTS':
      return { ...state, products: action.payload };
    case 'ADD_TO_CART': {
      const existingItem = state.cart.find(item => item.product.id === action.payload.id);
      if (existingItem) {
        // If item exists, update quantity
        return {
          ...state,
          cart: state.cart.map(item =>
            item.product.id === action.payload.id
              ? { ...item, quantity: item.quantity + 1 }
              : item
          ),
        };
      }
      // If item doesn't exist, add new item
      return {
        ...state,
        cart: [...state.cart, { id: Date.now(), product: action.payload, quantity: 1 }],
      };
    }
    case 'REMOVE_FROM_CART':
      return { ...state, cart: state.cart.filter(item => item.id !== action.payload) };
    case 'UPDATE_CART_ITEM':
      return {
        ...state,
        cart: state.cart.map(item =>
          item.id === action.payload.id
            ? { ...item, quantity: action.payload.quantity }
            : item
        ),
      };
    case 'TOGGLE_WISHLIST':
      return state.wishlist.some(item => item.id === action.payload.id)
        ? { ...state, wishlist: state.wishlist.filter(item => item.id !== action.payload.id) }
        : { ...state, wishlist: [...state.wishlist, action.payload] };
    case 'SET_USER':
      return { ...state, user: action.payload };
    case 'ADD_REVIEW':
      return {
        ...state,
        products: state.products.map(product =>
          product.id === action.payload.id
            ? { ...product, reviews: [...(product.reviews || []), action.payload.review] }
            : product
        ),
      };
    default:
      console.warn(`Unhandled action type: ${(action as Action).type}`);
      return state;
  }
};
1
// Create context
const AppContext = createContext<{
  state: State;
  dispatch: React.Dispatch<Action>;
}>({ state: initialState, dispatch: () => null });

// Context Provider
export const AppProvider = ({ children }: { children: ReactNode }) => {
  const [state, dispatch] = useReducer(reducer, initialState);
  return <AppContext.Provider value={{ state, dispatch }}>{children}</AppContext.Provider>;
};

// Custom hook to use context
export const useAppContext = () => useContext(AppContext);
