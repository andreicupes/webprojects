<?php
  session_start();
  require_once "configuration.php";

  global $connection;
$myUser =$_SESSION['myUser'];
$result = mysqli_query($connection, "select * from channels where subscribers like '%".$myUser."' or subscribers like '+".$myUser."%' or subscribers like '%".$myUser."%')");

//echo "<html><body>";
echo "<table border='1'><tr><th>ID</th><th>Owner</th><th>Name</th><th>Desc</th><th>Subscribers</th></tr>";

while($row = mysqli_fetch_array($result) ){
    echo "<tr>";
    echo "<td>" . $row['name'] . "</td>";
    echo "<td>" . $row['description'] . "</td>";
    echo "</tr>";
}
echo "</table>";
//echo "</body></html>";
mysqli_close($connection);
?>