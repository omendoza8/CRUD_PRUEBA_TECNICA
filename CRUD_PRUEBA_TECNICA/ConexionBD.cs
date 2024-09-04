using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CRUD_PRUEBA_TECNICA
{
    internal class ConexionBD
    {
        MySqlConnection connection = new MySqlConnection();
        static string server = "localhost";
        static string database = "dbcrud";
        static string user = "root";
        static string password = "akt125sl";
        static string port = "3306";
        string credenciales = 
            $"server={server};" +
            $"port={port};" +
            $"user id={user};" +
            $"password={password};" +
            $"database={database};";

        public MySqlConnection enableConnection()
        {
            try
            {
                if (connection == null)
                {
                    connection = new MySqlConnection(credenciales);
                }
                else if (connection.State == ConnectionState.Closed)
                {
                    connection.ConnectionString = credenciales;
                }

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
            }
            catch (MySqlException e)
            {
                MessageBox.Show("Error en la conexión." + e.ToString());
            }

            return connection;
        }

        public void closeConnection()
        {
            if (connection != null && connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
    }
}
