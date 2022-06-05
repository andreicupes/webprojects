using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MySql.Data.MySqlClient;
using A9.Models;

namespace A9.DataAbstractionLayer;

public class DAL
{
    public int Login(string username, string password)
    {
        MySql.Data.MySqlClient.MySqlConnection conn;
        string myConnectionString;

        myConnectionString = "server=localhost;uid=root;pwd=;database=logsdb;";

        try
        {
            conn = new MySql.Data.MySqlClient.MySqlConnection();
            conn.ConnectionString = myConnectionString;
            conn.Open();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select * from users where username='" + username + "' and password='" + password + "'";
            MySqlDataReader myreader = cmd.ExecuteReader();

            if (myreader.Read())
            {
                int userId = myreader.GetInt32("id");

                return userId;
            }
            return -1;

            myreader.Close();
        }
        catch (MySql.Data.MySqlClient.MySqlException ex)
        {
            Console.Write(ex.Message);
        }

        return -1;
    }
    public void DeleteLog(int logId)
    {
        MySql.Data.MySqlClient.MySqlConnection conn;
        string myConnectionString;

        myConnectionString = "server=localhost;uid=root;pwd=;database=logsdb; convert zero datetime=True";

        try
        {
            conn = new MySql.Data.MySqlClient.MySqlConnection();
            conn.ConnectionString = myConnectionString;
            conn.Open();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "delete from logreports where id=" + logId;
            cmd.ExecuteNonQuery();
        }
        catch (MySql.Data.MySqlClient.MySqlException ex)
        {
            Console.Write(ex.Message);
        }
    }
    public void InsertLog(string type, string severity, DateTime date, string log,int userId)
    {
        MySql.Data.MySqlClient.MySqlConnection conn;
        string myConnectionString;

        myConnectionString = "server=localhost;uid=root;pwd=;database=logsdb;convert zero datetime=True";

        try
        {
            conn = new MySql.Data.MySqlClient.MySqlConnection();
            conn.ConnectionString = myConnectionString;
            conn.Open();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select max(id) as maxId from logreports";
            MySqlDataReader myreader = cmd.ExecuteReader();

            int itemId = 1;
            if (myreader.Read())
            {
                itemId = myreader.GetInt32("maxId") + 1;
            }
            Console.WriteLine(itemId);
            myreader.Close();

            cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "insert into logreports(id,type,severity,date,log, userId) values (" + itemId + ", '" + type + "', '" +
                severity + "', '" + date + "', '" + log + "', " + userId +")";

            cmd.ExecuteNonQuery();
        }
        catch (MySql.Data.MySqlClient.MySqlException ex)
        {
            Console.Write(ex.Message);
        }
    }
    public List<LogReport> GetLogs()
    {
        MySql.Data.MySqlClient.MySqlConnection conn;
        string myConnectionString;

        myConnectionString = "server=localhost;uid=root;pwd=;database=logsdb; convert zero datetime=True";
        List<LogReport> llist = new List<LogReport>();

        try
        {
            conn = new MySql.Data.MySqlClient.MySqlConnection();
            conn.ConnectionString = myConnectionString;
            conn.Open();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select * from logreports";
            MySqlDataReader myreader = cmd.ExecuteReader();

            while (myreader.Read())
            {
                LogReport l = new LogReport();
                l.Id = myreader.GetInt32("id");
                l.Type = myreader.GetString("type");
                l.Severity = myreader.GetString("severity");
                l.Date = myreader.GetDateTime("date");
                l.UserID = myreader.GetInt32("userID");
                l.Log = myreader.GetString("log");
                llist.Add(l);
            }
            myreader.Close();
        }
        catch (MySql.Data.MySqlClient.MySqlException ex)
        {
            Console.Write(ex.Message);
        }
        return llist;
    }


public List<LogReport> GetMyLogs(int userId)
    {
        MySql.Data.MySqlClient.MySqlConnection conn;
        string myConnectionString;

        myConnectionString = "server=localhost;uid=root;pwd=;database=logsdb; convert zero datetime=True";
        List<LogReport> llist = new List<LogReport>();

        try
        {
            conn = new MySql.Data.MySqlClient.MySqlConnection();
            conn.ConnectionString = myConnectionString;
            conn.Open();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select * from logreports where userID=" + userId;
            MySqlDataReader myreader = cmd.ExecuteReader();

            while (myreader.Read())
            {
                LogReport l = new LogReport();
                l.Id = myreader.GetInt32("id");
                l.Type = myreader.GetString("type");
                l.Severity = myreader.GetString("severity");
                l.Date = myreader.GetDateTime("date");
                l.UserID = userId;
                l.Log = myreader.GetString("log");
                llist.Add(l);
            }
            myreader.Close();
        }
        catch (MySql.Data.MySqlClient.MySqlException ex)
        {
            Console.Write(ex.Message);
        }
        return llist;
    }
}