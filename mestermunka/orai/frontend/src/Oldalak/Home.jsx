import React, { useState, useEffect } from 'react';
import 'bootstrap/dist/css/bootstrap.min.css'; // Bootstrap import
import 'bootstrap/dist/js/bootstrap.bundle.min.js'; // Bootstrap JavaScript importálása
import '../css/Web.css'; // Egyedi CSS import
import DungeonBook from './DungeonBook'; // DungeonBook komponens importálása

const Home = () => {
  const [username, setUsername] = useState("Felhasználó"); // Default name if not found
  const [userId, setUserId] = useState(null); // ID kezdetben null
  const [isAccountVisible, setIsAccountVisible] = useState(false);

  // Véletlenszerű ID generálása (csak egyszer)
  const generateRandomId = () => {
    return Math.floor(Math.random() * 1000000); // Generál egy véletlenszerű számot
  };

  // Az ID és a felhasználónév beállítása a localStorage-ból
  useEffect(() => {
    const storedUserId = localStorage.getItem('userId');
    const storedUsername = localStorage.getItem('username');

    if (!storedUserId) {
      const newId = generateRandomId();
      localStorage.setItem('userId', newId);
      setUserId(newId);
    } else {
      setUserId(storedUserId);
    }

    if (storedUsername) {
      setUsername(storedUsername);
    }
  }, []);

  // Fiók törlése
  const handleDeleteAccount = () => {
    const confirmDelete = window.confirm("Biztosan törölni szeretnéd a fiókodat?");
    if (confirmDelete) {
      alert("Fiók sikeresen törölve.");
      setUsername("");
      setUserId(null);
      localStorage.removeItem('userId');
      localStorage.removeItem('username');
    }
  };

  // Kijelentkezés
  const handleLogout = () => {
    alert("Sikeres kijelentkezés!");
    window.location.href = "/";
  };

  // A profil szerkesztése
  const handleEditProfile = () => {
    window.location.href = "/edit-profile";
  };

  // Letöltési funkció
  const handleDownload = () => {
    const content = "Ez egy példa tartalom a DungeonBookból!";
    const blob = new Blob([content], { type: 'text/plain' });
    const url = window.URL.createObjectURL(blob);
    const link = document.createElement('a');
    link.href = url;
    link.download = 'DungeonBook.txt'; // A fájl neve
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
          {/* Letöltési gomb hozzáadása */}
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