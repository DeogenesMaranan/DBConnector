using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using DotNetEnv;

namespace DBConnector
{
    internal abstract class Base
    {
        protected readonly string _connectionString;

        protected Base()
        {
            Env.Load();
            _connectionString = ConstructConnectionString();
        }

        private string ConstructConnectionString()
        {
            string server = Environment.GetEnvironmentVariable("DB_SERVER");
            string port = Environment.GetEnvironmentVariable("DB_PORT");
            string database = Environment.GetEnvironmentVariable("DB_DATABASE");
            string username = Environment.GetEnvironmentVariable("DB_USERNAME");
            string password = Environment.GetEnvironmentVariable("DB_PASSWORD");
            return $"Server={server};Port={port};Database={database};Uid={username};Pwd={password};";
        }

        protected bool ExecuteNonQuery(string query, params MySqlParameter[] parameters)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                using (var command = new MySqlCommand(query, connection))
                {
                    connection.Open();
                    command.Parameters.AddRange(parameters);
                    command.ExecuteNonQuery();
                }
                MessageBox.Show("Operation completed successfully!");
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
                return false;
            }
        }

        protected T ExecuteScalar<T>(string query, params MySqlParameter[] parameters)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                using (var command = new MySqlCommand(query, connection))
                {
                    connection.Open();
                    command.Parameters.AddRange(parameters);
                    var result = command.ExecuteScalar();
                    return (result == DBNull.Value) ? default(T) : (T)Convert.ChangeType(result, typeof(T));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
                return default(T);
            }
        }

        protected List<T> ExecuteQuery<T>(string query, Func<MySqlDataReader, T> mapFunction, params MySqlParameter[] parameters)
        {
            var resultList = new List<T>();

            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                using (var command = new MySqlCommand(query, connection))
                {
                    connection.Open();
                    command.Parameters.AddRange(parameters);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var result = mapFunction(reader);
                            resultList.Add(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }

            return resultList;
        }
    }
}
