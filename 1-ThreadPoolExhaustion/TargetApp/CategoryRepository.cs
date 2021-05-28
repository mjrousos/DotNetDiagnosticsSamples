using Dapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TargetApp.Models;

namespace TargetApp
{
    public class CategoryRepository
    {
        private IDbConnection DbConnection => new SqlConnection(_dbOptions.ConnectionString);

        private readonly ILogger<CategoryRepository> _logger;
        private readonly ProductDatabaseSettings _dbOptions;

        public CategoryRepository(IOptions<ProductDatabaseSettings> dbOptions, ILogger<CategoryRepository> logger)
        {
            _dbOptions = dbOptions?.Value ?? throw new ArgumentNullException(nameof(dbOptions)); ;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public IEnumerable<ProductCategory> GetAllCategories()
        {
            using var db = DbConnection;
            return db.Query<ProductCategory>("SELECT ProductCategoryID Id, Name, ParentProductCategoryID ParentId FROM SalesLT.ProductCategory");
        }

        public ProductCategory? GetCategory(int id)
        {
            using var db = DbConnection;
            return db.QueryFirstOrDefault<ProductCategory>("SELECT ProductCategoryID Id, Name, ParentProductCategoryID ParentId FROM SalesLT.ProductCategory WHERE ProductCategoryID=@id", new { id });
        }
    }
}
