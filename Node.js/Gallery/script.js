const expressNS = require("express");
const fsNS = require("fs");
const pathNS = require("path");

const app = expressNS();

app.set("view engine", "pug");
app.use(expressNS.static(__dirname + '/images'));

class Image
{
    Path;
    FileName;
    FileNameWithoutExt;
    Params = new Map();

    constructor(path)
    {
        this.Path = path;
        this.FileName = pathNS.basename(path);
        this.FileNameWithoutExt = pathNS.parse(this.FileName).name;

        this.Params.set("weight", "5MB");
    }
}

app.get("/", (request, response) =>
{
    fsNS.readdir("./images", (err, files) =>
    {
        if (err) response.status(500).send(err);

        fileImgs = new Array();
        files.forEach((file) => fileImgs.push(new Image(`${__dirname}/${file}`)));

        response.render("index", { imgs : fileImgs });
    });
});

app.get("/*", (request, response) =>
{
    console.log(new Image(request.url.substring(1)));
    response.render("image", { img: new Image(request.url.substring(1)) })
});

app.listen(80, () => console.log("Sharmanka started"));