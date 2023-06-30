const expressNS = require("express");
const pathNS = require("path");
const fsNS = require("fs");
const path = require("path");

`use strict`

const app = expressNS();
app.set("view engine", "pug");

let BeginPath = "C:/Users/OMON/Desktop";

class FileContainer
{
    FileName;
    IsDirectory;

    constructor(path)
    {
        let stats = fsNS.statSync(path);
        this.FileName = pathNS.basename(path);
        this.IsDirectory = stats.isDirectory();
    }
}

app.get("/*", (request, response) =>
{
    // response.set('Cache-Control', 'no-store, no-cache, must-revalidate, private');
    
    let currentPath = request.url == "/" ? BeginPath :
        path.join(BeginPath, request.url);

    let extPath = pathNS.extname(currentPath)
    if (extPath != "")
    {
        console.log(request.url);
        response.sendFile(currentPath);
        return;
    }

    let containerFiles = new Array();

    fsNS.readdir(currentPath, (err, files) =>
    {
        if (err)
        {
            console.log(`error reading dir: ${err}`);
            return;
        }

        files.forEach((file) =>
        {
            let container = new FileContainer(`${currentPath}/${file}`);
            containerFiles.push(container);
        });

        console.log(request.url);
        response.render("render",
        {
            data : containerFiles,
            url : request.url
        });
    });

});

app.listen(80, () => console.log("Sharmanka started"));