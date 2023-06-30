const expressNS = require("express");
const pathNS = require("path");
const fsNS = require("fs");
const path = require("path");
const bodyParser = require("body-parser")

const parser = bodyParser.urlencoded({extended: true});

`use strict`

const app = expressNS();
app.set("view engine", "pug");


app.get("/", (request, response) => response.render("second"));

app.post("/display", parser, (request, response) =>
{
    console.log(request.body.folderName);
    fsNS.readdir(request.body.folderName, (err, files) =>
    {
        if (err)
            response.send(err);
        console.log(files);
        response.render("displayData", { data: files });
    });
});

app.listen(80, () => console.log("Sharmanka started"));