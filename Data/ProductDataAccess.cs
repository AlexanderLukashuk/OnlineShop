using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace OnlineShop.Data
{
    public class ProductDataAccess : IConnection
    {
        public SqlConnection OpenConnection()
        {
            SqlConnection newConnection = new SqlConnection();
            newConnection.ConnectionString = "Server = localhost; Database = OnlineShopDatabase; Trusted_Connection = True;";
            newConnection.Open();

            return newConnection;
        }
        public void Create(Laptop product)
        {
            var connection = OpenConnection();

            SqlTransaction transaction = connection.BeginTransaction();

            SqlCommand command = new SqlCommand($"insert into Product(Id, Name, Model, Manufacturer, Type, Title) values (@Id, @Name, @Model, @Manufacturer, @Type, @Title);", connection);

            SqlParameter idParameter = command.CreateParameter();
            idParameter.ParameterName = "Id";
            idParameter.Value = product.Id;

            command.Parameters.Add(idParameter);

            SqlParameter nameParameter = command.CreateParameter();
            nameParameter.ParameterName = "Name";
            nameParameter.Value = product.Name;

            command.Parameters.Add(nameParameter);

            SqlParameter modelParameter = command.CreateParameter();
            modelParameter.ParameterName = "Model";
            modelParameter.Value = product.Model;

            command.Parameters.Add(modelParameter);

            SqlParameter manufacturerParameter = command.CreateParameter();
            manufacturerParameter.ParameterName = "Manufacturer";
            manufacturerParameter.Value = product.Manufacturer;

            command.Parameters.Add(manufacturerParameter);

            SqlParameter typeParameter = command.CreateParameter();
            typeParameter.ParameterName = "Type";
            typeParameter.Value = product.Type;

            command.Parameters.Add(typeParameter);

            SqlParameter titleParameter = command.CreateParameter();
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
            connection.Close();
        }

        public Laptop Get(Laptop product)
        {
            var connection = OpenConnection();

            SqlCommand command = new SqlCommand($"select top 1 from Product where name='{product.Name}';", connection);

            SqlDataReader reader = command.ExecuteReader();

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