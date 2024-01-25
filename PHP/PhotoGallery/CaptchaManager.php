<?php
class CaptchaManager
{
    public static function GenerateCaptcha(): array
    {
        $id = imageCreateTrueColor(320, 200);
        $c1 = ImageColorAllocate($id, 240, 240, 240);
        $c2 = ImageColorAllocate($id, 255, 0, 1);

        imagefilledrectangle($id, 0, 0, 800, 600, $c1);

        $digits = array();
        for($i=0; $i<5; $i++)
        {
            $digits[] = rand(0, 9);
            imagettftext ($id, 48, 0+rand(-20, 20), 100 + $i*20, 100+rand(-20, 20), $c2, __DIR__.'\Fonts\comic.ttf', $digits[$i]);

        }

        $response = [];
        $response["digits"] = implode('', $digits);

        $path = "./captcha/" . self::GenerateUid() . ".jpg";
        $response["imgPath"] = $path;
        ImageJpeg($id, $path);
        ImageDestroy($id);

        return $response;
    }

    private static function GenerateUid(): string
    {
        $data = random_bytes(16);
        $data[6] = chr(ord($data[6]) & 0x0f | 0x40); // устанавливаем версию 4
        $data[8] = chr(ord($data[8]) & 0x3f | 0x80); // устанавливаем вариант
        return vsprintf('%s%s-%s-%s-%s-%s%s%s', str_split(bin2hex($data), 4));
    }
}