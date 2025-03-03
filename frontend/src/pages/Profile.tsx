import { useAppContext } from '../contexts/AppContext';
import { useNavigate, Link } from 'react-router-dom';

const Profile = () => {
    const { state, dispatch } = useAppContext();
    const navigate = useNavigate();

    const handleLogout = () => {
        dispatch({ type: 'SET_USER', payload: null });
        alert('You have been logged out!');
        navigate('/login');
    };

    if (!state.user) {
        return (
            <div>
                <h2>You are not logged in!</h2>
                <Link to="/login">Go to Login</Link>
            </div>
        );
    }

    return (
        <div>
            <h2>Profile</h2>
            <p>Username: {state.user.name}</p>
            <p>Status: {state.user.isPremium ? 'Premium User' : 'Free User'}</p>
            {!state.user.isPremium && (
                <Link to="/subscription">
                    <button style={{ marginTop: '10px' }}>Subscribe Now</button>
                </Link>
            )}
            <button onClick={handleLogout}>Logout</button>
        </div>
    );
};

export default Profile;
