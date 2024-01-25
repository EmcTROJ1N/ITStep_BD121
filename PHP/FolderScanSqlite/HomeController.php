<?php
require "FilesystemDBContext.php";
class HomeController
{
    public static $Context;

    public static function ScanFolder(string $path)
    {
        $dir = new DirectoryIterator($path);
        $filesData = [];

        foreach ($dir as $fileInfo)
        {
            if ($fileInfo->isDot())
                continue;

            $filesData[] = [
                'name' => $fileInfo->getFilename(),
                'type' => $fileInfo->isDir() ? 'Folder' : pathinfo($fileInfo->getFilename(), PATHINFO_EXTENSION),
                'size' => $fileInfo->isDir() ? '0' : $fileInfo->getSize(),
            ];

            //if ($fileInfo->isDir())
            //   scanFolder($fileInfo->getPathname(), $filesData);
        }
        return $filesData;
    }
    public static function GetStat($data)
    {
        $result = [];

        $result['filesCount'] = count(array_filter($data, function ($elem) {
            if ($elem['type'] != 'Folder')
                return $elem;
        }));
        $result['foldersCount'] = count($data) - $result['filesCount'];

        return $result;
    }


    public static function PushDump($data)
    {
        self::$Context = new FilesystemDBContext("filesystem.db");
        foreach ($data as $file)
            self::$Context->AddFile($file);
    }
}