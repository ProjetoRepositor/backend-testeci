namespace App.DAO;

using MongoDB.Driver;

public class BaseDAO
{
    // Get username and password from environment variable
    private static readonly string username = 
        Environment.GetEnvironmentVariable("MONGO_USERNAME") ?? "repositordev";
    private static readonly string password = 
        Environment.GetEnvironmentVariable("MONGO_PASSWORD") ?? "repositordev";
    private static readonly string url =
        Environment.GetEnvironmentVariable("MONGO_URL") ?? "repositordev.5zz8zz5.mongodb.net";

    private static readonly string ConnectionString = $"mongodb+srv://{username}:{password}@{url}/?retryWrites=true&w=majority";
    
    protected static MongoClient Client { get; set; } = new MongoClient(ConnectionString);

    public BaseDAO()
    {
        Client ??= new MongoClient(ConnectionString);
    }
}