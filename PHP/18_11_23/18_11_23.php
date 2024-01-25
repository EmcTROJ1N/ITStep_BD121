<?php

function rsearch($folder, $pattern) {
    $return = [];

    $iti = new RecursiveDirectoryIterator($folder);
    foreach(new RecursiveIteratorIterator($iti) as $file) {
        if(preg_match($pattern , $file)){
            $return[] = $file;
        }
    }
    return $return;
}

function deleteTxtInPath(string $folderName) {
    if (is_dir($folderName)) {
        // Получаем список файлов в папке
        $files = glob($folderName . '/*.txt');

        // Удаляем каждый текстовый файл
        foreach ($files as $file) {
            unlink($file);
            echo "Файл удален: $file <br>";
        }

        echo "Все текстовые файлы в папке удалены.\n";
    } else {
        echo "Указанная папка не существует.\n";
    }
}
function copyImages(string $sourcePath, string $destPath) {

    if (!is_dir($sourcePath) || !is_dir($destPath)) {
        echo "Указанные папки не существуют.\n";
        return;
    }

    $images = rsearch($sourcePath, '/.*.(jpg|jpeg|png|gif)/i');

    // Создаем результирующую папку, если она не существует
    if (!is_dir($destPath)) {
        mkdir($destPath, 0777, true);
    }

    // Копируем каждую картинку в результирующую папку
    foreach ($images as $image) {
        $destFile = $destPath . '/' . basename($image);
        copy($image, $destFile);
        echo "Картинка скопирована: $destFile <br>";

        // Записываем имя скопированного файла в файл логов
        file_put_contents('log.txt', $destFile . "\n", FILE_APPEND);
    }

    echo "Все картинки успешно скопированы.\n";
}
function findFiles(string $path, string $mask) {
    $result = rsearch($path, $mask);
    foreach ($result as $file) {
        echo "$file <br>";
    }
}

function createDictionary(array $fileInfo) {
    $fileContent = file_get_contents($fileInfo["tmp_name"]);
    $words = array_unique(preg_split('/\s+/i', $fileContent, -1, PREG_SPLIT_NO_EMPTY));

    echo "$fileContent <br><br><br>";

    foreach ($words as $word) {
        echo "$word ";
    }

}

if (isset($_REQUEST["txtDeletePath"]) != "")
    deleteTxtInPath(isset($_REQUEST["txtDeletePath"]));
elseif (isset($_REQUEST["copyImagesSource"]) != "" && isset($_REQUEST["copyImagesDest"]) != "")
    copyImages($_REQUEST["copyImagesSource"], $_REQUEST["copyImagesDest"]);
elseif (isset($_REQUEST["pathToMaskFind"]) != "" && isset($_REQUEST["fileMask"]) != "")
    findFiles($_REQUEST["pathToMaskFind"], $_REQUEST["fileMask"]);
elseif ($_FILES && $_FILES["filename"]["error"]== UPLOAD_ERR_OK)
    createDictionary($_FILES["filename"]);
else
    echo "Error";