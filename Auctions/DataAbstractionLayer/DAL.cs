using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MySql.Data.MySqlClient;
using Auctions.Models;
namespace Auctions.DataAbstractionLayer;

public class DAL
{
    public List<Auction> GetOthersAuctions(string user)
    {
        MySql.Data.MySqlClient.MySqlConnection conn;
        string myConnectionString;

        myConnectionString = "server=localhost;uid=root;pwd=;database=auctions;convert zero datetime=True";
        List<Auction> llist = new List<Auction>();

        try
        {
            conn = new MySql.Data.MySqlClient.MySqlConnection();
            conn.ConnectionString = myConnectionString;
            conn.Open();
            
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select * from auction except select * from auction where user='"+user+"'";
            MySqlDataReader myreader = cmd.ExecuteReader();

            while (myreader.Read())
            {
                Auction l = new Auction();
                l.Id = myreader.GetInt32("id");
                l.User = myreader.GetString("user");
                l.CategoryId = myreader.GetInt32("categoryID");
                l.Description = myreader.GetString("description");
                l.Date = DateOnly.FromDateTime(myreader.GetDateTime("date"));


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

    public int getCategoryId(string catName)
    {
        MySql.Data.MySqlClient.MySqlConnection conn;
        string myConnectionString;

        myConnectionString = "server=localhost;uid=root;pwd=;database=auctions;";
        try
        {
            conn = new MySql.Data.MySqlClient.MySqlConnection();
            conn.ConnectionString = myConnectionString;
            conn.Open();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select id from Category where name = '" + catName + "'";
            MySqlDataReader myreader = cmd.ExecuteReader();

            int itemId = -1;
            if (myreader.Read())
            {
                itemId = myreader.GetInt32("id");
            }
            Console.WriteLine(itemId);
            myreader.Close();

            return itemId;
        }
        catch (MySql.Data.MySqlClient.MySqlException ex)
        {
            Console.Write(ex.Message);
            return -1;
        }
    }

    public void InsertCategory(string name)
    {
        MySql.Data.MySqlClient.MySqlConnection conn;
        string myConnectionString;

        myConnectionString = "server=localhost;uid=root;pwd=;database=auctions;";
        try
        {
            conn = new MySql.Data.MySqlClient.MySqlConnection();
            conn.ConnectionString = myConnectionString;
            conn.Open();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select max(id) as maxId from Category";
            MySqlDataReader myreader = cmd.ExecuteReader();

            int itemId = 1;
            if (myreader.Read() && myreader["maxId"].GetType() != typeof(DBNull))
            {
                itemId = myreader.GetInt32("maxId") + 1;
            }
            Console.WriteLine(itemId);
            myreader.Close();

            cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "insert into Category(id,name) values (" + itemId + ", '" + name + "')";

            cmd.ExecuteNonQuery();

        }
        catch (MySql.Data.MySqlClient.MySqlException ex)
        {
            Console.Write(ex.Message);

        }
    }


    public void InsertAuction(string? username, string catName, string description, DateOnly date)
    {
        MySql.Data.MySqlClient.MySqlConnection conn;
        string myConnectionString;

        myConnectionString = "server=localhost;uid=root;pwd=;database=auctions;";
        try
        {
            conn = new MySql.Data.MySqlClient.MySqlConnection();
            conn.ConnectionString = myConnectionString;
            conn.Open();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select max(id) as maxId from Auction";
            MySqlDataReader myreader = cmd.ExecuteReader();

            int itemId = 1;
            if (myreader.Read() && myreader["maxId"].GetType()!= typeof(DBNull))
            {
                itemId = myreader.GetInt32("maxId") + 1;
            }

            Console.WriteLine(itemId);
            myreader.Close();

            int catId = this.getCategoryId(catName);
            if (catId == -1) { this.InsertCategory(catName);catId = this.getCategoryId(catName); }

                cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "insert into Auction(id,user,categoryID,description,date) values (" + itemId + ", '" + username + "', " +
                catId + ", '" + description + "', '" + date.ToString("yyyy-MM-dd") + "')";

            cmd.ExecuteNonQuery();
        }
        catch (MySql.Data.MySqlClient.MySqlException ex)
        {
            Console.Write(ex.Message);
        }


    }

    public void UpdateAuction(int id, string description, DateOnly date)
    {
        MySql.Data.MySqlClient.MySqlConnection conn;
        string myConnectionString;

        myConnectionString = "server=localhost;uid=root;pwd=;database=auctions;";
        try
        {
            conn = new MySql.Data.MySqlClient.MySqlConnection();
            conn.ConnectionString = myConnectionString;
            conn.Open();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "update Auction set description='" + description + "', date='" + date.ToString("yyyy-MM-dd") + "'"+
                "where id ="+id;

            cmd.ExecuteNonQuery();
        }
        catch (MySql.Data.MySqlClient.MySqlException ex)
        {
            Console.Write(ex.Message);
        }


    }

    public List<Auction> GetAuctions()
    {
        MySql.Data.MySqlClient.MySqlConnection conn;
        string myConnectionString;

        myConnectionString = "server=localhost;uid=root;pwd=;database=auctions;convert zero datetime=True";
        List<Auction> llist = new List<Auction>();

        try
        {
            conn = new MySql.Data.MySqlClient.MySqlConnection();
            conn.ConnectionString = myConnectionString;
            conn.Open();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select * from auction";
            MySqlDataReader myreader = cmd.ExecuteReader();

            while (myreader.Read())
            {
                Auction l = new Auction();
                l.Id = myreader.GetInt32("id");
                l.User = myreader.GetString("user");
                l.CategoryId = myreader.GetInt32("categoryID");
                l.Description = myreader.GetString("description");
                l.Date = DateOnly.FromDateTime(myreader.GetDateTime("date"));


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

    public List<Auction> GetChanges(string cond)
    {
        MySql.Data.MySqlClient.MySqlConnection conn;
        string myConnectionString;

        myConnectionString = "server=localhost;uid=root;pwd=;database=auctions;convert zero datetime=True";
        List<Auction> llist = new List<Auction>();

        try
        {
            conn = new MySql.Data.MySqlClient.MySqlConnection();
            conn.ConnectionString = myConnectionString;
            conn.Open();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select * from auction where id in "+cond;
            MySqlDataReader myreader = cmd.ExecuteReader();

            while (myreader.Read())
            {
                Auction l = new Auction();
                l.Id = myreader.GetInt32("id");
                l.User = myreader.GetString("user");
                l.CategoryId = myreader.GetInt32("categoryID");
                l.Description = myreader.GetString("description");
                l.Date = DateOnly.FromDateTime(myreader.GetDateTime("date"));


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