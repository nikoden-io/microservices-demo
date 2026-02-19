namespace Catalog.API.Products.GetProducts;

public record GetProductResponse(IEnumerable<Product> Products);

public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async (ISender sender, CancellationToken ct) =>
            {
                var result = await sender.Send(new GetProductsQuery(), ct);
                var response = result.Adapt<GetProductResponse>();

                return Results.Ok(response);
            })
            .WithName("GetProducts")
            .Produces<GetProductResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Products")
            .WithDescription("Get Products");
    }
}