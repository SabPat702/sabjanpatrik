import React from 'react';
import 'bootstrap/dist/css/bootstrap.min.css'; // Bootstrap import
import '../css/Web.css'; // Egyedi CSS import (az elérési út a projektedtől függ)
import DungeonBook from './DungeonBook'; // DungeonBook komponens importálása

const App = () => {
  const handleLogout = () => {
    alert("Sikeres kijelentkezés!");
    window.location.href = "/";
  };

  return (
    <>
      <div className="header">
        <div className="header-left">
          <a href="#">Dungeon Valley Explorer</a>
        </div>
        <div className="header-right">
          <button id="logoutButton" className="btn btn-danger" onClick={handleLogout}>
            Logout
          </button>
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
         <DungeonBook/>
        </div>
      </div>
    </>
  );
};

export default App;