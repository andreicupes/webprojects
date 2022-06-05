<?php
require_once "configuration.php";
$id = $_POST['id'];
session_start();
$userID = $_SESSION['myUser'];
$sql_query = "delete from logreports where id='$id' and userID='$userID'";
global $connection;
try{
$result = mysqli_query($connection, $sql_query);
if ($result) {
    echo "Your log was deleted successfully!";
    header("Location: showTable.html");
} 
    echo "New records deleted successfully";
} catch(PDOException $e) {
  echo "Error: " . $e->getMessage();
}
mysqli_close($connection);
