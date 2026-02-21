using System.Text.Json;
using Marten.Schema;

namespace Catalog.API.Data;

public class CatalogInitialData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellationToken)
    {
        using var session = store.LightweightSession();

        if (await session.Query<Product>().AnyAsync(cancellationToken))
            return;

        var products = await LoadProductsFromJson();
        session.Store(products);
        await session.SaveChangesAsync(cancellationToken);
    }

    private static async Task<IEnumerable<Product>> LoadProductsFromJson()
    {
        var path = Path.Combine(AppContext.BaseDirectory, "Data", "products.json");
        var json = await File.ReadAllTextAsync(path);

        return JsonSerializer.Deserialize<IEnumerable<Product>>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }) ?? [];
    }
}