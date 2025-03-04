import { Link } from "react-router-dom";

const Nav = () => (
  <nav>
    <Link to="/">Home</Link> | <Link to="/wishlist">Wishlist</Link> |{' '}
    <Link to="/profile">Profile</Link> | <Link to="/subscription">Subscription</Link> |{' '}
    <Link to="/order-tracking">Order Tracking</Link>
  </nav>
);

export default Nav;