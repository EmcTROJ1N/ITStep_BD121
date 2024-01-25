<?php

class FilesystemDBContext extends SQLite3
{
    public function __construct($filename, $flags = SQLITE3_OPEN_READWRITE | SQLITE3_OPEN_CREATE, $encryptionKey = '')
    {
        parent::__construct($filename, $flags, $encryptionKey);

        $this->exec("CREATE TABLE files 
                              (file_id INTEGER PRIMARY KEY,  
                               name TEXT, 
                               type TEXT,
							    size INTEGER); 
                              ");
    }

    public function AddFile($fileInfo)
    {
        $query = $this->prepare("INSERT INTO files(name, type, size) VALUES (:name, :type, :size)");
        $query->bindValue(':name', $fileInfo['name'], SQLITE3_TEXT);
        $query->bindValue(':type', $fileInfo['type'], SQLITE3_TEXT);
        $query->bindValue(':size', $fileInfo['size'], SQLITE3_INTEGER);
        $query->execute();
    }
}