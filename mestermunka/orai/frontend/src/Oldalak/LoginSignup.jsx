import React, { useEffect, useState } from "react";
import '../css/LoginSignup.css';
import { FaEye, FaEyeSlash } from "react-icons/fa";
import { useLocation, Link } from "react-router-dom";

const LoginSignup = () => {
    const location = useLocation();
    const [action, setAction] = useState(location.pathname === "/Register" ? "register" : "login");

    useEffect(() => {
        setAction(location.pathname === "/Register" ? "register" : "login");
    }, [location.pathname]);

    const [successMessage, setSuccessMessage] = useState('');
    const [errorMessage, setErrorMessage] = useState('');
    const [showPassword, setShowPassword] = useState(false);
    const [showRegisterPassword, setShowRegisterPassword] = useState(false);
    const [showConfirmPassword, setConfirmPassword] = useState(false);
    const [passwordStrength, setPasswordStrength] = useState('');
    const [password, setPassword] = useState('');
    const [username, setUsername] = useState('');
    const [showModal, setShowModal] = useState(false);

    const registerLink = () => setAction('register');
    const loginLink = () => setAction('login');

    const handleRegisterSubmit = (e) => {
        e.preventDefault();
        console.log("Register form submitted");

        const username = e.target.username.value;
        const email = e.target.email.value;
        const password = e.target.password.value;
        const passwordAgain = e.target["password_again"].value;

        if (password !== passwordAgain) {
            setErrorMessage("Passwords do not match!");
            return;
        }

        fetch('http://localhost:3001/signup', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ username, email, password })
        })
            .then(response => response.json())
            .then(data => {
                console.log("Backend response:", data);
                if (data.message === "User successfully created!") {
                    setSuccessMessage("Registration successful! Redirecting...");
                    setUsername(username);
                    setShowModal(true);
                    setTimeout(() => {
                        window.location.href = "/DungeonValleyExplorer";
                    }, 2000);   
                } else {
                    setErrorMessage(`Registration failed! Server says: ${data.message}`);
                }
            })
            .catch(error => {
                console.error('Error during registration:', error);
                setErrorMessage("Error during registration.");
            });
    };

    const handleLoginSubmit = (e) => {
        e.preventDefault();

        const username = e.target.username.value;
        const password = e.target.password.value;

        fetch('http://localhost:3001/login', {
            method: 'POST',  // POST metódust használunk
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ username, password })  // A felhasználónevet és jelszót küldjük a szervernek
        })
        .then(response => response.json())  // Az eredményt JSON formátumban dolgozzuk fel
        .then(data => {
            console.log(data);  // A válasz naplózása
        
            if (data.message === "Login successful!") {
                setSuccessMessage("Login successful! Redirecting...");
                setShowModal(true); // Modal megjelenítése
                setTimeout(() => {
                    window.location.href = "/DungeonValleyExplorer";  // Átirányítás másik oldalra
                }, 2000);
            } else {
                setErrorMessage("Invalid username or password!");  // Hibaüzenet megjelenítése
                console.error("Login failed:", data.message);
            }
        })
        .catch(() => {
            setErrorMessage("Error during login.");  // Hálózati hiba esetén
        });        
    };

    const handlePasswordChange = (e) => {
        const inputPassword = e.target.value;
        setPassword(inputPassword);
        evaluatePasswordStrength(inputPassword);
    };

    const evaluatePasswordStrength = (password) => {
        let score = 0;
        let strength = "";
        let color = '';

        // Length-based scoring
        if (password.length < 6) {
            strength = "Too Short";
            color = "gray";
        } else {
            if (password.length >= 16) score += 4;
            else if (password.length >= 12) score += 3;
            else if (password.length >= 8) score += 2;
            else if (password.length >= 6) score += 1;

            // Character variety
            if (/[A-Z]/.test(password)) score += 2; // Uppercase
            if (/[a-z]/.test(password)) score += 2; // Lowercase
            if (/\d/.test(password)) score += 2; // Numbers
            if (/[^A-Za-z0-9]/.test(password)) score += 2; // Special characters

            // Penalize repetition or predictable patterns
            if (/(.)\1{2,}/.test(password)) score -= 3; // Repeated characters (e.g., "aaa")
            if (/12|123|1234|4321|abcd|qwer/i.test(password)) score -= 4; // Common sequences

            // Common passwords (expanded list)
            const commonPasswords = [
                "password", "123456", "qwerty", "abc123", "letmein", 
                "admin", "welcome", "monkey", "football", "password123"
            ];
            if (commonPasswords.some(common => password.toLowerCase().includes(common))) score -= 5;

            // Evaluate final score
            if (score >= 11) {
                strength = "Strong";
                color = "green";
            } else if (score >= 8) {
                strength = "Medium";
                color = "orange";
            } else {
                strength = "Weak";
                color = "red";
            }
        }

        setPasswordStrength(strength);

        // Update password strength color
        const strengthElement = document.querySelector(".password-strength p");
        if (strengthElement) {
            strengthElement.style.color = color;
        }

        // Debugging: Log the score for testing
        console.log(`Password: ${password}, Score: ${score}, Strength: ${strength}`);
    };

    return (
        <div className="login-signup">
            <div className={`wrapper ${action}`}>    
                <div className={`form-box login ${action === 'login' ? 'show' : ''}`}>
                    <form onSubmit={handleLoginSubmit}>
                        <h1>Login</h1>
                        <div className="input-box">
                            <input type="text" name="username" placeholder="Username" required />
                        </div>
                        <div className="input-box password-box">
                            <input type={showPassword ? "text" : "password"} name="password" placeholder="Password" required />
                            <span className="toggle-icon" onClick={() => setShowPassword(!showPassword)}>
                                {showPassword ? <FaEyeSlash /> : <FaEye />}
                            </span>
                        </div>
                        <button type="submit">Login</button>
                        {errorMessage && <p style={{ color: 'red' }}>{errorMessage}</p>}
                        <div className="register-link">
                            <p>Don't have an account? <a href="#" onClick={registerLink}>Register</a></p>
                        </div>   
                    </form>
                    {showModal && (
                        <div className="modal-overlay">
                            <div className="modal-content">
                                <h2>Dear {username}, Welcome back!</h2>
                                <button onClick={() => setShowModal(false)}>Close</button>
                            </div>
                        </div>
                    )}
                    {successMessage && (
                        <div className="success-message">
                            <p>{successMessage}</p>
                        </div>
                    )}
                </div>

                <div className={`form-box register ${action === 'register' ? 'show' : ''}`}>
                    <form onSubmit={handleRegisterSubmit}>
                        <h1>Registration</h1>
                        <div className="input-box">
                            <input type="text" name="username" placeholder="Username" required />
                        </div>
                        <div className="input-box">
                            <input type="email" name="email" placeholder="Email" required />
                        </div>
                        <div className="input-box password-box">
                            <input 
                                type={showRegisterPassword ? "text" : "password"} 
                                name="password" 
                                placeholder="Password" 
                                value={password} 
                                onChange={handlePasswordChange} 
                                required 
                            />
                            <span className="toggle-icon" onClick={() => setShowRegisterPassword(!showRegisterPassword)}>
                                {showRegisterPassword ? <FaEyeSlash /> : <FaEye />}
                            </span>
                        </div>
                        <div className="password-strength">
                            {password && <p>{`Password strength: ${passwordStrength}`}</p>}
                        </div>
                        <div className="input-box password-box">
                            <input 
                                type={showConfirmPassword ? "text" : "password"} 
                                name="password_again" 
                                placeholder="Password again" 
                                required 
                            />
                            <span className="toggle-icon" onClick={() => setConfirmPassword(!showConfirmPassword)}>
                                {showConfirmPassword ? <FaEyeSlash /> : <FaEye />}
                            </span>
                        </div>
                        <button type="submit">Register</button>
                        <div className="register-link">
                            <p>Already have an account? <a href="#" onClick={loginLink}>Login</a></p>
                        </div>
                    </form>
                    {showModal && (
                        <div className="modal-overlay">
                            <div className="modal-content">
                                <h2>Dear {username}, thank you for registering!</h2>
                                <button onClick={() => setShowModal(false)}>Close</button>
                            </div>
                        </div>
                    )}
                </div>
            </div>
        </div>
    );
};

export default LoginSignup;