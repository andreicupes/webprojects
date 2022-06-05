<?php
  session_start();
  require_once "configuration.php";

  global $connection;
  $myUser =$_SESSION['myUser'];
  $channel= $_GET["cname"];
//   echo $channel;
$today = date("m.d.y");
$today2 = "01.01.01";
echo $today;
  $str = "SELECT id,subscribers from channels where (subscribers like '%".$myUser."' or subscribers like '".$myUser."%' or subscribers like '%".$myUser."%') and name ='".$channel."';";
//  echo $str;
    $result = mysqli_query($connection, $str);

//echo "<html><body>";
if ($result->num_rows > 0) {
    echo "need to update the date";
    $row1 = $result->fetch_assoc() ;
    $idToUpdate = $row1['id'];
    $oldSub = $row1['subscribers'];
    $array = explode('|', $oldSub);
    $newSub="";
    foreach($array as $sub){
        if($sub != ''){
        if (str_contains($sub, $myUser)) { 
            $newSub = $newSub.$myUser.$today."|";
        }
        else{
            $newSub = $newSub.$sub."|";
        }}
    }
    $str = "UPDATE channels set subscribers = '".$newSub."' where id =".$idToUpdate.";";
    echo $str;
    $result = mysqli_query($connection, $str);
    
}
else{
    $str = "SELECT id,subscribers from channels where name ='".$channel."';";
    //  echo $str;
    $result = mysqli_query($connection, $str);
    $row1 = $result->fetch_assoc() ;
    $idToUpdate = $row1['id'];
    $newSub = $row1['subscribers'];
    $newSub = $newSub.$myUser.$today."|";
    echo $newSub;
    $str = "UPDATE channels set subscribers = '".$newSub."' where id =".$idToUpdate.";";
    echo $str;
    $result = mysqli_query($connection, $str);
}

//echo "</body></html>";
mysqli_close($connection);




  header("Location: http://localhost:8080/channelsbackend/login.php?username=".$myUser);
    die();
  ?>