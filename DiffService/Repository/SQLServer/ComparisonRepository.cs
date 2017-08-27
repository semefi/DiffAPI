using Dapper;
using DiffAPI.Models;
using System.Data.SqlClient;
using System.Linq;

namespace DiffAPI.Repository.SQLServer
{
    public class ComparisonRepository : DbRepository, IComparisonRepository
    {
        public ComparisonRepository(string connectionString) : base(connectionString)
        {
        }

        public void Insert(Comparison data)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute("Insert into Comparison " +
                "(ComparisonId, [Left], [Right]) Values " +
                "(@ComparisonId, @Left, @Right); ", data);
            }
        }

        public Comparison Get(string id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                return connection.Query<Comparison>(
                    "Select ComparisonId, [Left], [Right] From Comparison " +
                    "Where ComparisonId = @id", new { id }).FirstOrDefault();
            }
        }

        public void Update(Comparison data)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute("Update Comparison SET " +
                                       "[Left] = @Left " +
                                       ",[Right] = @Right " +
                                       "Where ComparisonId = @ComparisonId", data);
            }
        }

    }
}