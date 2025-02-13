using APInet.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Net.WebSockets;

namespace APInet.Data
{
    public class MongoCRUD
    {
        private IMongoDatabase db;

        public MongoCRUD(string database)
        {
            var client = new MongoClient("mongodb+srv://Admin:admin@isabellasapi.wfmr9.mongodb.net/?retryWrites=true&w=majority&appName=IsabellasAPI");
            db = client.GetDatabase(database);
        }

        //Add product
        public async Task<List<Products>> AddProduct(string table, Products product)
        {
            var collection = db.GetCollection<Products>(table);
            await collection.InsertOneAsync(product);
            return collection.AsQueryable().ToList();
        }

        //Get all products
        public async Task<List<Products>> GetAllProducts(string table)
        {
            var collection = db.GetCollection<Products>(table);
            var products = await collection.AsQueryable().ToListAsync();
            return products;
        }

        //Get product by Id
        public async Task<Products> GetProductsById(string id)
        {
            var collection = db.GetCollection<Products>("Products");
            var product = await collection.FindAsync(x => x.Id == id);
            return await product.FirstOrDefaultAsync();
        }

        //Update product
        public async Task<Products> UpdatedProduct(string table, Products product)
        {
            var collection = db.GetCollection<Products>(table);
            product.Brand = "Updated product";
            await collection.ReplaceOneAsync(x => x.Id == product.Id, product, new ReplaceOptions { IsUpsert = true });
            return product;
        }

        //Delete product
        public async Task<string> DeleteProduct(string table, string id)
        {
            var collection = db.GetCollection<Products>(table);
            var product = await collection.DeleteOneAsync(x => x.Id == id);
            return "Product deleted";

        }
    }
}
