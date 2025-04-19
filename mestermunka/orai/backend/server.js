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
    if (err) {
        console.error('Database connection failed:', err);
        process.exit(1);
    } else {
        console.log('Connected to MySQL');
    }
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
                console.error("Signup DB error:", err);
                return res.status(500).json({ message: "Error during registration." });
            }
            res.status(201).json({ message: "User successfully created!" });
        });
    });
});

app.get('/login', (req, res) => {
    db.query('SELECT * FROM user', (err, results) => {
        if (err) return res.status(500).json({ error: err.message });
        res.json(results);
    });
});

app.post('/login', (req, res) => {
    const { username, password } = req.body;

    console.log(`Login attempt: Username: ${username}, Password: ${password ? '[provided]' : '[missing]'}`);

    if (!username) {
        return res.status(400).json({ message: 'Username is required.' });
    }
    if (!password) {
        return res.status(400).json({ message: 'Password is required.' });
    }

    db.query('SELECT * FROM user WHERE UserName = ?', [username], (err, results) => {
        if (err) {
            console.error('Database error:', err);
            return res.status(500).json({ message: 'Database error.' });
        }
        if (results.length === 0) {
            return res.status(400).json({ message: 'User not found.' });
        }

        bcrypt.compare(password, results[0].Password, (err, match) => {
            if (err) {
                console.error('Bcrypt error:', err);
                return res.status(500).json({ message: 'Password verification error.' });
            }
            if (!match) {
                return res.status(400).json({ message: 'Incorrect password.' });
            }
            res.status(200).json({ message: 'Login successful!' });
        });
    });
});

app.delete('/users/:username', (req, res) => {
    const { username } = req.params;

    const query = 'DELETE FROM user WHERE UserName = ?';
    db.query(query, [username], (err, result) => {
        if (err) return res.status(500).json({ message: 'Error during deletion.' });
        if (result.affectedRows === 0) return res.status(404).json({ message: 'User not found.' });

        res.status(200).json({ message: 'User successfully deleted!' });
    });
});

app.get('/chat', (req, res) => {
    const query = 'SELECT * FROM post';
    db.query(query, (err, results) => {
        if (err) {
            console.error('Error fetching posts:', err);
            return res.status(500).json({ message: 'Error fetching posts' });
        }
        res.json(results);
    });
});

app.post('/chat', (req, res) => {
    const { title, content, } = req.body;

if (!title || !content) {
    return res.status(400).json({ message: "Title, content and are required!" });
}

    const query = 'INSERT INTO post (Title, Content) VALUES (?, ?)';
    db.query(query, [title, content, ], (err) => {
        if (err) {
            console.error('Post DB error:', err);
            return res.status(500).json({ message: "Error creating post." });
        }
        res.status(201).json({ message: "Post created" });
    });
});

app.get('/chat/:id', (req, res) => {
    const id = parseInt(req.params.id);
    const query = 'SELECT * FROM post WHERE id = ?';
    db.query(query, [id], (err, results) => {
        if (err) {
            console.error('Error fetching post:', err);
            return res.status(500).json({ message: 'Error fetching post' });
        }
        if (results.length === 0) {
            return res.status(404).json({ message: "Post not found" });
        }
        res.json(results[0]);
    });
});

app.put('/chat/:id', (req, res) => {
    const id = parseInt(req.params.id);
    const { title, content } = req.body;

    if (!title || !content) {
        return res.status(400).json({ message: "Title and content are required!" });
    }

    const query = 'UPDATE post SET title = ?, content = ? WHERE id = ?';
    db.query(query, [title, content, id], (err, result) => {
        if (err) {
            console.error('Error updating post:', err);
            return res.status(500).json({ message: 'Error updating post' });
        }
        if (result.affectedRows === 0) {
            return res.status(404).json({ message: 'Post not found' });
        }
        res.sendStatus(200);
    });
});

app.post('/chat/:id/report', (req, res) => {
    const id = parseInt(req.params.id);
    const query = 'UPDATE post SET reports = reports + 1 WHERE id = ?';
    db.query(query, [id], (err, result) => {
        if (err) {
            console.error('Error reporting post:', err);
            return res.status(500).json({ message: 'Error reporting post' });
        }
        if (result.affectedRows === 0) {
            return res.status(404).json({ message: 'Post not found' });
        }
        res.json({ message: "Reported" });
    });
});

app.delete('/chat/:id', (req, res) => {
    const id = parseInt(req.params.id);

    const deleteComments = 'DELETE FROM comment WHERE PostId = ?';
    const deletePost = 'DELETE FROM post WHERE id = ?';

    db.query(deleteComments, [id], (err) => {
        if (err) {
            console.error('Error deleting comments:', err);
            return res.status(500).json({ message: 'Error deleting comments' });
        }

        db.query(deletePost, [id], (err, result) => {
            if (err) {
                console.error('Error deleting post:', err);
                return res.status(500).json({ message: 'Error deleting post' });
            }
            if (result.affectedRows === 0) {
                return res.status(404).json({ message: 'Post not found' });
            }
            res.sendStatus(200);
        });
    });
});


app.listen(3001, () => console.log("Server is running on port 3001"));