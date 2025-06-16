namespace Ambev.DeveloperEvaluation.Application.Products.Queries.GetAllCategories;

public class GetAllCategoriesResult 
{
    public IEnumerable<string> Categories { get; set; } = Enumerable.Empty<string>();
}