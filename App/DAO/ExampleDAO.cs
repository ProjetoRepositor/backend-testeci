namespace App.DAO;

using App.Models;
using MongoDB.Driver;

public class ExampleDAO : BaseDAO
{
    private IMongoCollection<Example> Collection { get; set; }
    private IMongoDatabase Database { get; set; }

    public ExampleDAO() : base()
    {
        Database ??= Client.GetDatabase("example");
        Collection ??= Database.GetCollection<Example>("example");
    }

    public async Task<IEnumerable<Example>> FindAll()
    {
        var queryResult = await Collection.FindAsync(p => true);
        return queryResult.ToList();
    }

    public async Task<Example> FindOne(string id)
    {
        var queryResult = await Collection.FindAsync(p => p._id == id);
        return queryResult.FirstOrDefault();
    }

    public async Task<Example> Insert(Example example)
    {
        await Collection.InsertOneAsync(example);
        return example;
    }

    public async Task<Example> Update(string id, Example example)
    {
        example._id = id;
        await Collection.ReplaceOneAsync(p => p._id == id, example);
        return example;
    }

    public async Task<Example> Delete(string id)
    {
        var example = await FindOne(id);
        await Collection.DeleteOneAsync(p => p._id == id);
        return example;
    }

}