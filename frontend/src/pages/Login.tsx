import { useState } from 'react';
import { useAppContext } from '../contexts/AppContext';
import { useNavigate, Link } from 'react-router-dom';

const Login = () => {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const { dispatch } = useAppContext();
    const navigate = useNavigate();

    const handleLogin = async () => {
        if (email && password) {
            try {
                // Simulate API login
                const response = await fetch('/api/login', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                    },
                    body: JSON.stringify({ name, email, password }),
                });

                if (response.ok) {
                    const data = await response.json();
                    dispatch({
                        type: 'SET_USER',
                        payload: {
                            name: data.name,
                            email: data.email,
                            isPremium: data.isPremium,
                        },
                    });
                    alert('Login successful!');
                    navigate('/profile');
                } else {
                    alert('Login failed. Please check your credentials.');
                }
            } catch (error) {
                alert('An error occurred. Please try again.');
            }
        } else {
            alert('Please enter your email, and password.');
        }
    };

    return (
        <div>
            <h2>Login</h2>
            <input
                type="email"
                placeholder="Email"
                value={email}
                onChange={(e) => setEmail(e.target.value)}
            />
            <input
                type="password"
                placeholder="Password"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
            />
            <button onClick={handleLogin}>Login</button>
            <p>
                Don't have an account? <Link to="/signup">Sign up</Link>
            </p>
        </div>
    );
};

export default Login;
