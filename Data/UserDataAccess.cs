using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using OnlineShop.Models;
using System.Data.Common;
using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace OnlineShop.Data
{
    public class UserDataAccess : IConnection
    {
        public DbProviderFactory CreateFactory()
        {
            DbProviderFactories.RegisterFactory("mssql", SqlClientFactory.Instance);
            DbProviderFactory factory = DbProviderFactories.GetFactory("mssql");

            return factory;
        }
        public DbConnection OpenConnection(DbProviderFactory factory, IConfigurationRoot configuration)
        {
            //var connectionString = configuration.GetSection("connectionStrings")["onlineshop"];

            DbConnection newConnection = factory.CreateConnection();
            //newConnection.ConnectionString = "Server = localhost; Database = OnlineShopDatabase; Trusted_Connection = True;";
            newConnection.ConnectionString = configuration.GetSection("connectionStrings")["onlineshop"];
            newConnection.Open();

            return newConnection;
        }

        public string GetConnectionString(IConfigurationRoot configuration)
        {
            var connectionString = configuration.GetSection("connectionStrings")["onlineshop"];

            return connectionString;
        }

        /*public void CreteParams(DbCommand command)
        {
            //Assembly myAssembly = Assembly.LoadFrom("/Users/sanya/csharp_study/csharp_source/ado.net/OnlineShop/Models/User.cs");
            Type myType = Type.GetType("/Users/sanya/csharp_study/csharp_source/ado.net/OnlineShop/Models/User.cs", false, true);
            string columnName;
            foreach (MemberInfo info in myType.GetMembers())
            {
                if (info.MemberType == MemberTypes.Property)
                {
                    columnName = typeof(info.MemberType);
                }
                //Console.WriteLine($"{mi.DeclaringType} {mi.MemberType} {mi.Name}");
            }
        }*/
        public void Create(User user, Profile profile)
        {
            /*SqlConnection connection = new SqlConnection();
            connection.ConnectionString = "Server = localhost; Database = OnlineShopDatabase; Trusted_Connection = True;";
            connection.Open();*/

            /*DbProviderFactories.RegisterFactory("mssql", SqlClientFactory.Instance);
            DbProviderFactory factory = DbProviderFactories.GetFactory("mssql");
            DbConnection connection = factory.CreateConnection();
            connection.ConnectionString = "Server = localhost; Database = OnlineShopDatabase; Trusted_Connection = True;";
            connection.Open();*/
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appSettings.json")
                .Build();

            /*var factory = CreateFactory();
            var connection = OpenConnection(factory, configuration);
            //var connection = OpenConnection();

            DbTransaction transaction = connection.BeginTransaction();

            DbCommand command = factory.CreateCommand();
            command.CommandText = $"insert into Users(Id, Email, Password) values (@Id, @Email, @Password);";
            command.Connection = connection;

            DbParameter idParameter = command.CreateParameter();
            idParameter.ParameterName = "Id";
            idParameter.Value = user.Id;

            command.Parameters.Add(idParameter);

            DbParameter emailParameter = command.CreateParameter();
            emailParameter.ParameterName = "Email";
            emailParameter.Value = user.Email;

            command.Parameters.Add(emailParameter);

            DbParameter passwordParameter = command.CreateParameter();
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
            }*/

            using (var context = new ApplicationContext(GetConnectionString(configuration)))
            {
                context.Users.Add(user);
                context.Profiles.Add(profile);
                context.SaveChanges();
            }

            //command.Dispose();
            //transaction.Dispose();
            //connection.Close();
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
            //var connection = OpenConnection();
             var configuration = new ConfigurationBuilder()
                .AddJsonFile("appSettings.json")
                .Build();

            var factory = CreateFactory();
            var connection = OpenConnection(factory, configuration);

            //DbCommand command = new SqlCommand($"select top 1 from Users where email='{user.Email}';", connection);
            DbCommand command = factory.CreateCommand();
            command.CommandText = $"select top 1 from Users where email='{user.Email}';";
            command.Connection = connection;

            DbDataReader reader = command.ExecuteReader();

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
            //var connection = OpenConnection();
             var configuration = new ConfigurationBuilder()
                .AddJsonFile("appSettings.json")
                .Build();

            var factory = CreateFactory();
            var connection = OpenConnection(factory, configuration);

            DbCommand command = factory.CreateCommand();
            command.CommandText = $"select * from Users;";
            command.Connection = connection;

            DbDataReader reader = command.ExecuteReader();

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