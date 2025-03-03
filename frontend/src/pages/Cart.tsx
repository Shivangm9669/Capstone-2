import { Link } from 'react-router-dom';
import { useAppContext } from '../contexts/AppContext'; // Assuming you have a Nav component

const Cart = () => {
    const { state, dispatch } = useAppContext();

    // Calculate total price
    const totalPrice = state.cart.reduce((sum, item) => sum + item.product.price * item.quantity, 0);

    if (state.cart.length === 0) {
        return (
            <div>
                <h2>Your Cart is Empty</h2>
                <Link to="/shop">Go to Shop</Link>
            </div>
        );
    }

    return (
        <div>
            <h2>Your Cart</h2>

            {state.cart.map((item) => (
                <div key={item.id} style={{ borderBottom: '1px solid #ddd', marginBottom: '10px' }}>
                    <h3>{item.product.name}</h3>
                    <img src={item.product.image} alt={item.product.name} style={{ width: '100px' }} />
                    <p>Price: ${item.product.price}</p>
                    <p>
                        Quantity:
                        <button
                            onClick={() =>
                                dispatch({ type: 'UPDATE_CART_ITEM', payload: { ...item, quantity: item.quantity - 1 } })
                            }
                            disabled={item.quantity <= 1}
                        >
                            -
                        </button>
                        {item.quantity}
                        <button
                            onClick={() =>
                                dispatch({ type: 'UPDATE_CART_ITEM', payload: { ...item, quantity: item.quantity + 1 } })
                            }
                        >
                            +
                        </button>
                    </p>
                    <button onClick={() => dispatch({ type: 'REMOVE_FROM_CART', payload: item.id })}>
                        Remove from Cart
                    </button>
                </div>
            ))}

            <h3>Total: ${totalPrice.toFixed(2)}</h3>
            <button>Proceed to Checkout</button>

            <footer>
                <p>Contact Us | Terms of Use | Privacy Policy</p>
            </footer>
        </div>
    );
};

export default Cart;
