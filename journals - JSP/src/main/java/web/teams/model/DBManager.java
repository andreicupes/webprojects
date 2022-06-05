package web.teams.model;
import com.mysql.cj.util.StringUtils;
import web.teams.domain.*;

import java.sql.*;
import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;
import java.util.ArrayList;
import java.util.List;
import java.util.Objects;
import java.util.stream.Collectors;

public class DBManager {
    private Statement stmt;
    private static Connection connection;

    public DBManager() {
        connect();
    }
    public static void connect() {
        if(connection == null) {
            String url = "jdbc:mysql://localhost:3306/journals";
            try{
                Class.forName( "com.mysql.jdbc.Driver" );
                connection = DriverManager.getConnection( url, "root", "" );
            } catch (Exception e) {
                e.printStackTrace();
            }
        }
    }

    public static Connection getConnection() {
        if(connection == null)
            connect();
        return connection;
    }



/*    public boolean checkArticle(){
        try{
            String sql = "SELECT * FROM Articles WHERE name = '" + name + "'";

            PreparedStatement stmt = getConnection().prepareStatement(sql);
            ResultSet rs = stmt.executeQuery();
            if (rs.next()) {
                return true;
            }
            rs.close();
            return false;
        }
        catch (SQLException e) {
            e.printStackTrace();
            return false;
        }
    }*/

    public int getJournalID(String name){
        try{
            String sql = "SELECT id FROM journals WHERE name = '" + name + "'";

            PreparedStatement stmt = getConnection().prepareStatement(sql);
            ResultSet rs = stmt.executeQuery();
            if (rs.next()) {
                return rs.getInt(1);
            }
            rs.close();
            return 0;
        }
        catch (SQLException e) {
            e.printStackTrace();
            return 0;
        }
    }
    public boolean checkJournal(String name){
        try{
            String sql = "SELECT * FROM journals WHERE name = '" + name + "'";

            PreparedStatement stmt = getConnection().prepareStatement(sql);
            ResultSet rs = stmt.executeQuery();
            if (rs.next()) {
                return true;
            }
            rs.close();
            return false;
        }
        catch (SQLException e) {
            e.printStackTrace();
            return false;
        }
    }

    public int getNextJID(){
        try {
            String sql = "SELECT id FROM journals ORDER BY id DESC LIMIT 1;";

            PreparedStatement stmt = getConnection().prepareStatement(sql);
            ResultSet rs = stmt.executeQuery();
            int value = 0;
            if (rs.next())
                value = rs.getInt(1);
            rs.close();


            value+=1;
            return value;
        } catch (SQLException e) {
            e.printStackTrace();
        }
        return 0;
    }
    public void addJournalToJournals(String name) {
        try {
            if(!checkJournal(name)){
                int id= getNextJID();
            String sql = "INSERT INTO journals VALUES ("+id+",'"+name+"')";

            PreparedStatement stmt = getConnection().prepareStatement(sql);
            stmt.executeUpdate();
            }
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }
    public int getNextAID(){
        try {
            String sql = "SELECT id FROM articles ORDER BY id DESC LIMIT 1;";

            PreparedStatement stmt = getConnection().prepareStatement(sql);
            ResultSet rs = stmt.executeQuery();
            int value = 0;
            if (rs.next())
                value = rs.getInt(1);
            rs.close();


            value+=1;
            return value;
        } catch (SQLException e) {
            e.printStackTrace();
        }
        return 0;
    }

    public void addArticleToJournals(String user,String jname, String summary){
        try {
                addJournalToJournals(jname);
                int id= getNextAID();
                int jid = getJournalID(jname);
                String date = LocalDateTime.now().format(DateTimeFormatter.ofPattern("yyyy-MM-dd"));
                String sql = "INSERT INTO articles VALUES ("+id+",'"+user+"',"+jid+",'"+summary+"','"+date+"');";
                System.out.println(sql);
                PreparedStatement stmt = getConnection().prepareStatement(sql);
                stmt.executeUpdate();
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }

    public String getChanges(String myuser){
        try {
            String sql = "SELECT id FROM articles except select id from articles where user ='"+myuser+"';";

            PreparedStatement stmt = getConnection().prepareStatement(sql);
            ResultSet rs = stmt.executeQuery();
            List<Integer> myList = new ArrayList<Integer>();
            while (rs.next())
                myList.add(rs.getInt(1));
            rs.close();

            String numberString = myList.stream().map(String::valueOf)
                    .collect(Collectors.joining(","));

            return numberString;
        } catch (SQLException e) {
            e.printStackTrace();
        }
        return "";
    }

    public String getDiff(String interv){
        try {
            String sql = "SELECT * FROM articles where id in "+interv;

            PreparedStatement stmt = getConnection().prepareStatement(sql);
            ResultSet rs = stmt.executeQuery();
            String c="";
             while (rs.next()) {
            c+=rs.getInt("id");
            c+=" | ";
                 c+=rs.getString("user");
                 c+=" | ";
                 c+=rs.getInt("journalid");
                 c+=" | ";
                 c+=rs.getString("summary");
                 c+=" | ";
                 c+=rs.getDate("date");
                 c+=" ||| ";
             }

            return c;
        } catch (SQLException e) {
            e.printStackTrace();
        }
        return "no changes were found";
    }
//
//    public void addPlayerToTeams(String plname, String teams){
//        try {
//            if(checkPlayer(plname)){
//
//                String[] tokens = teams.split(",");
//
//                for (String team : tokens)
//                {
//
//                    if(!checkTeam(team)){
//                        String sql = "INSERT INTO teams(name) VALUES ('"+team+"')";
//
//                        PreparedStatement stmt = getConnection().prepareStatement(sql);
//                        stmt.executeUpdate();
//                    }
//                    if(!checkPlayerInTeam(plname,team)) {
//                        String sql2 = "SELECT members FROM teams WHERE name = '" + team + "'";
//
//                        PreparedStatement stmt = getConnection().prepareStatement(sql2);
//                        ResultSet rs = stmt.executeQuery();
//                        if (rs.next()) {
//                            String m = rs.getString(1);
//                            if(m == null) m="";
//                            else m += ",";
//                            m += plname;
//                            String sql1 = "UPDATE teams set members= '" + m + "' where name ='" + team + "'";
//
//                            PreparedStatement stmt1 = getConnection().prepareStatement(sql1);
//                            stmt1.executeUpdate();
//                        }
//                        rs.close();
//
//                    }
//
//
//
//                }
//
//            }
//        } catch (SQLException e) {
//            e.printStackTrace();
//        }
//    }
}
