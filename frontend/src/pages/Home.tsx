import { useEffect } from 'react';
import { useAppContext } from '../contexts/AppContext';
import { Link } from 'react-router-dom';

const Home = () => {
  const { state, dispatch } = useAppContext();

  // Simulate fetching products from an API
  useEffect(() => {
    const fetchedProducts = [
      { id: 1, name: 'Product 1', price: 100, image: '/images/product1.jpg', rating: 4 },
      { id: 2, name: 'Product 2', price: 150, image: '/images/product2.jpg', rating: 5 },
      { id: 3, name: 'Product 3', price: 200, image: '/images/product3.jpg', rating: 3 },
    ];
    dispatch({ type: 'SET_PRODUCTS', payload: fetchedProducts });
  }, [dispatch]);

  return (
    <div>
      {state.user?.isPremium && <div>Welcome Premium User! Enjoy exclusive deals!</div>}

      <h2>Featured Products</h2>
      <div>
        {state.products.map((product) => (
          <div key={product.id}>
            <img src={product.image} alt={product.name} />
            <h3>{product.name}</h3>
            <p>${product.price}</p>
            <button onClick={() => dispatch({ type: 'ADD_TO_CART', payload: product })}>
              Add to Cart
            </button>
          </div>
        ))}
      </div>

      {/* Trending Now Section */}
      <h2>Trending Now</h2>
      <div>
        {state.products.map((product) => (
          <div key={product.id}>
            <img src={product.image} alt={product.name} />
            <h3>{product.name}</h3>
            <p>${product.price}</p>
            <button onClick={() => dispatch({ type: 'ADD_TO_CART', payload: product })}>
              Add to Cart
            </button>
          </div>
        ))}
      </div>

      {/* Footer */}
      <footer>
        <p>Contact Us | Terms of Use | Privacy Policy</p>
      </footer>
    </div>
  );
};

export default Home;
