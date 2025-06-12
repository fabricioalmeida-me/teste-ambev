namespace Ambev.DeveloperEvaluation.Application.Products.Queries.GetAllCategoriesQuery;

public class GetAllCategoriesResult 
{
    public IEnumerable<string> Categories { get; set; } = Enumerable.Empty<string>();
}