const expressNS = require("express");
const pathNS = require("path");
const fsNS = require("fs");
const path = require("path");
const bodyParser = require("body-parser")

const parser = bodyParser.urlencoded({extended: true});

`use strict`

const app = expressNS();
app.set("view engine", "pug");


app.get("/", (request, response) => response.render("first"));

app.post("/display", parser, (request, response) =>
{
    let min = request.body.min;
    let max = request.body.max;
    let arr = new Array()

    for (let i = min; i < max; i++)
    {
        if (i % 2 != 0)
            arr.push(i);
    }

    response.render("displayNums", { nums: arr })
});

app.listen(80, () => console.log("Sharmanka started"));