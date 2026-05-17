using Serenity.Data;
using Serenity.Web;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SYP.Catalog;

[LookupScript("Catalog.ProductCategory", Permission = "Catalog:ProductCategory:Read")]
public class ProductCategoryLookup : LookupScript
{
    private readonly ISqlConnections _sqlConnections;

    public ProductCategoryLookup(ISqlConnections sqlConnections)
    {
        _sqlConnections = sqlConnections;
        IdField = "Id";
        TextField = "FullPath";
        LookupKey = "Catalog.ProductCategory";
    }

    public override string GetScript()
    {
        return "Q.ScriptData.set(" + ("Lookup." + LookupKey).ToSingleQuoted() + ", " + GetLookupData() + ");";
    }

    private string GetLookupData()
    {
        var items = GetItems();
        var data = new Dictionary<string, object>
        {
            ["Items"] = items,
            ["IdField"] = IdField,
            ["TextField"] = TextField
        };
        return JSON.Stringify(data);
    }

    protected override IEnumerable GetItems()
    {
        var fld = ProductCategoryRow.Fields;
        List<ProductCategoryRow> rows;

        using (var connection = _sqlConnections.NewFor<ProductCategoryRow>())
        {
            rows = connection.List<ProductCategoryRow>(q => q
                .Select(fld.Id, fld.Name, fld.ParentId, fld.IsActive)
                .OrderBy(fld.Name));
        }

        var dict = rows.ToDictionary(x => x.Id.Value);
        var result = new List<Dictionary<string, object>>();

        foreach (var row in rows)
        {
            var fullPath = GetFullPath(row, dict);
            result.Add(new Dictionary<string, object>
            {
                ["Id"] = row.Id,
                ["Name"] = row.Name,
                ["ParentId"] = row.ParentId,
                ["FullPath"] = fullPath,
                ["IsActive"] = row.IsActive
            });
        }

        return result.OrderBy(x => x["FullPath"]?.ToString()).ToList();
    }

    private string GetFullPath(ProductCategoryRow item, Dictionary<int, ProductCategoryRow> dict)
    {
        var parts = new List<string> { item.Name };
        var parentId = item.ParentId;

        while (parentId.HasValue && dict.TryGetValue(parentId.Value, out var parent))
        {
            parts.Insert(0, parent.Name);
            parentId = parent.ParentId;
        }

        return string.Join(" > ", parts);
    }
}
