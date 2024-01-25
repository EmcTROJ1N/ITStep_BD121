<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Edit Author Contact</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            margin: 0;
            padding: 0;
            background-color: #2b2b2b;
            color: #fff;
        }

        .container {
            max-width: 600px;
            margin: 50px auto;
            background-color: #333;
            padding: 20px;
            box-shadow: 0 0 10px rgba(255, 255, 255, 0.1);
        }

        h2 {
            color: #fff;
        }

        form {
            display: grid;
            grid-gap: 15px;
        }

        label {
            font-weight: bold;
            color: #fff;
        }

        input, textarea {
            width: 100%;
            padding: 10px;
            box-sizing: border-box;
            margin-bottom: 10px;
            background-color: #464646;
            color: #fff;
            border: 1px solid #464646;
        }

        button {
            background-color: #333;
            color: #fff;
            font-weight: bold;
            padding: 10px 15px;
            border: 1px solid #464646;
            cursor: pointer;
        }
    </style>
</head>
<body>
<div class="container">
    <h2>Edit Author</h2>
    <?php
    require "PubsContext.php";
    $Context = new PubsContext();
    $contact = $Context->GetAuthorById($_REQUEST["au_id"]);

    print_r($contact);
    ?>
    <form action="HomeController.php">
        <label for="au_id">Author ID:</label>
        <input type="text" id="au_id" name="au_id" placeholder="Author ID" value="<?php echo $contact['au_id']; ?>" required>

        <label for="au_lname">Last Name:</label>
        <input type="text" id="au_lname" name="au_lname" value="<?php echo $contact['au_lname']; ?>" placeholder="Last Name" required>

        <label for="au_fname">First Name:</label>
        <input type="text" id="au_fname" name="au_fname" value="<?php echo $contact['au_fname']; ?>" placeholder="First Name" required>

        <label for="phone">Phone:</label>
        <input type="text" id="phone" name="phone" value="<?php echo $contact['phone']; ?>" placeholder="Phone" required>

        <label for="address">Address:</label>
        <input type="text" id="address" name="address" value="<?php echo $contact['address']; ?>" placeholder="Address" required>

        <label for="city">City:</label>
        <input type="text" id="city" name="city" value="<?php echo $contact['city']; ?>" placeholder="City" required>

        <label for="state">State:</label>
        <input type="text" id="state" name="state" value="<?php echo $contact['state']; ?>" placeholder="State" required>

        <label for="zip">Zip:</label>
        <input type="text" id="zip" name="zip" value="<?php echo $contact['zip']; ?>" placeholder="Zip" required>

        <label for="contract">Contract</label>
        <input type="checkbox" id="contract" name="contract" <?php echo ($contact['contract'] == 1) ? 'checked' : ''; ?>>

        <input type="hidden" name="action" value="OnAuthorEdit">
        <button type="submit">Save Changes</button>
        <input type="button" value="Cancel" id="cancel-btn">
    </form>

</div>
</body>
<script>
    document.getElementById("cancel-btn").addEventListener("click", e =>
    window.location.href = "/index.php");
</script>
</html>
