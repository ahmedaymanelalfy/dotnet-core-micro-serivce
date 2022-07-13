using Catalog.Api.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Catalog.Api.Data
{
    public class CatalogContext : ICatalogContext
    {
        public CatalogContext(IConfiguration Config)
        {
            var client = new MongoClient(Config.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(Config.GetValue<string>("DatabaseSettings:DatabaseName"));
            Products = database.GetCollection<Product>(Config.GetValue<string>("DatabaseSettings:CollectionName"));
            CatalogContextSeed.SeedData(Products);
        }
        public IMongoCollection<Product> Products { get; }
    }
}
