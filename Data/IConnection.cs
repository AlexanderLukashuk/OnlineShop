using System.Data.SqlClient;

namespace OnlineShop.Data
{
    public interface IConnection
    {
        public SqlConnection OpenConnection();
    }
}