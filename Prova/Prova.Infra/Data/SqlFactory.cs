using System.Data;
using MySql.Data.MySqlClient;

namespace Prova.Infra.Data
{
    public class SqlFactory
    {
        public IDbConnection SqlConnection()
        {
            return new MySqlConnection("Server=127.0.0.1;Port=3306;Database=prova;Uid=root;Pwd=1234;");
        }
    }
}