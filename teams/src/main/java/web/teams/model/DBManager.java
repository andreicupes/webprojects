package web.teams.model;
import com.mysql.cj.util.StringUtils;
import web.teams.domain.*;

import java.sql.*;
import java.util.ArrayList;
import java.util.List;
import java.util.Objects;

public class DBManager {
    private Statement stmt;
    private static Connection connection;

    public DBManager() {
        connect();
    }
    public static void connect() {
        if(connection == null) {
            String url = "jdbc:mysql://localhost:3306/football";
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

    public boolean checkPlayer(String name){
        try{
            String sql = "SELECT * FROM players WHERE name = '" + name + "'";

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
    public boolean checkTeam(String name){
        try{
            String sql = "SELECT * FROM teams WHERE name = '" + name + "'";

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
    public boolean checkPlayerInTeam(String pl,String tname){
        try{
            String sql = "select * from teams where (members like '%"+pl+"' or members like '+"+pl+"%' or members like '%"+pl+"%') and name = '"+tname+"'";

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

    public void addPlayerToPlayers(String name) {
        try {
            if(!checkPlayer(name)){
            String sql = "INSERT INTO players(name) VALUES ('"+name+"')";

            PreparedStatement stmt = getConnection().prepareStatement(sql);
            stmt.executeUpdate();
            }
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }

    public void addPlayerToTeams(String plname, String teams){
        try {
            if(checkPlayer(plname)){

                String[] tokens = teams.split(",");

                for (String team : tokens)
                {

                    if(!checkTeam(team)){
                        String sql = "INSERT INTO teams(name) VALUES ('"+team+"')";

                        PreparedStatement stmt = getConnection().prepareStatement(sql);
                        stmt.executeUpdate();
                    }
                    if(!checkPlayerInTeam(plname,team)) {
                        String sql2 = "SELECT members FROM teams WHERE name = '" + team + "'";

                        PreparedStatement stmt = getConnection().prepareStatement(sql2);
                        ResultSet rs = stmt.executeQuery();
                        if (rs.next()) {
                            String m = rs.getString(1);
                            if(m == null) m="";
                            else m += ",";
                            m += plname;
                            String sql1 = "UPDATE teams set members= '" + m + "' where name ='" + team + "'";

                            PreparedStatement stmt1 = getConnection().prepareStatement(sql1);
                            stmt1.executeUpdate();
                        }
                        rs.close();

                    }



                }

            }
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }
}
