using Navigator.Actions.Builder;

namespace Navigator.Catalog;

public interface IBotActionCatalogFactory
{
    public IBotActionBuilder OnUpdate(Delegate condition, Delegate handler);

    public BotActionCatalog Retrieve();
}