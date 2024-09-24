using MongoDB.Driver;
using WebApplication_Services.Models;
namespace WebApplication_Services;


public class UserContext
{
    private readonly IMongoDatabase _database;
    public UserContext(string connectionString)
    {
        var client = new MongoClient(connectionString);
        _database = client.GetDatabase("Academy");

    }

    public IMongoCollection<User> Users => _database.GetCollection<User>("Users");
}
