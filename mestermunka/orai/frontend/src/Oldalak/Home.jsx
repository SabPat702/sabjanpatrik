import React, { useState, useEffect } from 'react';
import 'bootstrap/dist/css/bootstrap.min.css'; // Bootstrap import
import 'bootstrap/dist/js/bootstrap.bundle.min.js'; // Bootstrap JavaScript importálása
import '../css/Web.css'; // Egyedi CSS import (az elérési út a projektedtől függ)
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
    const storedUsername = localStorage.getItem('username'); // Retrieve username from localStorage

    if (!storedUserId) {
      const newId = generateRandomId();
      localStorage.setItem('userId', newId);
      setUserId(newId);
    } else {
      setUserId(storedUserId);
    }

    if (storedUsername) {
      setUsername(storedUsername); // Set username from localStorage
    }
  }, []);

  // Fiók törlése
  const handleDeleteAccount = () => {
    const confirmDelete = window.confirm("Biztosan törölni szeretnéd a fiókodat?");
    if (confirmDelete) {
      alert("Fiók sikeresen törölve.");
      setUsername(""); // Üres név, jelezve, hogy törölték a fiókot
      setUserId(null); // Üres ID
      localStorage.removeItem('userId'); // Tárolt ID törlése
      localStorage.removeItem('username'); // Remove stored username
    }
  };

  // Kijelentkezés
  const handleLogout = () => {
    alert("Sikeres kijelentkezés!");
    window.location.href = "/"; // Redirect to home page after logout
  };

  // A profil szerkesztése
  const handleEditProfile = () => {
    window.location.href = "/edit-profile"; // Például egy másik oldalra navigálás
  };

  return (
    <>
      <div className="header">
        <div className="header-left">
          <a href="#">Dungeon Valley Explorer</a>
        </div>
        <div className="header-right d-flex"> {/* d-flex: Flexbox, hogy egymás mellett legyenek a gombok */}
          {/* Felhasználói ikon menü */}
          <div className="dropdown">
            <button className="btn btn-secondary dropdown-toggle" type="button" id="userMenuButton" data-bs-toggle="dropdown" aria-expanded="false">
              <i className="bi bi-person-circle"></i> {/* Bootstrap ikon a felhasználóhoz */}
            </button>
            <ul className="dropdown-menu" aria-labelledby="userMenuButton">
              <li>
                <a className="dropdown-item" href="#">
                  <strong>{username}</strong> <br />
                  User ID: {userId}
                </a>
              </li>
              <li><a className="dropdown-item" href="#" onClick={handleEditProfile}>Edit Profile</a></li>
              <li><a className="dropdown-item" href="#" onClick={handleLogout}>Log Out</a></li>
              <li><a className="dropdown-item" href="#" onClick={handleDeleteAccount}>Delete Account</a></li>
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
          <DungeonBook />
        </div>
      </div>
    </>
  );
};

export default Home;
