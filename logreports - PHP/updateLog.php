<?php
require_once "configuration.php";
session_start();
$userID = $_SESSION['myUser'];
$id = $_POST['id'];
$type = $_POST['type'];
$severity = $_POST['severity'];
$date = $_POST['date'];
$log = $_POST['log'];

$sql_query = "update logreports set type = '$type',severity = '$severity' ,date = '$date ',log ='$log' where id='$id' and userID = '$userID' ";
global $connection;
try{
$result = mysqli_query($connection, $sql_query);
if ($result) {
    echo "Your log was updated successfully!";
    header("Location: showTable.html");
}    echo "New records updated successfully";
} catch(PDOException $e) {
  echo "Error: " . $e->getMessage();
}
mysqli_close($connection);