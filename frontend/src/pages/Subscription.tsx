
import { useAppContext } from '../contexts/AppContext';
import { useNavigate } from 'react-router-dom';

const Subscription = () => {
  const { state, dispatch } = useAppContext();
  const navigate = useNavigate();

  const handleSubscribe = () => {
    if (state.user) {
      dispatch({
        type: 'SET_USER',
        payload: {
          ...state.user,
          isPremium: true,  // Upgrade to Premium
        },
      });
      alert('You have successfully subscribed to Premium!');
      navigate('/profile');
    } else {
      alert('Please log in to subscribe.');
      navigate('/login');
    }
  };

  const handleCancelSubscription = () => {
    if (state.user && state.user.isPremium) {
      dispatch({
        type: 'SET_USER',
        payload: {
          ...state.user,
          isPremium: false,  // Downgrade to Free
        },
      });
      alert('Your subscription has been canceled.');
    }
  };

  return (
    <div>
      <h2>Subscription</h2>
      <p>
        Current Status: {state.user?.isPremium ? 'Premium User' : 'Free User'}
      </p>
      {state.user?.isPremium ? (
        <button onClick={handleCancelSubscription}>Cancel Subscription</button>
      ) : (
        <button onClick={handleSubscribe}>Subscribe to Premium</button>
      )}
    </div>
  );
};

export default Subscription;
