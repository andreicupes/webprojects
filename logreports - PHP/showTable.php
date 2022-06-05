<?php
	// $con = mysqli_connect("localhost", "root","", "logsdb");
	// if (!$con) {
	// 	die('Could not connect: ' . mysqli_error());
	// }
	session_start();
	require_once "configuration.php";
	//mysqli_query($con, "insert into Students values(3,'aaa','pass',23)");
	global $connection;

	$result = mysqli_query($connection, "SELECT * FROM logreports");

	//echo "<html><body>";
	echo "<table border='1'><tr><th>ID</th><th>Type</th><th>Severity</th><th>Date</th><th>UserID</th><th>Log</th></tr>";

	while($row = mysqli_fetch_array($result) ){
		echo "<tr>";
		echo "<td>" . $row['id'] . "</td>";
		echo "<td>" . $row['type'] . "</td>";
		echo "<td>" . $row['severity'] . "</td>";
		echo "<td>" . $row['date'] . "</td>";
		echo "<td>" . $row['userID'] . "</td>";
		echo "<td>" . $row['log'] . "</td>";
		echo "</tr>";
	}
	echo "</table>";
	//echo "</body></html>";
	mysqli_close($connection);
?> 