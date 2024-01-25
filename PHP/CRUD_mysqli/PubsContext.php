<?php
class PubsContext
{
    public $HostName = "localhost";
    public $Login = "root";
    public $Password = null;
    public $DBName = "pubs";

    public $Connection;

    public function __construct()
    {
        // параметры: адрес сервера, логин, пароль, название БД
        $this->Connection = new mysqli($this->HostName, $this->Login, $this->Password, $this->DBName);
        /* Выполняем SQL-запрос */
        $this->Connection->query("SET NAMES 'cp1251'")
        or die("Query failed : " . mysqli_error($this->Connection));

        //$query = "insert into readers set passportid='{$_POST["passport"]}', surname='{$_POST["surname"]}', name='{$_POST["name"]}', adress='{$_POST["adress"]}', phone='{$_POST["phone"]}'";
        //$query = "insert into books (bookcode, bookname, author, present) values (3, 'Незнайка', 'Носов', 1)";
        //$connection->query($query)
        //or die("Query failed : " . mysqli_error($connection));
    }

    public function Execute(string $query, string $types = null, array $bindedParams = null)
    {
        //$query = "insert into books set bookname=?, bookcode=?, author=?, present=?";
        // подготовка запроса к исполнению
        $stmt = $this->Connection->prepare($query);

        // присоединение параметров к подготовленному запросу
        if ($bindedParams != null)
            $stmt->bind_param($types, ...$bindedParams);
        // исполнение запроса с заполненными данными

        $stmt->execute();

        return $stmt->get_result();

        //$this->Connection->query($query);
    }

    public function __destruct()
    {
        $this->Connection->close();
    }

    public function GetAuthors()
    {
        return $this->Execute("select * from authors");
    }

    public function GetAuthorById(string $id): mixed
    {
        $result = $this->Execute("select * from authors where au_id = ?", "s", [$id]);
        return $result->fetch_assoc();
    }

    public function UpdateAuthor(string $au_id, AuthorDTO $author)
    {
        $query = "UPDATE authors SET au_lname=?, au_fname=?, phone=?, address=?, city=?, state=?, zip=?, contract=? WHERE au_id=?";
        $this->Execute($query, 'sssssssss',
            [$author->au_lname, $author->au_fname, $author->phone, $author->address, $author->city, $author->state, $author->zip, $author->contract, $author->au_id]);
    }

    public function InsertAuthor(AuthorDTO $author)
    {
        $query = "INSERT INTO authors (au_id, au_lname, au_fname, phone, address, city, state, zip, contract) 
          VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?)";
        $this->Execute($query, 'sssssssss',
            [$author->au_id, $author->au_lname, $author->au_fname, $author->phone, $author->address, $author->city, $author->state, $author->zip, $author->contract]);
    }

    public function DeleteAuthor(string $au_id)
    {
        $this->Execute("delete from authors where au_id = ?", "s", [$au_id]);
    }
}