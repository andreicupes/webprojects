<?php
// Array with names

  session_start();
  require_once "configuration.php";

  global $connection;
  $myUser =$_SESSION['myUser'];



  $sql = "SELECT * FROM channels";
  $result = $connection->query($sql);

  if ($result->num_rows > 0) {
    $array = array();

    while($row = $result->fetch_assoc()) {
      $obj = array(
        'id' => $row["id"], 
        'ownerid' => $row["ownerid"],
        'name' => $row['name'],
        'description' => $row['description'],
        'subscribers' => $row['subscribers'],
      );

      $json_obj = json_encode($obj);
      array_push($array, $obj);
    }}

$q = $_REQUEST["q"];

$a = array();


if ($q !== "") {
    
  
  foreach($array as $log) {
    
    $sql1 = "SELECT name FROM persons where id =".$log['ownerid'];
    $result1 = $connection->query($sql1);
    if ($result1->num_rows > 0) {
    $row1 = $result1->fetch_assoc() ;
    if ($q == $row1['name']) {
        array_push($a, $log);
    }
}
  }

}

// Output "no suggestion" if no hint was found or output correct values
// echo json_encode($a);

if($a){
    echo "<table border='1'><tr><th>ID</th><th>Owner</th><th>Name</th><th>Description</th><th>Subscribers</th></tr>";
    foreach($a as $row){ 
    echo "<tr>";
    echo "<td>" . $row['id'] . "</td>";
    echo "<td>" . $row['ownerid'] . "</td>";
    echo "<td>" . $row['name'] . "</td>";
    echo "<td>" . $row['description'] . "</td>";
    echo "<td>" . $row['subscribers'] . "</td>";
    echo "</tr>";
}
echo "</table>";
    }


$connection->close();
?>
