import express from 'express';
import cors from 'cors';
import mysql2 from 'mysql2';
import bodyParser from 'body-parser';
import bcrypt from 'bcrypt';

const app = express();
const saltRounds = 5;

app.use(bodyParser.json());
app.use(cors());

const db = mysql2.createConnection({
    user: "root",
    host: "127.0.0.1",
    password: "",
    database: "jatek",
    port: 3306
});

db.connect(err => {
    if (err) console.error('Database connection failed:', err);
    else console.log('Connected to MySQL');
});

app.get("/", (req, res) => {
    res.send("Fut a backend!");
});

app.get('/signup', (req, res) => {
    db.query('SELECT * FROM user', (err, results) => {
        if (err) return res.status(500).json({ error: err.message });
        res.json(results);
    });
});

app.post('/signup', (req, res) => {
    const { username, email, password } = req.body;

    if (!username || !email || !password) {
        return res.status(400).json({ message: "All fields are required!" });
    }

    bcrypt.hash(password, saltRounds, (err, hash) => {
        if (err) return res.status(500).json({ message: "Error while hashing password." });

        const query = 'INSERT INTO user (UserName, Email, Password) VALUES (?, ?, ?)';
        db.query(query, [username, email, hash], (err) => {
            if (err) {
                console.error("Signup DB error:", err); // EZT ADD HOZZÁ
                return res.status(500).json({ message: "Error during registration." });
            }
            res.status(201).json({ message: "User successfully created!" });
        });        
    });
});

app.post('/login', (req, res) => {
    const { username, password } = req.body;

    const query = 'SELECT * FROM user WHERE Username = ?';
    db.query(query, [username], (err, results) => {
        if (err || results.length === 0) return res.status(400).json({ message: "Invalid username or password." });

        bcrypt.compare(password, results[0].Password, (err, match) => {
            if (!match) return res.status(400).json({ message: "Invalid username or password." });
            res.status(200).json({ message: "Login successful!" });
        });
    });
});

app.delete('/users/:username', (req, res) => {
    const { username } = req.params;

    const query = 'DELETE FROM user WHERE Username = ?';
    db.query(query, [username], (err, result) => {
        if (err) return res.status(500).json({ message: 'Error during deletion.' });
        if (result.affectedRows === 0) return res.status(404).json({ message: 'User not found.' });

        res.status(200).json({ message: 'User successfully deleted!' });
    });
});

app.listen(3001, () => console.log("Server is running on port 3001"));
