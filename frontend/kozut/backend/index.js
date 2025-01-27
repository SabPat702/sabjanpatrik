const express = require("express");
const app = express();
const cors = require("cors");
const mysql = require("mysql");
const bodyParser = require("body-parser");
app.use(bodyParser.json());
app.use(cors());

const db = mysql.createConnection({
    user: "root",
    host: "127.0.0.1",
    password: "",
    database: "kozutak",
});

app.get("/", (req, res) => {
    res.send("Fut a backend.");
});

app.get("/regiok", (req, res) => {
    const query = "SELECT * FROM regiok";
    db.query(query, (err, result) => {
        if (err) return res.json(err);
        return res.json(result)
    })
});

app.get("/regio8", (req, res) => {
    const query = "SELECT * FROM regiok WHERE Rid = 8"
    db.query(query, (err, result) => {
        if (err) return res.json(err);
        return res.json(result)
    })
});

app.post("/ujRegio", (req, res) => {
    const query = "INSERT INTO `regiok` (`Rid` , `regionev` , `regio_tipusa`) VALUES (? , ? , ?)"
    const values = ['11', 'Budapest', 'főváros'];

    db.query(query, values, (err, result) => {
        if (err){
            console.error("Hiba történt:", err); 
            return res.status(500).json({error: "Adatbázis hiba történt."});
        }
        return res.status(200).json({message: "Az adatok fellettek töltve.", result});
    })
});

app.post("/ujRegio2", (req, res) => {
    const query = "INSERT INTO `regiok` (`Rid` , `regionev` , `regio_tipusa`) VALUES (? , ? , ?), (? , ? , ?)"
    const values = ['10', 'Budapest', 'főváros', '11', 'Pest' , 'régió'];

    db.query(query, values, (err, result) => {
        if (err){
            console.error("Hiba történt:", err); 
            return res.status(500).json({error: "Adatbázis hiba történt."});
        }
        return res.status(200).json({message: "Az adatok fellettek töltve.", result});
    })
});

app.delete("/deleteRegio/:id", (req, res) => {
    const query = "DELETE FROM `regiok` WHERE Rid = ?"
    db.query(query, [req.params.id], (err, result) => {
        if (err) return res.json(err);
        return res.json(result)
    })
});

app.listen(3001, () => {
    console.log("Server is running on port 3001");
});