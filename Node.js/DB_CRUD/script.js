const expressNS = require("express");
const mysql = require("mysql2");
const { render } = require("pug");
const bodyParser = require("body-parser");
const { request, response } = require("express");

`use strict`

const app = expressNS();
app.set("view engine", "pug");
app.use(expressNS.static(__dirname));
const parser = bodyParser.urlencoded({extended: true});
app.use(expressNS.json())

const connection = mysql.createConnection(
{
    host: "localhost",
    user: "root",
    database: "pubs",
    password: ""
});

connection.connect((err) =>
{
    if (err) return console.error("Error: " + err.message);
    else console.log("Connection successful!");
});

class Title
{
    Title;
    Price;
    Type;
    Title_id;

    constructor(title, price, type, Title_id)
    {
        this.Title = title;
        this.Price = price;
        this.Type = type;
        this.Title_id = Title_id
    }
}

app.get("/", (request, response) =>
{
    connection.query("SELECT * FROM TITLES", (err, data, fields) => {
        if (err) throw err;
        
        let titles = []
        data.forEach((title) =>
            titles.push(new Title(title.TITLE, title.PRICE, title.TYPE, title.TITLE_ID)));

        return response.render("index", { titles: titles })
    });
});

app.post("/delete", parser, (request, response) =>
{
    console.log(request.body);

    request.body.forEach((id) =>
    {
        connection.query("DELETE FROM TITLES WHERE TITLE_ID = ?", [id], (err, data, fields) => {});
    })
});

app.post("/append", parser, (request, response) =>
{
    connection.query("INSERT INTO TITLES(TITLE_ID, TITLE, TYPE, PUB_ID, PRICE, ADVANCE, ROYALTY, YTD_SALES, NOTES) values (?, ?, ?, ?, ?, ?, ?, ?, ?)",
    [ request.body.title_id, request.body.title, request.body.type, request.body.pub_id, request.body.price,
      request.body.advance, request.body.royalty, request.body, request.body.ytd_sales, request.body.notes ],
    (err, data, field) =>
    {
        if (err) 
            response.json({ message: err.message });
        response.json({ message: 'Deleted successfully' });
    });
});

app.listen(80, () => console.log("Sharmanka started"));