import React, { useState, useEffect } from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap/dist/js/bootstrap.bundle.min.js';
import { useNavigate } from 'react-router-dom'; // React Router navigáláshoz
import '../css/Web.css';
import DungeonBook from './DungeonBook';

const Home = () => {
    const [username, setUsername] = useState('');
    const [userId, setUserId] = useState(null);
    const [isAccountVisible, setIsAccountVisible] = useState(false);
    const navigate = useNavigate(); // Hook a navigáláshoz

    const generateRandomId = () => {
        return Math.floor(Math.random() * 1000000);
    };

    useEffect(() => {
        const storedUserId = localStorage.getItem('userId');
        const storedUsername = localStorage.getItem('username');

        console.log('Home - Betöltött username:', storedUsername, 'Betöltött userId:', storedUserId);

        if (!storedUserId) {
            const newId = generateRandomId();
            localStorage.setItem('userId', newId);
            setUserId(newId);
        } else {
            setUserId(storedUserId);
        }

        if (storedUsername && storedUsername.trim() !== '') {
            setUsername(storedUsername);
        } else {
            setUsername('');
            localStorage.removeItem('username'); // Tisztítjuk a localStorage-t
        }
    }, []);

    const handleDeleteAccount = async () => {
        console.log('handleDeleteAccount - Jelenlegi username:', username);
        if (!username || username.trim() === '') {
            alert('Nincs érvényes felhasználónév a törléshez!');
            console.error('Nincs érvényes felhasználónév:', username);
            return;
        }

        const confirmDelete = window.confirm(`Biztosan törölni szeretnéd a "${username}" fiókot?`);
        if (!confirmDelete) {
            return;
        }

        try {
            console.log('DELETE kérés URL:', `http://localhost:3001/users/${encodeURIComponent(username)}`);
            const response = await fetch(`http://localhost:3001/users/${encodeURIComponent(username)}`, {
                method: 'DELETE',
                headers: {
                    'Content-Type': 'application/json',
                },
            });

            const data = await response.json();
            console.log('Backend válasz:', data);

            if (!response.ok) {
                if (response.status === 404) {
                    throw new Error('Felhasználó nem található.');
                } else {
                    throw new Error(data.message || 'Hiba történt a fiók törlése közben.');
                }
            }

            alert('Fiók sikeresen törölve!');
            localStorage.removeItem('userId');
            localStorage.removeItem('username');
            setUsername('');
            setUserId(null);
            window.location.href = "/Login";
        } catch (error) {
            console.error('Hiba a fiók törlése közben:', error);
            alert(`Hiba történt: ${error.message}`);
        }
    };

    const handleLogout = () => {
        alert('Sikeres kijelentkezés!');
        localStorage.removeItem('userId');
        localStorage.removeItem('username');
        setUsername('');
        setUserId(null);
        window.location.href = "/Login";
    };

    const handleEditProfile = () => {
        window.location.href = "/edit-profile";
    };

    const handleDownload = () => {
        const content = "Ez egy példa tartalom a DungeonBookból!";
        const blob = new Blob([content], { type: 'text/plain' });
        const url = window.URL.createObjectURL(blob);
        const link = document.createElement('a');
        link.href = url;
        link.download = 'DungeonBook.txt';
        document.body.appendChild(link);
        link.click();
        document.body.removeChild(link);
        window.URL.revokeObjectURL(url);
    };

    // Handle chat functionality - navigate to Chat page
    const handleChat = () => {
        navigate('/chat'); // Navigálás a /chat oldalra
    };

    return (
        <>
            <div className="header">
                <div className="header-left">
                    <a href="#">Dungeon Valley Explorer</a>
                </div>
                <div className="header-right d-flex">
                    <div className="dropdown">
                        <button
                            className="btn btn-secondary dropdown-toggle"
                            type="button"
                            id="userMenuButton"
                            data-bs-toggle="dropdown"
                            data-bs-placement="bottom-end"
                            data-bs-offset="0,5"
                            aria-expanded="false"
                        >
                            <i className="bi bi-person-circle"></i>
                        </button>
                        <ul className="dropdown-menu" aria-labelledby="userMenuButton">
                            <li>
                                <a className="dropdown-item" href="#">
                                    <strong>{username || 'Felhasználó'}</strong> <br />
                                    Felhasználó ID: {userId || 'N/A'}
                                </a>
                            </li>
                            {username && username.trim() !== '' && (
                                <>
                                    <li>
                                        <a className="dropdown-item" href="#" onClick={handleEditProfile}>
                                            Profil szerkesztése
                                        </a>
                                    </li>
                                    <li>
                                    <a className="dropdown-item" href="#" onClick={handleChat}>
                                            Chat
                                        </a>
                                    </li>
                                    <li>
                                        <a className="dropdown-item" href="#" onClick={handleDeleteAccount}>
                                            Fiók törlése
                                        </a>
                                    </li>
                                </>
                            )}
                            <li>
                                <a className="dropdown-item" href="#" onClick={handleLogout}>
                                    Kijelentkezés
                                </a>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>

            <div className="castle-background">
                <img src="/AiSlop.jpg" alt="Kastély" id="castle" />
            </div>

            <div className="main">
                <h1>Üdvözöllek a Dungeon Valley Explorerben</h1>
                <h2>Itt kezdődik az utazásod.</h2>
                <br />
                <br />
                <div className="row">
                    <div className="download-section">
                        <button className="download-btn" onClick={handleDownload}>
                            Dungeon Valley Explorer letöltése
                        </button>
                    </div>
                    <DungeonBook />
                </div>
            </div>
        </>
    );
};

export default Home;
