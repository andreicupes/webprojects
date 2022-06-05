
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
</head>
<body>
<div>
    <%
        out.println("<h3>Welcome to the forum, " + (request.getSession().getAttribute("user")) + "! </h3>");
    %>
</div>
<div>
    <form action="MainPageController" method="get">
        <input type="text" placeholder="enter the player's name here:" name="player" required>
        <input type="text" placeholder="enter the teams here (separate multiple teams by commas:" name="teams" required>
        <input type="submit" id="newPlayer" >
    </form>
</div>

<div>


    <%
        Statement st = null;
        Connection con = null;
        try {
            Class.forName("com.mysql.jdbc.Driver");
            con = (Connection) DriverManager.getConnection(
                    "jdbc:mysql://localhost:3306/football", "root", "");
            st = con.createStatement();
            ResultSet rs = st.executeQuery("select * from teams;");

    %>
    <%
        out.println("<h3>The teams we have so far: </h3><br>");
    %>
    <table style="text-align:center; width: 500px;">
        <thead>
        <tr>
            <th>ID</th>
            <th>CAPTAIN</th>
            <th>NAME</th>
            <th>DESCRIPTION</th>
            <th>MEMBERS</th>
        </tr>
        </thead>
        <tbody>
        <%while (rs.next()) {
        %>
        <tr>
            <td><%=rs.getInt("id")%></td>
            <td><%=rs.getInt("captainID")%></td>
            <td><%=rs.getString("name")%></td>
            <td><%=rs.getString("description")%></td>
            <td><%=rs.getString("members")%></td>
        </tr>
        <%}%>
        </tbody>
    </table><br>
    <%} catch (Exception e) {
        out.print(e.getMessage());%><br><%
    } finally {
        st.close();
        con.close();
    }
%>
</div>
<div>
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
</div>

</body>
</html>