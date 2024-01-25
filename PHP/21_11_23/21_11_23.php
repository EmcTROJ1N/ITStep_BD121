<?php

function getNumbersSum(array $fileInfo) {
    $fileContent = file_get_contents($fileInfo["tmp_name"]);
    $numbers = [];
    preg_match_all("/\d+/i", $fileContent, $numbers);

    foreach ($numbers[0] as $number) {
        echo "$number ";
    }
    echo "= " . array_sum($numbers[0]);
}

function numbersCalcFile(array $fileInfo) {
    $fileContent = file_get_contents($fileInfo["tmp_name"]);
    $fileContent = str_replace("\n", "<br>", $fileContent);

    //echo "$fileContent <br><br><br>";

    $replacedContent = preg_replace_callback("/\d+\s*[\+|\-\/|\*]\s*\d+/i", function($matches)  {
        return eval("return $matches[0];");
    }, $fileContent);

    echo $replacedContent;
}

if ($_FILES && isset($_FILES["numbersSumFile"]) && $_FILES["numbersSumFile"]["error"] == UPLOAD_ERR_OK)
    getNumbersSum($_FILES["numbersSumFile"]);
elseif ($_FILES && isset($_FILES["numbersCalcFile"]) && $_FILES["numbersCalcFile"]["error"] == UPLOAD_ERR_OK)
    numbersCalcFile($_FILES["numbersCalcFile"]);
else
    echo "Error";