import React, { useState, useEffect } from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap/dist/js/bootstrap.bundle.min.js';
import '../css/Web.css';
import DungeonBook from './DungeonBook';

const Home = () => {
    const [username, setUsername] = useState("");
    const [userId, setUserId] = useState(null);
    const [isAccountVisible, setIsAccountVisible] = useState(false);

    const generateRandomId = () => {
        return Math.floor(Math.random() * 1000000);
    };

    useEffect(() => {
        const storedUserId = localStorage.getItem('userId');
        const storedUsername = localStorage.getItem('username');

        console.log('Betöltött username:', storedUsername);

        if (!storedUserId) {
            const newId = generateRandomId();
            localStorage.setItem('userId', newId);
            setUserId(newId);
        } else {
            setUserId(storedUserId);
        }

        if (storedUsername && storedUsername !== "Vendég") {
            setUsername(storedUsername);
        } else {
            setUsername("Vendég");
            localStorage.setItem('username', "Vendég"); // Biztosítjuk a konzisztenciát
        }
    }, []);

    const handleDeleteAccount = async () => {
        console.log('handleDeleteAccount called with username:', username); // Hibakeresés
        const confirmDelete = window.confirm("Biztosan törölni szeretnéd a fiókodat?");
        if (confirmDelete) {
            try {
                console.log('Törlési kérés username:', username);
                if (!username || username === "Vendég") {
                    throw new Error('Nincs érvényes felhasználónév a törléshez.');
                }
                console.log('DELETE URL:', `http://localhost:3001/users/${encodeURIComponent(username)}`);
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

                alert("Fiók sikeresen törölve.");
                localStorage.removeItem('userId');
                localStorage.removeItem('username');
                setUsername("");
                setUserId(null);
                window.location.href = "/";
            } catch (error) {
                console.error('Hiba a fiók törlése közben:', error);
                alert(`Hiba történt: ${error.message}`);
            }
        }
    };

    const handleLogout = () => {
        alert("Sikeres kijelentkezés!");
        localStorage.removeItem('userId');
        localStorage.removeItem('username');
        window.location.href = "/";
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
                                    <strong>{username}</strong> <br />
                                    User ID: {userId}
                                </a>
                            </li>
                            <li>
                                <a className="dropdown-item" href="#" onClick={handleEditProfile}>
                                    Edit Profile
                                </a>
                            </li>
                            <li>
                                <a className="dropdown-item" href="#" onClick={handleLogout}>
                                    Log Out
                                </a>
                            </li>
                            <li>
                                <a className="dropdown-item" href="#" onClick={handleDeleteAccount}>
                                    Delete Account
                                </a>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>

            <div className="castle-background">
                <img src="/AiSlop.jpg" alt="Castle" id="castle" />
            </div>

            <div className="main">
                <h1>Welcome to Dungeon Valley Explorer</h1>
                <h2>Your journey begins here.</h2>
                <br />
                <br />
                <div className="row">
                    <div className="download-section">
                        <button className="download-btn" onClick={handleDownload}>
                            Download Dungeon Valley Explorer
                        </button>
                    </div>
                    <DungeonBook />
                </div>
            </div>
        </>
    );
};

export default Home;