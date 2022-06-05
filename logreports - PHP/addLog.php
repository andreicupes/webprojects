<?php
session_start(); 
$userID = $_SESSION['myUser'];
require_once "configuration.php";
$id = $_POST['id'];
$type = $_POST['type'];
$severity = $_POST['severity'];
$date = $_POST['date'];
$log = $_POST['log'];

$sql_query = "insert into logreports(id,type,severity,date,userID,log) values ('$id', '$type', '$severity', '$date', '$userID', '$log')";
global $connection;
try{
$result = mysqli_query($connection, $sql_query);
if ($result) {
    echo "Your log was added successfully!";
    header("Location: showTable.html");
}    echo "New records deleted successfully";
} catch(PDOException $e) {
  echo "Error: " . $e->getMessage();
}
mysqli_close($connection);