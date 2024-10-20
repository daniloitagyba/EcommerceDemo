using WebAppComponents.Catalog;

namespace WebAppComponents.Item;

public static class ItemHelper
{
    public static string Url(CatalogItem item)
        => $"item/{item.Id}";
}
