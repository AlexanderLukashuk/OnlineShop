using System.Data.SqlClient;
using System.Data.Common;
using Microsoft.Extensions.Configuration;

namespace OnlineShop.Data
{
    public interface IConnection
    {
        public DbConnection OpenConnection(DbProviderFactory factory, IConfigurationRoot configuration);
        public DbProviderFactory CreateFactory();
    }
}