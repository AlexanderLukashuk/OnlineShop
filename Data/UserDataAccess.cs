using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using OnlineShop.Models;

namespace OnlineShop.Data
{
    public class UserDataAccess : IConnection
    {
        public SqlConnection OpenConnection()
        {
            SqlConnection newConnection = new SqlConnection();
            newConnection.ConnectionString = "Server = localhost; Database = OnlineShopDatabase; Trusted_Connection = True;";
            newConnection.Open();

            return newConnection;
        }
        public void Create(User user)
        {
            /*SqlConnection connection = new SqlConnection();
            connection.ConnectionString = "Server = localhost; Database = OnlineShopDatabase; Trusted_Connection = True;";
            connection.Open();*/
            var connection = OpenConnection();

            SqlTransaction transaction = connection.BeginTransaction();

            SqlCommand command = new SqlCommand($"insert into Users(Id, Email, Password) values (@Id, @Email, @Password);", connection);

            SqlParameter idParameter = command.CreateParameter();
            idParameter.ParameterName = "Id";
            idParameter.Value = user.Id;

            command.Parameters.Add(idParameter);

            SqlParameter emailParameter = command.CreateParameter();
            emailParameter.ParameterName = "Email";
            emailParameter.Value = user.Email;

            command.Parameters.Add(emailParameter);

            SqlParameter passwordParameter = command.CreateParameter();
            passwordParameter.ParameterName = "Password";
            passwordParameter.Value = user.Password;

            command.Parameters.Add(passwordParameter);

            command.Transaction = transaction;

            try
            {
                command.ExecuteNonQuery();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }

            command.Dispose();
            transaction.Dispose();
            connection.Close();
        }

        public void ExecuteInTransaction(SqlConnection connection, params SqlCommand[] sqlCommands)
        {
            var transaction = connection.BeginTransaction();
            try
            {
                foreach (var command in sqlCommands)
                {
                    command.Transaction = transaction;
                    command.ExecuteNonQuery();
                }
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }

            transaction.Dispose();
        }

        public User Get(User user)
        {
            /*SqlConnection connection = new SqlConnection();
            connection.ConnectionString = "Server = localhost; Database = OnlineShopDatabase; Trusted_Connection = True;";
            connection.Open();*/
            var connection = OpenConnection();

            SqlCommand command = new SqlCommand($"select top 1 from Users where email='{user.Email}';", connection);

            SqlDataReader reader = command.ExecuteReader();

            User gotUser = new User();

            while (reader.Read())
            {
                gotUser.Id = Guid.Parse(reader["Id"].ToString());
                gotUser.Email = reader["Email"].ToString();
                gotUser.Phone = reader["Phone"].ToString();
                gotUser.Password = reader["Password"].ToString();
            }

            reader.Close();

            command.Dispose();

            connection.Close();

            return gotUser;
        }

        public List<User> GetAll()
        {
            List<User> users = new List<User>();
            var connection = OpenConnection();

            SqlCommand command = new SqlCommand($"select * from Users;", connection);
            
            SqlDataReader reader = command.ExecuteReader();

            User gotUser = new User();

            while (reader.Read())
            {
                gotUser.Id = Guid.Parse(reader["Id"].ToString());
                gotUser.Email = reader["Email"].ToString();
                gotUser.Phone = reader["Phone"].ToString();
                gotUser.Password = reader["Password"].ToString();

                users.Add(gotUser);
            }

            reader.Close();

            command.Dispose();

            connection.Close();

            return users;
        }

        public void Update(User user)
        {

        }

        public void Delete(Guid id)
        {

        }
    }
}