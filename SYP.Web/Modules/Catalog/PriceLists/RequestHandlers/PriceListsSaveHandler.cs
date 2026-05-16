using Serenity.Data;
using Serenity.Services;
using System.Collections.Generic;
using MyRow = SYP.Catalog.PriceListsRow;

namespace SYP.Catalog;

public interface IPriceListsSaveHandler : ISaveHandler<MyRow> { }

public class PriceListsSaveHandler : SaveRequestHandler<MyRow>, IPriceListsSaveHandler
{
    public PriceListsSaveHandler(IRequestContext context) : base(context) { }

    private List<PriceListItemsRow> ItemList;

    protected override void GetEditableFields(HashSet<Field> editable)
    {
        base.GetEditableFields(editable);
        editable.Remove(MyRow.Fields.InsertDate);
        editable.Remove(MyRow.Fields.InsertUserId);
        editable.Remove(MyRow.Fields.UpdateDate);
        editable.Remove(MyRow.Fields.UpdateUserId);
    }

    protected override void BeforeSave()
    {
        base.BeforeSave();

        Row.UpdateDate = System.DateTime.Now;
        Row.UpdateUserId = Context.User.GetIdentifier()?.TryParseID32();

        if (IsCreate)
        {
            Row.InsertDate = System.DateTime.Now;
            Row.InsertUserId = Context.User.GetIdentifier()?.TryParseID32();
        }

        // Varsayılan olarak işaretlendiyse diğerlerini kaldır
        if (Row.IsDefault == true)
        {
            new SqlUpdate(MyRow.Fields.TableName)
                .Set(MyRow.Fields.IsDefault, false)
                .Where(MyRow.Fields.Id != (Row.Id ?? 0))
                .Execute(Connection);
        }
    }

    protected override void SetInternalFields()
    {
        base.SetInternalFields();

        // ItemList'i Request'ten al
        if (Request.Entity != null)
        {
            ItemList = Request.Entity.ItemList;
        }
    }

    protected override void AfterSave()
    {
        base.AfterSave();

        if (ItemList == null)
            return;

        var itemFld = PriceListItemsRow.Fields;
        var oldItems = new Dictionary<int, PriceListItemsRow>();

        if (!IsCreate)
        {
            foreach (var item in Connection.List<PriceListItemsRow>(
                itemFld.PriceListId == Row.Id.Value))
            {
                oldItems[item.Id.Value] = item;
            }
        }

        foreach (var item in ItemList)
        {
            item.PriceListId = Row.Id.Value;

            if (item.Id == null)
            {
                // Yeni kayıt
                Connection.Insert(item);
            }
            else if (oldItems.TryGetValue(item.Id.Value, out var old))
            {
                // Güncelle
                Connection.UpdateById(item);
                oldItems.Remove(item.Id.Value);
            }
        }

        // Silinen kayıtlar
        foreach (var item in oldItems.Values)
        {
            Connection.DeleteById<PriceListItemsRow>(item.Id.Value);
        }
    }
}
