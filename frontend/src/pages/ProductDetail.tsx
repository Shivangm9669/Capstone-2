
import { useParams, Link } from 'react-router-dom';
import { useAppContext } from '../contexts/AppContext';

const ProductDetail = () => {
  const { id } = useParams<{ id: string }>(); // Get product ID from URL
  const { state, dispatch } = useAppContext();

  // Find the product based on ID
  const product = state.products.find((p) => p.id === parseInt(id || '', 10));

  if (!product) {
    return <p>Product not found!</p>;
  }

  const isInWishlist = state.wishlist.some(item => item.id === product.id);

  const handleWishlistToggle = () => {
    dispatch({
      type: 'TOGGLE_WISHLIST',
      payload: product,
    });
  };

  return (
    <div>
      {/* Header */}
      <header>
        <nav>
          <Link to="/">Home</Link> | <Link to="/shop">Shop</Link> | <Link to="/cart">Cart</Link> | <Link to="/profile">Profile</Link>
        </nav>
      </header>

      {/* Product Details */}
      <h2>{product.name}</h2>
      <div>
        <img src={product.image} alt={product.name} />
        <p>Price: ${product.price}</p>
        <p>Rating: {product.rating}⭐</p>
        <button onClick={() => dispatch({ type: 'ADD_TO_CART', payload: product })}>
          Add to Cart
        </button>
        <button onClick={handleWishlistToggle}>
        {isInWishlist ? 'Remove from Wishlist' : 'Add to Wishlist'}
      </button>
      </div>

      {/* Product Description */}
      <div>
        <h3>Description</h3>
        <p>{product.description || 'No description available.'}</p>
      </div>

      {/* Reviews and Ratings Section */}
      <div>
        <h3>Customer Reviews</h3>
        {product.reviews && product.reviews.length > 0 ? (
          product.reviews.map((review, index) => (
            <div key={index}>
              <p>{review.user}: {review.comment}</p>
              <p>Rating: {review.rating}⭐</p>
            </div>
          ))
        ) : (
          <p>No reviews yet.</p>
        )}
      </div>

      {/* Add Review Form */}
      <div>
        <h3>Leave a Review</h3>
        <form onSubmit={(e) => {
          e.preventDefault();
          const formData = new FormData(e.currentTarget);
          const review = {
            user: 'Anonymous',
            comment: formData.get('comment') as string,
            rating: parseInt(formData.get('rating') as string, 10),
          };
          dispatch({ type: 'ADD_REVIEW', payload: { id: product.id, review: review } });
          e.currentTarget.reset();
        }}>
          <textarea name="comment" placeholder="Write your review..." required />
          <select name="rating" required>
            <option value="">Rating</option>
            <option value="1">1 Star</option>
            <option value="2">2 Stars</option>
            <option value="3">3 Stars</option>
            <option value="4">4 Stars</option>
            <option value="5">5 Stars</option>
          </select>
          <button type="submit">Submit Review</button>
        </form>
      </div>

      {/* Footer */}
      <footer>
        <p>Contact Us | Terms of Use | Privacy Policy</p>
      </footer>
    </div>
  );
};

export default ProductDetail;
