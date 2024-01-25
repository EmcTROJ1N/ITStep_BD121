<?php

require "AccountsManager.php";
require "GalleryManager.php";

class Controller {
    public AccountsManager $_AccountManager;

    public function __construct()
    {
        $this->_AccountManager = new AccountsManager();
    }

    public function OnLogin()
    {
        $login = $_REQUEST["login"];
        $password = $_REQUEST["password"];

        if ($this->_AccountManager->Login($login, $password))
        {
            setcookie("LOGGED", true, time() + 3600 * 24 * 30);
            setcookie("Login", $login, time() + 3600 * 24 * 30);
            setcookie("Password", $password, time() + 3600 * 24 * 30);
            header("Location: ./home.php");
            exit();
        }
        else
        {
            header("Location: ./Error.php?Error=Invalid_password");
            exit();
        }
    }

    public function OnRegister()
    {
        if (!isset($_REQUEST["login"])) $this->OnError("Login_not_found");
        elseif (!isset($_REQUEST["password"])) $this->OnError("password_not_found");
        elseif (!isset($_REQUEST["captcha"])) $this->OnError("captcha_not_found");
        elseif (!isset($_REQUEST["captcha-input"])) $this->OnError("captcha_not_found");

        if ($_REQUEST["captcha-input"] != $_REQUEST["captcha"])
            $this->OnError("Invalid_captcha");

        $login = $_REQUEST["login"];
        $password = $_REQUEST["password"];

        if ($this->_AccountManager->Register($login, $password))
        {
            setcookie("LOGGED", true, time() + 3600 * 24 * 30);
            setcookie("Login", $login, time() + 3600 * 24 * 30);
            setcookie("Password", $password, time() + 3600 * 24 * 30);
            header("Location: /home.php");
            exit();
        }
    }

    public function OnLogout()
    {
        if (isset($_COOKIE["LOGGED"]) && $_COOKIE["LOGGED"])
        {
            setcookie("LOGGED", false, time() + 3600 * 24 * 30);
            setcookie("Login", false, time() + 3600 * 24 * 30);
            setcookie("Password", false, time() + 3600 * 24 * 30);
            header("Location: /index.php");
            exit();
        }
    }

    public function OnError(string $errorMsg)
    {
        header("Location: /Error.php?Error=$errorMsg");
        exit();
    }

    public function OnUploadPhoto()
    {
        if (!isset($_REQUEST["uid"]))
            $this->OnError("uid_not_found");
        if ($_FILES['image']['error'] !== UPLOAD_ERR_OK)
            $this->OnError("Upload_error");

        GalleryManager::UploadImage($_REQUEST["uid"], $_FILES["image"]);
        header("Location: /home.php");
        exit();
    }

    public function OnDeletePhoto()
    {
        if (!isset($_REQUEST["uid"])) $this->OnError("uid_not_found");
        elseif (!isset($_REQUEST["pathImg"])) $this->OnError("path_not_found");

        GalleryManager::DeleteImage($_REQUEST["uid"], $_REQUEST["pathImg"]);

        header("Location: /home.php");
        exit();
    }

}

$controller = new Controller();

if (isset($_REQUEST["action"]) && $_REQUEST["action"] != "")
    call_user_func([$controller, $_REQUEST["action"]]);
else
    $controller->OnError("Not found");


