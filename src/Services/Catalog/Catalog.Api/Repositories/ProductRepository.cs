using Catalog.Api.Data;
using Catalog.Api.Entities;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.Api.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _context;

        public ProductRepository(ICatalogContext context)
        {
            _context =  context;
        }
        public async Task CreateProduct(Product product)
        {
             await _context.Products.InsertOneAsync(product);
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(string category)
        {
            return await _context.Products.Find(x => x.Category == category).ToListAsync();
        }

        public async Task<Product> GetProductById(string id)
        {
            return await _context.Products.Find(x =>x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            return await _context.Products.Find(x => x.Name == name).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _context.Products.Find(x => true).ToListAsync();
        }

        public async Task<bool> UpdateProduct(Product product)
        {
           var result= await _context.Products.ReplaceOneAsync(filter : x=>x.Id == product.Id, replacement : product);
            return result.IsAcknowledged && result.ModifiedCount>0;
        }

        public async Task<bool> DeleteProduct(string Id)
        {
            var result = await _context.Products.DeleteOneAsync(filter: x=>x.Id == Id);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }
    }
}
