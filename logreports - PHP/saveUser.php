 <?php
    session_start();
    $_SESSION['myUser']= $_GET["userID"];
    $myUser = $_SESSION['myUser'];
    ?>
 <html>

 <body>

     Welcome userID = <?php echo $myUser; ?><br>
     <br>
     <br>


     <button onclick="window.location.href='addLog.html'"> Add new Log</button>
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
     <br>
     <button onclick="window.location.href='showAjax.html'"> Show the logs based on type/severity(with Ajax)</button>
     <br>
     <br>
     
    

 </body>

 </html>