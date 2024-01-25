<?php
require "PubsContext.php";
require "AuthorModel.php";
class HomeController
{
    private $Context;

    public function __construct()
    {
        $this->Context = new PubsContext();
    }

    public function OnAuthorCreate()
    {
        $au_id = $_REQUEST["au_id"];
        $au_lname = $_REQUEST["au_lname"];
        $au_fname = $_REQUEST["au_fname"];
        $phone = $_REQUEST["phone"];
        $address = $_REQUEST["address"];
        $city = $_REQUEST["city"];
        $state = $_REQUEST["state"];
        $zip = $_REQUEST["zip"];
        $contract = isset($_REQUEST["contract"]) ? 1 : 0;

        $this->Context->InsertAuthor(new AuthorDTO(
            $au_id, $au_lname, $au_fname, $phone,
            $address, $city, $state, $zip, $contract
        ));

        header("Location: /index.php");
        exit();
    }

    public function OnAuthorEdit()
    {
        $au_id = $_REQUEST["au_id"];
        $au_lname = $_REQUEST["au_lname"];
        $au_fname = $_REQUEST["au_fname"];
        $phone = $_REQUEST["phone"];
        $address = $_REQUEST["address"];
        $city = $_REQUEST["city"];
        $state = $_REQUEST["state"];
        $zip = $_REQUEST["zip"];
        $contract = isset($_REQUEST["contract"]) ? 1 : 0;

        $this->Context->UpdateAuthor($au_id, new AuthorDTO(
            $au_id, $au_lname, $au_fname, $phone,
            $address, $city, $state, $zip, $contract
        ));

        header("Location: /index.php");
        exit();
    }

    public function OnAuthorDelete()
    {
        $this->Context->DeleteAuthor($_REQUEST["au_id"]);
        header("Location: /index.php");
        exit();
    }

    public function OnError(string $errorMsg)
    {
        header("Location: /Error.php?Error=$errorMsg");
        exit();
    }
}

$controller = new HomeController();

if (isset($_REQUEST["action"]) && $_REQUEST["action"] != "")
    call_user_func([$controller, $_REQUEST["action"]]);
else
    $controller->OnError("Not found");