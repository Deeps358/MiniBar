﻿using Dapper;
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

        public async Task<Guid> CreateAsync(Drink drink, CancellationToken cancellationToken)
        {
            const string sql = """
                INSERT INTO drinks (name, description, user_id, category_id, tags)
                VALUES (@Id, @Name, @Description, @UserId, @CategoryId, @Tags)
                """;

            await using var connection = new NpgsqlConnection(_configuration.GetConnectionString("Database"));

            await connection.ExecuteAsync(sql, new
            {
                Id = drink.id,
                Name = drink.name,
                Description = drink.description,
                UserId = drink.userId,
                CategoryId = drink.categoryId,
                Tags = drink.tags,
            });

            return drink.id;
        }

        public Task<Guid> DeleteAsync(Guid drinkId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Drink?> GetByIdAsync(Guid drinkId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> UpdateAsync(Drink drink, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
