<?php
// Array with names

  session_start();
  require_once "configuration.php";
  //mysqli_query($con, "insert into Students values(3,'aaa','pass',23)");
  global $connection;
  $myUser =$_SESSION['myUser'];
  // $myUser = $_GET["uID"];


  $sql = "SELECT * FROM logreports";
  $result = $connection->query($sql);

  if ($result->num_rows > 0) {
    $array = array();

    while($row = $result->fetch_assoc()) {
      $obj = array(
        'id' => $row["id"], 
        'type' => $row["type"],
        'severity' => $row['severity'],
        'date' => $row['date'],
        'userID' => $row['userID'],
        'log' => $row['log'],
      );

      $json_obj = json_encode($obj);
      array_push($array, $obj);
    }}

    // echo json_encode($array);
//   } else {
//     echo "0 results";
//   }

 


// get the q parameter from URL
$q = $_REQUEST["q"];

$a = array();

// lookup all hints from array if $q is different from ""
if ($q !== "") {
  
  foreach($array as $log) {
    if ($q == $log['type'] ||$q == $log['severity']  ) {
        // $json_obj = json_encode($log);
        array_push($a, $log);
    }
  }
}

// Output "no suggestion" if no hint was found or output correct values
echo json_encode($a);

$connection->close();
?>
