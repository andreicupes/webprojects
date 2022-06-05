
<%@ page import="web.teams.domain.*" %>
<%--<%@ page import="web.assignment8.domain.Grid" %>--%>
<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<%@ page import="java.sql.DriverManager" %>
<%@ page import="java.sql.Connection" %>
<%@ page import="java.sql.Statement" %>
<%@ page import="java.sql.ResultSet" %>
<html>
<head>

    <title>Forum</title>
    <style>
        .table {
            display: flex;
        }
        body{
            background-color: rgba(0, 255, 129, 0.44);
        }
    </style>
    <script type="text/javascript">

        var Msg ='<%=session.getAttribute("toDisplay")%>';
        console.log(Msg);
        if (Msg != "no changes were found") {
            function alertName(){
                alert("\nThe following changes were found:\n"+Msg);
            }

        }
    </script>
</head>
<body>
<div>
    <%
        out.println("<h3>Welcome to the forum, " + (request.getSession().getAttribute("user")) + "! </h3><br>");
    %>
    <form action="UpdatesController" method="get">
        <input type="submit" id="upd" value="Check for updates!">
    </form>
</div>
<div>
    <%
        out.println("<h3>Check your articles in a given journal,  "+(request.getSession().getAttribute("user"))+" :</h3>");
    %>
    <form action="MainPageController" method="get">
        <input type="text" placeholder="enter the journal's name here:" name="journal" required>
        <input type="submit" id="getJournals" value="Check!">
    </form>
</div>
<div>
    <%
        out.println("<br><h3>Add an article in a given journal,  "+(request.getSession().getAttribute("user"))+" :</h3>");
    %>
    <form action="MainPageController" method="post">
        <input type="text" placeholder="enter the journal's name here:" name="journalName" required>
        <input type="text" placeholder="enter the summary:" name="summary" required>
        <input type="submit" id="addArticle" value="Add" >
    </form>
</div>

<div>


    <%
        int journalID = (int) request.getSession().getAttribute("journalid");
        if(journalID!=0){
        Statement st = null;
        Connection con = null;

        try {
            Class.forName("com.mysql.jdbc.Driver");
            con = (Connection) DriverManager.getConnection(
                    "jdbc:mysql://localhost:3306/journals", "root", "");
            st = con.createStatement();
            ResultSet rs = st.executeQuery("select * from articles where journalid = "+journalID+" and user ='"+(request.getSession().getAttribute("user"))+"';");

    %>
    <%
        out.println("<br><h3>The articles from you we have so far in journal:  "+(request.getSession().getAttribute("journal"))+" </h3><br>");
    %>
    <table style="text-align:center; width: 500px;">
        <thead>
        <tr>
            <th>ID</th>
            <th>USER</th>
            <th>JOURNALID</th>
            <th>DESCRIPTION</th>
            <th>DATE</th>
        </tr>
        </thead>
        <tbody>
        <%while (rs.next()) {
        %>
        <tr>
            <td><%=rs.getInt("id")%></td>
            <td><%=rs.getString("user")%></td>
            <td><%=rs.getInt("journalid")%></td>
            <td><%=rs.getString("summary")%></td>
            <td><%=rs.getDate("date")%></td>
        </tr>
        <%}%>
        </tbody>
    </table><br>
    <%} catch (Exception e) {
        out.print(e.getMessage());%><br><%
    } finally {
        st.close();
        con.close();
    }}
        else{
        out.println("<h3>The articles from you we have so far in journal:  "+(request.getSession().getAttribute("journal"))+" </h3><br>");
    }
%>
</div>
<%--<div>
    <%
        Statement st1 = null;
        Connection con1 = null;
        String u = (String) request.getSession().getAttribute("user");
        try {
            Class.forName("com.mysql.jdbc.Driver");
            con1 = (Connection) DriverManager.getConnection(
                    "jdbc:mysql://localhost:3306/football", "root", "");
            st1 = con1.createStatement();
            ResultSet rs = st1.executeQuery("select * from teams where members like '%"+u+"' or members like '+"+u+"%' or members like '%"+u+"%' ");

    %>
    <%
        out.println("<h3>The teams you are part of: </h3><br>");
    %>
    <table style="text-align:center; width: 500px;">
        <thead>
        <tr>
            <th>NAME</th>
        </tr>
        </thead>
        <tbody>
        <%while (rs.next()) {
        %>
        <tr>
            <td><%=rs.getString("name")%></td>
        </tr>
        <%}%>
        </tbody>
    </table><br>
    <%} catch (Exception e) {
        out.print(e.getMessage());%><br><%
    } finally {
        st1.close();
        con1.close();
    }
%>
</div>--%>

</body>
<script type="text/javascript"> window.onload = alertName; </script>
</html>