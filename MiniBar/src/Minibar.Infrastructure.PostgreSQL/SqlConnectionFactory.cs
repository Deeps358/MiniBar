using System.Data;
using Microsoft.Extensions.Configuration;
using Minibar.Application.Database;
using Npgsql;

namespace Minibar.Infrastructure.PostgreSQL
{
    public class SqlConnectionFactory : ISqlConnectionFactory
    {
        private readonly IConfiguration _configuration;

        public SqlConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection Create()
        {
            var connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            return connection;
        }
    }
}
