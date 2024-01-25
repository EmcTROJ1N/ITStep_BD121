<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>File Scanner Report</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            margin: 20px;
        }

        h1 {
            color: #333;
        }

        .report-section {
            margin-top: 20px;
        }

        table {
            border-collapse: collapse;
            width: 100%;
            margin-top: 10px;
        }
        thead * {
            font-weight: bold;
        }

        table, th, td {
            border: 1px solid #ddd;
        }

        th, td {
            padding: 10px;
            text-align: left;
        }

        th {
            background-color: #f2f2f2;
        }

        .form-container {
            margin-top: 20px;
        }
    </style>
</head>
<body>

<h1>File Scanner Report</h1>

<div class="form-container">
    <form action="index.php" method="post">
        <label for="folderPath">Enter Folder Path:</label>
        <input type="text" id="folderPath" name="folderPath" required>
        <button type="submit">Scan</button>
    </form>
</div>

<?php

require "HomeController.php";

if ($_SERVER["REQUEST_METHOD"] == "POST")
{
    $folderPath = $_POST["folderPath"];
    $result = HomeController::ScanFolder($folderPath);
    $stat = HomeController::GetStat($result);
    print_r($stat);
    HomeController::PushDump($result);
}


?>

<div class="report-section">
    <table>
        <thead>
            <td>Name</td>
            <td>Type</td>
            <td>Size</td>
        </thead>
        <tbody>
            <?php
            if (isset($result)):
                foreach ($result as $row)
                {
                    echo "<tr>\n";
                    echo "<td>" . $row['name'] . "</td>\n";
                    echo "<td>" . $row['type'] . "</td>\n";
                    echo "<td>" . $row['size'] . "</td>\n";
                    echo "</tr>\n";
                }
            endif;
            ?>
        </tbody>
    </table>
</div>

</body>
</html>
