<?php
require "AccountsManager.php";
require "GalleryManager.php";

if (isset($_COOKIE["Login"]) && isset($_COOKIE["Password"]) &&
    AccountsManager::CheckAuth($_COOKIE["Login"], $_COOKIE["Password"])):
?>
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Фотогалерея</title>

    <style>
        body {
            font-family: Arial, sans-serif;
            margin: 50px;
        }

        form {
            margin: 0 auto;
        }

        label {
            display: block;
            margin-bottom: 8px;
        }

        input {
            width: 100%;
            padding: 8px;
            margin-bottom: 16px;
        }

        button {
            background-color: #4caf50;
            color: white;
            padding: 10px 15px;
            border: none;
            border-radius: 4px;
            cursor: pointer;
        }

        button:hover {
            background-color: #45a049;
        }

        #gallery {
            display: flex;
            flex-wrap: wrap;
            gap: 20px;
        }

        .photo {
            position: relative;
            overflow: hidden;
            width: 200px;
            height: 200px;
        }

        .photo img {
            width: 100%;
            height: 100%;
            object-fit: cover;
        }

        .delete-btn {
            position: absolute;
            top: 5px;
            right: 5px;
            background-color: #ff0000;
            color: #fff;
            border: none;
            padding: 5px 10px;
            cursor: pointer;
        }

        #upload-input {
            display: none;
        }

        #upload-label {
            display: block;
            background-color: #4caf50;
            color: #fff;
            padding: 10px 15px;
            border: none;
            border-radius: 4px;
            cursor: pointer;
            margin-top: 20px;
        }
    </style>


</head>
<body>

<div id="gallery">


</div>

<form id="upload-form" method="post" enctype="multipart/form-data" action="Controller.php">
    <input type="hidden" name="action" value="OnUploadPhoto">
    <input type="hidden" name="uid"
           value="<?php echo AccountsManager::GetUid($_COOKIE["Login"], $_COOKIE["Password"])?>">
    <input type="file" id="upload-input" accept="image/*" name="image" multiple>
    <label for="upload-input" id="upload-label">Загрузить фото</label>
</form>

<button id="logout-button">Logout</button>


<script>
    const gallery = document.getElementById('gallery');

    const imgs = [];
    <?php
    $uid = AccountsManager::GetUid($_COOKIE["Login"], $_COOKIE["Password"]);

    foreach (GalleryManager::GetImagesByUid($uid) as $path)
        echo "imgs.push('$path');\n";
    ?>
    imgs.forEach(img => addPhoto(img));


    document.getElementById("logout-button").addEventListener(`click`, () => {

        //console.log("here");

        window.location.href = "/Controller.php?action=OnLogout";
    });


    document.getElementById('upload-input').addEventListener('change', e => {
        document.getElementById("upload-form").submit();
    });
    //document.getElementById('upload-input').addEventListener('change', handleFileSelect);

    function handleFileSelect(event) {
        const files = event.target.files;

        for (const file of files) {
            const reader = new FileReader();

            reader.onload = function (e) {
               addPhoto(e.target.result)
            };

            reader.readAsDataURL(file);
        }
    }

    function addPhoto(url) {
        const photoContainer = document.createElement('div');
        photoContainer.className = 'photo';

        const img = document.createElement('img');
        img.src = url;

        const form = document.createElement('form');
        form.id = 'upload-form';
        form.method = 'post';
        form.enctype = 'multipart/form-data';
        form.action = 'Controller.php';

        const actionInput = document.createElement('input');
        actionInput.type = 'hidden';
        actionInput.name = 'action';
        actionInput.value = 'OnDeletePhoto';

        const uidInput = document.createElement('input');
        uidInput.type = 'hidden';
        uidInput.name = 'uid';
        uidInput.value="<?php echo AccountsManager::GetUid($_COOKIE["Login"], $_COOKIE["Password"])?>";

        const pathImg = document.createElement('input');
        pathImg.type = 'hidden';
        pathImg.name = 'pathImg';
        pathImg.value = url;

        const deleteBtn = document.createElement('button');
        deleteBtn.className = 'delete-btn';
        deleteBtn.textContent = 'Удалить';

        /*
        deleteBtn.addEventListener('click', function () {
            gallery.removeChild(photoContainer);
        });
        */

        form.appendChild(actionInput);
        form.appendChild(uidInput);
        form.appendChild(pathImg);
        form.appendChild(deleteBtn);

        photoContainer.appendChild(img);
        //photoContainer.appendChild(deleteBtn);
        photoContainer.appendChild(form);

        gallery.appendChild(photoContainer);
    }

</script>

</body>
</html>
<?php
else:
    header("Location: /Error.php?Error=Access_Denied");
    exit();
endif
?>