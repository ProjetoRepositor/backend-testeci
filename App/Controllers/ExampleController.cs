namespace App.Controllers;

using Microsoft.AspNetCore.Mvc;
using App.Models;
using App.DAO;

[ApiController]
[Route("/api/v1/[controller]")]
public class ExampleController : ControllerBase
{
    private readonly ILogger<ExampleController> _logger;
    private readonly ExampleDAO ExampleDao;

    public ExampleController(ILogger<ExampleController> logger)
    {
        ExampleDao = new ExampleDAO();
        _logger = logger;
    }

    [HttpGet(Name = "GetExample")]
    [ProducesResponseType(typeof(IEnumerable<Example>), StatusCodes.Status200OK)]
    public async Task<IEnumerable<Example>> Get()
    {
        var examples = await ExampleDao.FindAll();
        return examples;
    }

    [HttpGet("{id}", Name = "GetExampleById")]
    [ProducesResponseType(typeof(Example), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<Example> Get(string id)
    {
        var example = await ExampleDao.FindOne(id);
        if (example == null)
        {
            Response.StatusCode = StatusCodes.Status404NotFound;
            return new Example();
        }
        return example;
    }

    [HttpPost(Name = "PostExample")]
    [ProducesResponseType(typeof(Example), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<Example> Post([FromBody] Example example)
    {
        if (example == null)
        {
            Response.StatusCode = StatusCodes.Status400BadRequest;
            return new Example();
        }
        example = await ExampleDao.Insert(example);
        Response.StatusCode = StatusCodes.Status201Created;
        return example;
    }

    [HttpPut("{id}", Name = "PutExample")]
    [ProducesResponseType(typeof(Example), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<Example> Put(string id, [FromBody] Example example)
    {
        if (example == null)
        {
            Response.StatusCode = StatusCodes.Status400BadRequest;
            return new Example();
        }
        example = await ExampleDao.Update(id, example);
        return example;
    }

    [HttpDelete("{id}", Name = "DeleteExample")]
    [ProducesResponseType(typeof(Example), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<Example> Delete(string id)
    {
        var example = await ExampleDao.Delete(id);
        return example;
    }
}
