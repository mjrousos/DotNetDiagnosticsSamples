using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using TargetApp.Models;

namespace TargetApp
{
    public class CategoryRepository
    {
        private IDbConnection DbConnection => new SqlConnection(_dbOptions.ConnectionString);

        private readonly ProductDatabaseSettings _dbOptions;

        public CategoryRepository()
        {
            _dbOptions = new ProductDatabaseSettings { ConnectionString = ConfigurationManager.ConnectionStrings["ProductDatabase"].ConnectionString };
        }

        public IEnumerable<ProductCategory> GetAllCategories()
        {
            using (var db = DbConnection)
            {

                // Because the DB query is quick, add a Task.Delay to simulate other slower tasks
                Task.Delay(10).Wait();

                return db.Query<ProductCategory>(@"SELECT ProductCategoryID Id, Name, ParentProductCategoryID ParentId FROM SalesLT.ProductCategory");
            }
        }

        public async Task<IEnumerable<ProductCategory>> GetAllCategoriesAsync()
        {
            using (var db = DbConnection)
            {
                // Because the DB query is quick, add a Task.Delay to simulate other slower tasks
                await Task.Delay(10);

                return await db.QueryAsync<ProductCategory>("SELECT ProductCategoryID Id, Name, ParentProductCategoryID ParentId FROM SalesLT.ProductCategory");
            }
        }

        public ProductCategory GetCategory(int id)
        {
            using (var db = DbConnection)
            {
                return db.QueryFirstOrDefault<ProductCategory>("SELECT ProductCategoryID Id, Name, ParentProductCategoryID ParentId FROM SalesLT.ProductCategory WHERE ProductCategoryID=@id", new { id });
            }
        }

        public async Task<ProductCategory> GetCategoryAsync(int id)
        {
            using (var db = DbConnection)
            {
                return await db.QueryFirstOrDefaultAsync<ProductCategory>("SELECT ProductCategoryID Id, Name, ParentProductCategoryID ParentId FROM SalesLT.ProductCategory WHERE ProductCategoryID=@id", new { id });
            }
        }
    }
}
