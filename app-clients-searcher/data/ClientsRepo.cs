using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using MySql.Data.MySqlClient;

namespace data
{
    public class ClientsRepo
    {
        public async Task<IEnumerable<Client>> GetAll()
        {
            using (var connection = new MySqlConnection(RuntimeConfig.ClientsDbConnection))
            {
                return await connection.QueryAsync<Client>("Select * from client");
            }
        }
    }

    public class Client
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
