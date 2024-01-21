# QueryCraft

QueryCraft is a powerful library designed to seamlessly integrate JSON-based filter conditions into LINQ queries in your C# applications. With QueryCraft, you can easily transform complex filter criteria expressed in JSON format into expressive LINQ expressions (`Expression<Func<T, bool>>`) with just a few lines of code.

## Features

- **JSON-Based Filtering:** QueryCraft accepts JSON bodies containing filter conditions structured in a flexible and intuitive format, allowing you to express complex filtering logic concisely and clearly.
  
- **Dynamic Expression Building:** The library dynamically constructs LINQ expressions based on the provided JSON filters, enabling you to build dynamic queries that adapt to changing criteria at runtime.
  
- **Extensible Operator Support:** QueryCraft supports a wide range of operators, including equality (`eq`), inequality (`ne`), greater than (`gt`), less than (`lt`), greater than or equal to (`gte`), less than or equal to (`lte`), `in`, `not in` (`nin`), starts with (`startswith`), ends with (`endswith`), `between`, `is null` (`is null`), and more. This allows you to express diverse filtering conditions with ease.
  
- **Pagination and Sorting (Future Feature):** While currently focused on filtering, QueryCraft is designed with extensibility in mind and will soon support pagination and sorting, providing a comprehensive solution for querying and manipulating data.

## Example Usage

```csharp
// Initialize QueryCraft with your JSON filter
var queryCraft = new QueryCraft(jsonFilter);

// Convert the JSON filter to a LINQ expression
Expression<Func<T, bool>> filterExpression = queryCraft.BuildExpression();

// Use the filter expression in your LINQ query
var filteredResults = dbContext.YourEntity.Where(filterExpression).ToList();

## Getting Started

To get started with QueryCraft, simply install the package from NuGet and follow the documentation provided in the repository. Sample code and comprehensive examples are available to guide you through the integration process.

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
