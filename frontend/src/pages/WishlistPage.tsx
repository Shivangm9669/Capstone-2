import React from 'react';
import { useAppContext } from '../contexts/AppContext';
import { Link } from 'react-router-dom';

const WishlistPage: React.FC = () => {
  const { state, dispatch } = useAppContext();
  const { wishlist } = state;

  const handleRemoveFromWishlist = (productId: number) => {
    const product = wishlist.find((item) => item.id === productId);
    if (product) {
      dispatch({
        type: 'TOGGLE_WISHLIST',
        payload: product,
      });
    }
  };

  if (wishlist.length === 0) {
    return <h2>Your Wishlist is empty!</h2>;
  }

  return (
    <div style={{ padding: '20px' }}>
      <h2>Your Wishlist</h2>
      <div style={{ display: 'flex', flexWrap: 'wrap', gap: '20px' }}>
        {wishlist.map((product) => (
          <div
            key={product.id}
            style={{
              border: '1px solid #ddd',
              padding: '10px',
              width: '200px',
              textAlign: 'center',
            }}
          >
            <Link to={`/product/${product.id}`}>
              <img
                src={product.image}
                alt={product.name}
                style={{ width: '100px', height: '100px', objectFit: 'cover' }}
              />
              <h3>{product.name}</h3>
            </Link>
            <p>${product.price.toFixed(2)}</p>
            <p>Rating: {product.rating} ⭐</p>
            <button onClick={() => handleRemoveFromWishlist(product.id)}>
              Remove from Wishlist
            </button>
          </div>
        ))}
      </div>
    </div>
  );
};

export default WishlistPage;
