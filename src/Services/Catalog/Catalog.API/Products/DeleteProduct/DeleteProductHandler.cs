namespace Catalog.API.Products.DeleteProduct;

public record DeleteProductCommand(Guid ProductId) : ICommand<DeleteProductResult>;
public record DeleteProductResult(bool IsSuccess);

public class DeleteProductValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty().WithMessage("Product ID is required");
    }
}

public class DeleteProductHandler(IDocumentSession session, ILogger<DeleteProductHandler> logger) : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("DeleteProductHandler called with: {@Command}", command);

        var product = await session.LoadAsync<Product>(command.ProductId, cancellationToken);

        if (product is null)
            throw new ProductNotFoundException(command.ProductId);

        session.Delete<Product>(command.ProductId);
        await session.SaveChangesAsync(cancellationToken);

        return new DeleteProductResult(true);
    }
}