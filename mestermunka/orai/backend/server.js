import express from 'express';
import cors from 'cors';
import mysql2 from 'mysql2';
import bodyParser from 'body-parser';
import bcrypt from 'bcrypt';
/*
import nodemailer from 'nodemailer';
import dotenv from 'dotenv';
dotenv.config();  // Betölti az .env fájlt
*/
const app = express();
const saltRounds = 5;
app.use(bodyParser.json());
app.use(cors());

/*
const transporter = nodemailer.createTransport({
  service: 'gmail',
  auth: {
    user: process.env.EMAIL_USER, 
    pass: process.env.EMAIL_PASS, 
  },
});

  // Test Nodemailer setup
  transporter.verify((error, success) => {
    if (error) {
      console.error('Nodemailer configuration error:', error);
    } else {
      console.log('Nodemailer is ready to send emails', success);
    }
  });
  const db = mysql2.createConnection({
  user: process.env.DB_USER,
  host: process.env.DB_HOST,
  port: process.env.DB_PORT,
  password: process.env.DB_PASSWORD,
  database: process.env.DB_NAME,
});
*/


app.get("/", (req, res) => {
    res.send("Fut a backend!");
});

const db = mysql2.createConnection({
  user: "sabpat702",
  host: "10.3.1.65",
  password: "72587413702",
  database:"sabpat702",
  port: 3306
})


db.connect(err => {
    if (err) {
        console.error('Database connection failed:', err);
    } else {
        console.log('Connected to MySQL');
    }
});

  


// Felhasználók lekérdezése
app.get('/signup', (req, res) => {
    db.query('SELECT * FROM user', (err, results) => {
        if (err) return res.status(500).json({ error: err.message });
        res.json(results);
    });
});

// Regisztráció
app.post('/signup', (req, res) => {
    const { username, email, password } = req.body;

    if (!username || !email || !password) {
        return res.status(400).json({ message: "All fields are required!" });
    }

    bcrypt.hash(password, saltRounds, (err, hash) => {
        if (err) {
            console.error("Hashing error:", err);
            return res.status(500).json({ message: "Error while hashing password." });
        }

        const query = 'INSERT INTO user (Username, Email, Password) VALUES (?, ?, ?)';
        db.query(query, [username, email, hash], (err, result) => {
            if (err) {
                console.error("Database error:", err);
                return res.status(500).json({ message: "Error during registration." });
            }

            res.status(201).json({ message: "User successfully created!" });
        });
    });
});

// Bejelentkezés
app.post('/login', (req, res) => {
    const { username, password } = req.body;

    if (!username || !password) {
        return res.status(400).json({ message: "Username and password are required!" });
    }

    const query = 'SELECT * FROM user WHERE Username = ?';
    db.query(query, [username], (err, results) => {
        if (err) {
            console.error("Database error:", err);
            return res.status(500).json({ message: "Error during login." });
        }

        if (results.length === 0) {
            return res.status(400).json({ message: "Invalid username or password." });
        }

        bcrypt.compare(password, results[0].Password, (err, match) => {
            if (err) {
                console.error("Bcrypt error:", err);
                return res.status(500).json({ message: "Error while checking password." });
            }

            if (!match) {
                return res.status(400).json({ message: "Invalid username or password." });
            }

            res.status(200).json({ message: "Login successful!" });
        });
    });
});


app.post('/forgot-password', (req, res) => {
    const { email } = req.body;
  
    if (!email) {
      return res.status(400).json({ message: 'Email is required!' });
    }
  
    // Check if the email exists in the database
    const query = 'SELECT * FROM user WHERE Email = ?';
    db.query(query, [email], (err, results) => {
      if (err) {
        console.error('Database error:', err);
        return res.status(500).json({ message: 'Error checking email.' });
      }
  
      if (results.length === 0) {
        return res.status(404).json({ message: 'Email not found.' });
      }
  
      // Email options
      const mailOptions = {
        from: process.env.EMAIL_USER,
        to: email,
        subject: 'Password Reset Request',
        text: `Hello,\n\nYou requested a password reset. Click this link to reset your password: http://localhost:3000/reset-password?email=${email}\n\nIf you didn’t request this, ignore this email.`,
      };
  
      // Send the email
      transporter.sendMail(mailOptions, (error, info) => {
        if (error) {
          console.error('Error sending email:', error);
          return res.status(500).json({ message: 'Error sending reset email.' });
        }
        console.log('Reset email sent:', info.response);
        res.status(200).json({ message: 'Reset email sent successfully!' });
      });
    });
  });
  
app.listen(3001, () => {
    console.log("Server is running on port 3001");
});
