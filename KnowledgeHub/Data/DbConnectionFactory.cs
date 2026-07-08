using Npgsql;

namespace KnowledgeHub.Data
{
    public class DbConnectionFactory
    {
        private readonly IConfiguration _configuration;

        public DbConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public NpgsqlConnection CreateConnection()
        {
            return new NpgsqlConnection(
                _configuration.GetConnectionString("DefaultConnection")
            );
        }
    }
}