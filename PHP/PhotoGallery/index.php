<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Регистрация и Авторизация</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            margin: 50px;
        }

        form {
            max-width: 400px;
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

<?php

require "CaptchaManager.php";

if (!isset($_COOKIE["LOGGED"]) || !$_COOKIE["LOGGED"]):
    $captcha = CaptchaManager::GenerateCaptcha();
?>
    <form action="Controller.php" method="get">
        <h2>Регистрация</h2>
        <label for="regUsername">Логин:</label>
        <input type="text" id="login" name="login" required>

        <label for="regPassword">Пароль:</label>
        <input type="password" id="password" name="password" required>

        <label for="regPassword">Что вы видите на картинке?</label>
        <input type="number" name="captcha-input" required>
        <img src="<?php echo $captcha["imgPath"]?>">
        <input type="hidden" name="captcha" value="<?php echo $captcha["digits"] ?>" required>

        <button type="submit">Зарегистрироваться</button>
        <input type="hidden" name="action" value="OnRegister">
    </form>

    <form action="Controller.php" method="get">
        <h2>Авторизация</h2>
        <label for="loginUsername">Логин:</label>
        <input type="text" id="login" name="login" required>

        <label for="loginPassword">Пароль:</label>
        <input type="password" id="loginPassword" name="password" required>

        <button type="submit">Войти</button>
        <input type="hidden" name="action" value="OnLogin">
    </form>
<?php
else:
    header("Location: /home.php");
    exit();
endif;
?>

</body>
</html>
