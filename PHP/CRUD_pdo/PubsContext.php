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
        $opt = [
            PDO::ATTR_ERRMODE            => PDO::ERRMODE_EXCEPTION,
            PDO::ATTR_DEFAULT_FETCH_MODE => PDO::FETCH_ASSOC
        ];
        $this->Connection = new PDO("mysql:host=localhost; dbname=$this->DBName", "root", "", $opt);
    }

    public function Execute(string $query, array $bindedParams = null)
    {
        $stmt = $this->Connection->prepare($query);

        // присоединение параметров к подготовленному запросу
        if ($bindedParams != null)
            for ($i = 0; $i < count($bindedParams); $i++)
                $stmt->bindParam($i + 1, $bindedParams[$i]);

        echo "<br>";

        // исполнение запроса с заполненными данными
        $stmt->execute();
    }

    public function Select(string $query, array $bindedParams = null)
    {
        $stmt = $this->Connection->prepare($query);

        if ($bindedParams != null)
        {
            foreach ($bindedParams as $idx => $param)
                $stmt->bindParam($idx + 1, $param);

        }
        $stmt->execute();
        return $stmt->fetchAll();
    }

    public function GetAuthors()
    {
        return $this->Select("select * from authors");
    }

    public function GetAuthorById(string $id): mixed
    {
        $result = $this->Select("select * from authors where au_id = ?", [$id]);
        return $result[0];
    }

    public function UpdateAuthor(string $au_id, AuthorDTO $author)
    {
        $query = "UPDATE authors SET au_lname=?, au_fname=?, phone=?, address=?, city=?, state=?, zip=?, contract=? WHERE au_id=?";
        $this->Execute($query,
            [$author->au_lname, $author->au_fname, $author->phone, $author->address, $author->city, $author->state, $author->zip, $author->contract, $author->au_id]);
    }

    public function InsertAuthor(AuthorDTO $author)
    {
        $query = "INSERT INTO authors (au_id, au_lname, au_fname, phone, address, city, state, zip, contract) 
          VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?)";
        $this->Execute($query,
            [$author->au_id, $author->au_lname, $author->au_fname, $author->phone, $author->address, $author->city, $author->state, $author->zip, $author->contract]);
    }

    public function DeleteAuthor(string $au_id)
    {
        $this->Execute("delete from authors where au_id = ?", [$au_id]);
    }
}