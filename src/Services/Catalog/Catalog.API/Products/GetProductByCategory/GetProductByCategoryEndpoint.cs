namespace Catalog.API.Products.GetProductByCategory;

public record GetProductByCategoryRequest(string Category);
public record GetProductByCategoryResponse(List<Product> Products);

public class GetProductBCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/category/{category}", async (string category, ISender sender, CancellationToken ct) =>
            {
                var result = await sender.Send(new GetProductByCategoryQuery(category), ct);
                var response = result.Adapt<GetProductByCategoryResponse>();

                return Results.Ok(response);
            })
            .WithName("GetProductByCategory")
            .Produces<GetProductByCategoryResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Product By Category")
            .WithDescription("Get Product By Category");
    }
}