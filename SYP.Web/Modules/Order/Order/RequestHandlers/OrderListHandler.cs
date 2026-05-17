using Serenity.Data;
using Serenity.Services;
using System.Linq;
using MyRow = SYP.Order.OrderRow;

namespace SYP.Order;

public interface IOrderListHandler : IListHandler<MyRow, ListRequest, ListResponse<MyRow>> { }

public class OrderListHandler : ListRequestHandler<MyRow, ListRequest, ListResponse<MyRow>>, IOrderListHandler
{
    public OrderListHandler(IRequestContext context)
        : base(context)
    {
    }

    protected override void ApplyFilters(SqlQuery query)
    {
        base.ApplyFilters(query);

        var fld = OrderRow.Fields;
        var userId = int.Parse(Context.User.GetIdentifier());
        var userRole = GetUserRole(userId);

        switch (userRole)
        {
            case "Bayi":
                // Bayi sadece kendi siparişlerini görür
                var customer = Connection.TryFirst<Customer.CustomersRow>(q => q
                    .SelectTableFields()
                    .Where(new Criteria(Customer.CustomersRow.Fields.UserId) == userId));

                if (customer != null)
                {
                    query.Where(new Criteria(fld.CustomerId) == customer.Id.Value);
                }
                else
                {
                    query.Where(new Criteria("1=0")); // Hiçbir kayıt gösterme
                }
                break;

            case "Yönetici":
                // Yönetici sadece kendi bayilerinin siparişlerini görür
                var managedCustomerIds = Connection.Query<int>(
                    "SELECT Id FROM Customers WHERE ManagerUserId = @userId",
                    new { userId }).ToList();

                if (managedCustomerIds.Any())
                {
                    query.Where(new Criteria(fld.CustomerId).In(managedCustomerIds));
                }
                else
                {
                    query.Where(new Criteria("1=0"));
                }
                break;

            case "SüperAdmin":
                // Süper Admin herkesi görür, filtre yok
                break;
        }
    }

    private string GetUserRole(int userId)
    {
        // Süper Admin kontrolü (Admin rolüne sahip)
        if (Context.User.IsInRole("Admin"))
            return "SüperAdmin";

        // Yönetici kontrolü (CustomersRow'da ManagerUserId olarak kayıtlı)
        var isManager = Connection.Exists<Customer.CustomersRow>(
            new Criteria(Customer.CustomersRow.Fields.ManagerUserId) == userId);

        if (isManager)
            return "Yönetici";

        // Bayi kontrolü (CustomersRow'da UserId olarak kayıtlı)
        var isDealer = Connection.Exists<Customer.CustomersRow>(
            new Criteria(Customer.CustomersRow.Fields.UserId) == userId);

        if (isDealer)
            return "Bayi";

        return "Unknown";
    }
}
