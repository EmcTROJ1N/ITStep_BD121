const fs = require("fs");
const pathNS = require("path");
const { format } = require('date-fns');


function isImage(path)
{
    return [".jpg", ".jpeg", ".png", ".gif", ".bmp"].includes(pathNS.extname(path).toLowerCase());
}

function deletePics(path)
{
    fs.readdir(path, (err, files) =>
    {
        if (err)
        {
            console.log(err);
            return;
        }
        files.forEach((filename) =>
        {
            if (isImage(filename))
            {
                fs.unlink(pathNS.join(path, filename), (err) =>
                {
                    if (err)
                        console.log(err);
                })
            }
        });
    });
}

function copyTxtFiles(sourcePath, destPath)
{
    fs.readdir(sourcePath, (err, files) =>
    {
        if (err)
        {
            console.log(err);
            return;
        }
        files.forEach((filename) =>
        {
            if (pathNS.extname(filename).toLowerCase() == ".txt")
            {
                fs.copyFile(pathNS.join(sourcePath, filename), pathNS.join(destPath, filename), (err) =>
                {
                    if (err)
                        console.log(err);
                })
            }
        });
    });
}

function folderObserver(path, backUpPath)
{
    watcher = fs.watch(path, { recursive: true } , (eventType, filename) => {

        let actionType = eventType;

        if (eventType === 'rename')
        {
            if (fs.existsSync(pathNS.join(path, filename)))
            {
                actionType = "create";
                if (pathNS.extname(filename) === ".txt")
                {
                    fs.unlink(pathNS.join(path, filename), (err) =>
                    {
                        if (err)
                        {
                            console.log(err);
                            return;
                        }
                    })
                }

                if (isImage(filename))
                {
                    fs.copyFile(pathNS.join(path, filename), pathNS.join(backUpPath, filename), (err) =>
                    {
                        if (err)
                            console.log(err);
                    })
                }
            }
            else
                actionType = "delete";
            fs.appendFile("result.log", `${format(new Date(), "dd.MM.yyyy HH:mm")} file ${filename} was ${actionType}\n`, (err) =>
                { if (err) console.log(err) });
        }
    });
}

// deletePics("C:/Users/OMON/Desktop/test");
// copyTxtFiles("C:/Users/OMON/Desktop/", "C:/Users/OMON/Desktop/test");
// folderObserver("C:/Users/OMON/Desktop/test", "C:/Users/OMON/Desktop/backup");