<?php

require "Image.php";
class GalleryManager
{
    private static array $Images = [];
    private static string $BasePath = "./imgs";
    private static string $ImagesDumpFileName = "Images.json";

    public function __construct()
    {
        self::Deserialize();
    }

    public static function UploadImage(string $uid, array $fileInfo)
    {
        self::Deserialize();

        $path = self::$BasePath . "/" . $fileInfo["name"];
        echo $path;
        move_uploaded_file($fileInfo['tmp_name'], $path);
        self::$Images[] = new Image($path, $uid);

        self::Serialize();
    }

    public static function DeleteImage($uid, $path)
    {
        self::Deserialize();

        self::$Images = array_filter(self::$Images, function ($img) use ($uid, $path) {
            return $img->OwnerUid != $uid && $img->Path != $path;
        });

        self::Serialize();
    }

    public static function GetImagesByUid(string $uid): Generator
    {
        self::Deserialize();
        foreach (self::$Images as $image)
        {
            //$images[] = $image;
            if ($image->OwnerUid == $uid)
                yield $image->Path;
        }
    }

    private static function Serialize()
    {
        file_put_contents(self::$ImagesDumpFileName, serialize(self::$Images));
    }
    private static function Deserialize()
    {
        if (file_exists(self::$ImagesDumpFileName))
            self::$Images = unserialize(file_get_contents(self::$ImagesDumpFileName));
    }
}