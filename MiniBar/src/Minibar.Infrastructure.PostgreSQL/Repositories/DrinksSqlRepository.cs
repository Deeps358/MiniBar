using Dapper;
using Microsoft.Extensions.Configuration;
using Minibar.Application.Drinks;
using Minibar.Entities.Drinks;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minibar.Infrastructure.PostgreSQL.Repositories
{
    public class DrinksSqlRepository : IDrinksRepository
    {
        private readonly IConfiguration _configuration;

        public DrinksSqlRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> CreateAsync(Drink drink, CancellationToken cancellationToken)
        {
            const string sql = """
                INSERT INTO drinks (name, description, user_id, category_id, tags)
                VALUES (@Id, @Name, @Description, @UserId, @CategoryId, @Tags)
                """;

            await using var connection = new NpgsqlConnection(_configuration.GetConnectionString("Database"));

            await connection.ExecuteAsync(sql, new
            {
                Id = drink.Id,
                Name = drink.Name,
                Description = drink.Description,
                UserId = drink.UserId,
                CategoryId = drink.CategoryId,
                Tags = drink.Tags,
            });

            return drink.Id;
        }

        public Task<Guid> DeleteAsync(Guid drinkId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Drink?> GetByIdAsync(int drinkId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Drink?> GetByNameAsync(string name, CancellationToken cancellationToken) => throw new NotImplementedException();

        public Task<Guid> UpdateAsync(Drink drink, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
