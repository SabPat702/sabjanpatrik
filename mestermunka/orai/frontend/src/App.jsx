import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import KezdoLap from './Oldalak/KezdoLap';
import LoginSignup from './Oldalak/LoginSignup';
import Home from './Oldalak/Home'
import ForgotPassword from './Oldalak/ForgotPassword';

const App = () => {
  return (
      <Router>
          <Routes>
              <Route path="/" element={<KezdoLap />} />
              <Route path="/register" element={<LoginSignup />} />
              <Route path="/login" element={<LoginSignup />} />  
              <Route path="/DungeonValleyExplorer" element={<Home/>} />
              <Route path='forgot-password' element={<ForgotPassword/>}/>
          </Routes>
      </Router>
  );
}

export default App
