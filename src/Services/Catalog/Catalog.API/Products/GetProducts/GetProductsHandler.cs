namespace Catalog.API.Products.GetProducts;

public record GetProductsQuery : IRequest<GetProductsResult>;

public record GetProductsResult(IEnumerable<Product> Products);

public class GetProductsQueryHandler(IDocumentSession session, ILogger<GetProductsQueryHandler> logger)
    : IRequestHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProductsQueryHandler. Handler called with request: {@query}");
        var products = await session.Query<Product>().ToListAsync(cancellationToken);
        return new GetProductsResult(products);
    }
}