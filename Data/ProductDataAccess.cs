using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.Common;
using Microsoft.Extensions.Configuration;

namespace OnlineShop.Data
{
    public class ProductDataAccess : IConnection
    {
        public DbProviderFactory CreateFactory()
        {
            DbProviderFactories.RegisterFactory("mssql", SqlClientFactory.Instance);
            DbProviderFactory factory = DbProviderFactories.GetFactory("mssql");

            return factory;
        }
        public DbConnection OpenConnection(DbProviderFactory factory, IConfigurationRoot configuration)
        {
            DbConnection newConnection = factory.CreateConnection();
            newConnection.ConnectionString = configuration.GetSection("connectionStrings")["onlineshop"];
            newConnection.Open();

            return newConnection;
        }
        public string GetConnectionString(IConfigurationRoot configuration)
        {
            var connectionString = configuration.GetSection("connectionStrings")["onlineshop"];

            return connectionString;
        }
        public void Create(Laptop product)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appSettings.json")
                .Build();

            /*var factory = CreateFactory();
            var connection = OpenConnection(factory, configuration);

            DbTransaction transaction = connection.BeginTransaction();

            DbCommand command = factory.CreateCommand();
            command.CommandText = $"insert into Product(Id, Name, Model, Manufacturer, Type, Title) values (@Id, @Name, @Model, @Manufacturer, @Type, @Title);";
            command.Connection = connection;

            DbParameter idParameter = command.CreateParameter();
            idParameter.ParameterName = "Id";
            idParameter.Value = product.Id;

            command.Parameters.Add(idParameter);

            DbParameter nameParameter = command.CreateParameter();
            nameParameter.ParameterName = "Name";
            nameParameter.Value = product.Name;

            command.Parameters.Add(nameParameter);

            DbParameter modelParameter = command.CreateParameter();
            modelParameter.ParameterName = "Model";
            modelParameter.Value = product.Model;

            command.Parameters.Add(modelParameter);

            DbParameter manufacturerParameter = command.CreateParameter();
            manufacturerParameter.ParameterName = "Manufacturer";
            manufacturerParameter.Value = product.Manufacturer;

            command.Parameters.Add(manufacturerParameter);

            DbParameter typeParameter = command.CreateParameter();
            typeParameter.ParameterName = "Type";
            typeParameter.Value = product.Type;

            command.Parameters.Add(typeParameter);

            DbParameter titleParameter = command.CreateParameter();
            titleParameter.ParameterName = "Title";
            titleParameter.Value = product.Title;

            command.Parameters.Add(titleParameter);

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
            connection.Close();*/

            using (var context = new ApplicationContext(GetConnectionString(configuration)))
            {
                context.Laptops.Add(product);
                context.SaveChanges();
            }
        }

        public Laptop Get(Laptop product)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appSettings.json")
                .Build();

            var factory = CreateFactory();
            var connection = OpenConnection(factory, configuration);

            DbCommand command = factory.CreateCommand();
            command.CommandText = $"select top 1 from Product where name='{product.Name}';";
            command.Connection = connection;

            DbDataReader reader = command.ExecuteReader();

            Laptop gotProduct = new Laptop();

            while (reader.Read())
            {
                gotProduct.Id = Guid.Parse(reader["Id"].ToString());
                gotProduct.Name = reader["Name"].ToString();
                gotProduct.Model = reader["Model"].ToString();
                gotProduct.Manufacturer = reader["Manufacturer"].ToString();
                gotProduct.Type = reader["Type"].ToString();
                gotProduct.Title = reader["Title"].ToString();
            }

            reader.Close();

            command.Dispose();

            connection.Close();

            return gotProduct;
        }

        public List<Laptop> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(Laptop product)
        {

        }

        public void Delete(Guid id)
        {

        }
    }
}