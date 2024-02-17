# QueryCraft

QueryCraft is a powerful library designed to seamlessly integrate JSON-based filter conditions into LINQ queries in your C# applications. With QueryCraft, you can easily transform complex filter criteria expressed in JSON format into expressive LINQ expressions (`Expression<Func<T, bool>>`) with just a few lines of code.

## Features

- **JSON-Based Filtering:** QueryCraft accepts JSON bodies containing filter conditions structured in a flexible and intuitive format, allowing you to express complex filtering logic concisely and clearly.
  
- **Dynamic Expression Building:** The library dynamically constructs LINQ expressions based on the provided JSON filters, enabling you to build dynamic queries that adapt to changing criteria at runtime.
  
- **Extensible Operator Support:** QueryCraft supports a wide range of operators, including equality (`eq`), inequality (`ne`), greater than (`gt`), less than (`lt`), greater than or equal to (`gte`), less than or equal to (`lte`), `in`, `not in` (`nin`), starts with (`startswith`), ends with (`endswith`), `between`, `is null` (`is null`), and more. This allows you to express diverse filtering conditions with ease.
  
- **Pagination and Sorting (Future Feature):** While currently focused on filtering, QueryCraft is designed with extensibility in mind and will soon support pagination and sorting, providing a comprehensive solution for querying and manipulating data.

# Getting Started with QueryCraft

To get started with QueryCraft, install the NuGet package in your C# project. To do this you can find it on nuget or using the following command:

```bash
dotnet add package QueryCraft
```
Then register it using di:
```csharp
builder.Services.RegisterQueryCraft(opt =>
{
    // you can add here your custom configs
});
```
Now, you can use it in your controllers or services:
```csharp
using Microsoft.AspNetCore.Mvc;
using QueryCraft.Interfaces;
using QueryCraft.SampleApp.Data;
using QueryCraft.SampleApp.Models;

namespace QueryCraft.SampleApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        public QueryCraftContext _dbContext;
        private IParser _parser;

        public ProductController(QueryCraftContext dbContext, IParser parser)
        {
            _parser = parser;
            _dbContext = dbContext;
        }

        [HttpPost(Name = "GetProducts")]
        public IActionResult Get(Dictionary<string, object> model)
        {
            try
            {
                var operand = _parser.Parse(model, typeof(Product));
                return Ok(_dbContext.Products.Where(operand.GetPredicate<Product>()));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
```
## Examples
Example of web api project that uses this package you can find here: https://github.com/lordoleksiy/QueryCraft.SampleApp

Also, possible operators and examples of request bodies you can find here: https://github.com/lordoleksiy/QueryCraft/blob/main/Operators.md
## Contributions and Feedback

We welcome contributions and feedback from the community to improve QueryCraft and make it even more powerful and versatile. If you encounter any issues, have ideas for improvements, or want to contribute to the project, please feel free to open an issue or submit a pull request on GitHub.

## License

QueryCraft is licensed under the MIT License, ensuring that you can use, modify, and distribute it freely in your projects. For more information, please refer to the LICENSE file in the repository.

## Roadmap

Our roadmap includes adding support for pagination and sorting, expanding operator support, and enhancing the library based on user feedback and requirements. Stay tuned for updates!

## About the Authors

QueryCraft is developed and maintained by a team of experienced developers passionate about simplifying data querying and manipulation in C# applications.

## Support and Contact

If you have any questions, need support, or want to get in touch with the QueryCraft team, feel free to reach out via GitHub issues or the contact information provided in the repository.
