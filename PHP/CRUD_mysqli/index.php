<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Authors CRUD</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #2b2b2b;
            color: #fff;
        }

        table {
            width: 100%;
            border-collapse: collapse;
            margin-bottom: 20px;
        }

        th, td {
            border: 1px solid #464646;
            padding: 10px;
            text-align: left;
        }

        th {
            background-color: #333;
        }

        form {
            margin-bottom: 20px;
        }

        input, button {
            padding: 8px;
            margin-right: 10px;
            background-color: #333;
            color: #fff;
            border: 1px solid #464646;
        }

        input[type="checkbox"] {
            margin-right: 5px;
        }

        .update-btn, .delete-btn {
            cursor: pointer;
            background-color: #4CAF50;
            color: white;
            border: none;
            padding: 8px;
        }

        .delete-btn {
            background-color: #f44336;
        }

        #authorForm {
            display: flex;
            align-items: center;
            flex-wrap: wrap;
        }

        #authorForm * {
            margin: 5px;
        }

        a {
            color: white;
        }
        a:hover {
            color: #3498db;
        }

    </style>
</head>
<body>

<h2>Authors CRUD</h2>


<!-- Table -->
<table>
    <thead>
        <tr>
            <th>au_id</th>
            <th>au_lname</th>
            <th>au_fname</th>
            <th>phone</th>
            <th>address</th>
            <th>city</th>
            <th>state</th>
            <th>zip</th>
            <th>contract</th>
            <th>Actions</th>
        </tr>
    </thead>
    <tbody>
        <?php
        require "PubsContext.php";

        $context = new PubsContext();
        $authors = $context->GetAuthors();

        foreach ($authors as $row):
            echo "<tr>\n";
            echo "<td>" . $row['au_id'] . "</td>\n";
            echo "<td>" . $row['au_lname'] . "</td>\n";
            echo "<td>" . $row['au_fname'] . "</td>\n";
            echo "<td>" . $row['phone'] . "</td>\n";
            echo "<td>" . $row['address'] . "</td>\n";
            echo "<td>" . $row['city'] . "</td>\n";
            echo "<td>" . $row['state'] . "</td>\n";
            echo "<td>" . $row['zip'] . "</td>\n";
            echo "<td>" . $row['contract'] . "</td>\n";
        ?>

        <td>
            <a href="/EditContact.php?au_id=<?php echo $row['au_id']?>">Edit</a>
            <a href="/HomeController.php?action=OnAuthorDelete&au_id=<?php echo $row['au_id']?>">Delete</a>
        </td>

        <?php
        echo "</tr>\n";
        endforeach;
        ?>
    </tbody>
</table>

<!-- Form for Create and Update -->
<form id="authorForm" action="HomeController.php">
    <input type="text" id="au_id" name="au_id" placeholder="au_id">
    <input type="text" id="au_lname" name="au_lname" placeholder="Last Name" required>
    <input type="text" id="au_fname" name="au_fname" placeholder="First Name" required>
    <input type="text" id="phone" name="phone" placeholder="Phone" required>
    <input type="text" id="address" name="address" placeholder="Address" required>
    <input type="text" id="city" name="city" placeholder="City" required>
    <input type="text" id="state" name="state" placeholder="State" required>
    <input type="text" id="zip" name="zip" placeholder="Zip" required>
    <label for="contract">Contract</label>
    <input type="checkbox" id="contract" name="contract">

    <input type="hidden" name="action" value="OnAuthorCreate">
    <button type="submit" id="saveBtn">Add</button>
</form>



<script>
    // JavaScript code for handling CRUD operations will go here
</script>

</body>
</html>
