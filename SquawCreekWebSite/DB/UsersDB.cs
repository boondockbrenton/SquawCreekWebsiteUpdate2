
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SquawCreekWebSite.Models;

namespace SquawCreekWebSite.DB
{
    public class UsersDB : DatabaseLayer
    {
        public bool VerifyUserPass(string user,string password)
        {
            Int32 count = 0;

            string query = "select count(userID) from users where userName = @userName and password = SHA1(@password)";

            //Open connection
            if (this.OpenDBConnection() == true)
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, dbConnection);
                    cmd.Parameters.AddWithValue("@userName", user);
                    cmd.Parameters.AddWithValue("@password", password);
                    count = Int32.Parse(cmd.ExecuteScalar().ToString());                    
                    this.CloseDBConnection();
                }
                catch (MySqlException ex)
                {
                    return false;
                }

                return (count > 0 ? true : false);
            }
            else
            {
                return false;
            }
        }
    }
}