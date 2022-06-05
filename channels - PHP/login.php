<?php
    session_start();
	require_once "configuration.php";
    $_SESSION['myUser']= $_GET["username"];
    $myUser = $_SESSION['myUser'];

	//mysqli_query($con, "insert into Students values(3,'aaa','pass',23)");

    ?>
 <html>

 <body>

     Welcome <?php echo $myUser; ?><br>
     <br>
     <br>

     <p>You are subscribed so far to:</p>
     
<?php $myUser =$_SESSION['myUser'];
$str = "SELECT * from channels where subscribers like '%".$myUser."' or subscribers like '".$myUser."%' or subscribers like '%".$myUser."%';";
//  echo $str;
$result = mysqli_query($connection, $str);

//echo "<html><body>";
echo "<table border='1'><tr><th>Name</th><th>Description</th><th>Subscribers</th></tr>";
while($row = mysqli_fetch_array($result) ){
    echo "<tr>";
    echo "<td>" . $row['name'] . "</td>";
    echo "<td>" . $row['description'] . "</td>";
    echo "<td>" . $row['subscribers'] . "</td>";
    echo "</tr>";
}
echo "</table>";
//echo "</body></html>";
mysqli_close($connection);
?>
<br><br>

     <!-- <button onclick="window.location.href='addLog.html'"> Add new Log</button>
     <br>
     <br>
     <button onclick="window.location.href='updateLog.html'"> Update Log</button>
     <br>
     <br>
     <button onclick="window.location.href='deleteLog.html'"> Delete Log</button>
     <br>
     <br>
     <button onclick="window.location.href='data.html'"> Show the logs</button>
     <br>
     <br> -->
     <button onclick="window.location.href='ownedChannels.html'"> Show the owned by someone channels</button>
     <br>
     <br>
     <form action="addSubscriber.php" method="get">
            Name of the channel: <input type="text" name="cname" required><br>
            <input type="submit">
</form>
     
    

 </body>

 </html>