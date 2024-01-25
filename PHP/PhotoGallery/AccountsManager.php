<?php

require "Account.php";
class AccountsManager {
    private static array $Accounts = [];
    private static string $AccountsDumpFileName = "Accounts.json";
    public function __construct()
    {
        self::Deserialize();
    }


    public function Login(string $login, string $password): bool
    {
        AccountsManager::Deserialize();

        foreach (AccountsManager::$Accounts as $account) {
            if ($account->Login == $login && $account->Password == $password)
                return true;
        }
        return false;
    }

    public function Register($login, $password): Account
    {
        $account = new Account($login, $password);
        AccountsManager::$Accounts[] = $account;

        AccountsManager::Serialize();

        return $account;
    }

    private static function Serialize()
    {
        file_put_contents(self::$AccountsDumpFileName, serialize(self::$Accounts));
    }
    private static function Deserialize()
    {
        if (file_exists(self::$AccountsDumpFileName))
            self::$Accounts = unserialize(file_get_contents(self::$AccountsDumpFileName));
    }

    public static function CheckAuth(string $login, string $password): bool
    {
        self::Deserialize();
        foreach (self::$Accounts as $account) {
            if ($account->Login == $login && $account->Password == $password)
                return true;
        }
        return false;
    }

    public static function GetUid(string $login, string $password): string|null
    {
        self::Deserialize();
        foreach (self::$Accounts as $account) {
            if ($account->Login == $login && $account->Password == $password)
                return $account->Uid;
        }
        return null;
    }

}