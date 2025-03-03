import { useState } from 'react';
import { useAppContext } from '../contexts/AppContext';
import { useNavigate, Link } from 'react-router-dom';

const Signup = () => {
    const [username, setUsername] = useState('');
    const [name, setName] = useState('');
    const [password, setPassword] = useState('');
    const { dispatch } = useAppContext();
    const navigate = useNavigate();

    const handleSignup = async () => {
        if (username && name && password) {
            try {
                const response = await fetch('https://your-api-endpoint/signup', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                    },
                    body: JSON.stringify({ username, name, password }),
                });

                if (response.ok) {
                    const data = await response.json();
                    dispatch({
                        type: 'SET_USER',
                        payload: {
                            email: data.username,
                            name: data.name,
                            isPremium: data.isPremium,  // Assuming the API returns this field
                        },
                    });
                    alert('Signup successful!');
                    navigate('/login');
                } else {
                    alert('Signup failed. Please try again.');
                }
            } catch (error) {
                console.error('Error during signup:', error);
                alert('An error occurred. Please try again.');
            }
        } else {
            alert('Please fill all fields.');
        }
    };

    return (
        <div>
            <h2>Sign Up</h2>
            <input
                type="text"
                placeholder="Username"
                value={username}
                onChange={(e) => setUsername(e.target.value)}
            />
            <input
                type="text"
                placeholder="Name"
                value={name}
                onChange={(e) => setName(e.target.value)}
            />
            <input
                type="password"
                placeholder="Password"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
            />
            <button onClick={handleSignup}>Sign Up</button>
            <p>
                Already have an account? <Link to="/login">Log in</Link>
            </p>
        </div>
    );
};

export default Signup;
