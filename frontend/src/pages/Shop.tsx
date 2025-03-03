import React, { useState } from 'react';
import { useAppContext } from '../contexts/AppContext';
import { Link } from 'react-router-dom';

const Shop = () => {
  const { state, dispatch } = useAppContext();
  const [filters, setFilters] = useState({
    category: '',
    priceRange: [0, 500],
    rating: 0,
  });

  // Filter products based on state
  const filteredProducts = state.products.filter((product) => {
    return (
      (filters.category ? product.name.toLowerCase().includes(filters.category.toLowerCase()) : true) &&
      (product.price >= filters.priceRange[0] && product.price <= filters.priceRange[1]) &&
      (product.rating >= filters.rating)
    );
  });

  // Handle filter changes
  const handleFilterChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
    const { name, value } = e.target;
    setFilters((prev) => ({
      ...prev,
      [name]: name === 'rating' ? parseInt(value) : value,
    }));
  };

  return (
    <div>
      {/* Header */}
      <header>
        <nav>
          <Link to="/">Home</Link> | <Link to="/shop">Shop</Link> | <Link to="/cart">Cart</Link> | <Link to="/profile">Profile</Link>
        </nav>
      </header>

      <h2>Shop</h2>

      {/* Filter Section */}
      <div>
        <h3>Filters</h3>
        <div>
          <label>Category: </label>
          <input
            type="text"
            name="category"
            placeholder="Search by category"
            value={filters.category}
            onChange={handleFilterChange}
          />
        </div>
        <div>
          <label>Price Range: </label>
          <input
            type="range"
            name="priceRange"
            min="0"
            max="500"
            value={filters.priceRange[1]}
            onChange={(e) =>
              setFilters((prev) => ({
                ...prev,
                priceRange: [0, parseInt(e.target.value)],
              }))
            }
          />
          <span>${filters.priceRange[1]}</span>
        </div>
        <div>
          <label>Rating: </label>
          <select name="rating" value={filters.rating} onChange={handleFilterChange}>
            <option value={0}>All</option>
            <option value={3}>3+ Stars</option>
            <option value={4}>4+ Stars</option>
            <option value={5}>5 Stars</option>
          </select>
        </div>
        <button onClick={() => setFilters({ category: '', priceRange: [0, 500], rating: 0 })}>
          Reset Filters
        </button>
      </div>

      {/* Product Grid */}
      <div>
  {filteredProducts.length > 0 ? (
    filteredProducts.map((product) => (
      <div key={product.id}>
        <Link to={`/product/${product.id}`}>
          <img src={product.image} alt={product.name} />
        </Link>
        <h3>
          <Link to={`/product/${product.id}`}>{product.name}</Link>
        </h3>
        <p>${product.price}</p>
        <button onClick={() => dispatch({ type: 'ADD_TO_CART', payload: product })}>
          Add to Cart
        </button>
      </div>
    ))
  ) : (
    <p>No products found</p>
  )}
</div>

      {/* Footer */}
      <footer>
        <p>Contact Us | Terms of Use | Privacy Policy</p>
      </footer>
    </div>
  );
};

export default Shop;
