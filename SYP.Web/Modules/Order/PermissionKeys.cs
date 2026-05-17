using Serenity.ComponentModel;

namespace SYP.Order;

[NestedPermissionKeys]
[DisplayName("Sipariş Yönetimi")]
public class PermissionKeys
{
    [DisplayName("Siparişler")]
    public class Orders
    {
        public const string Navigation = "Order:Orders:Navigation";
        public const string Read = "Order:Orders:Read";
        public const string Insert = "Order:Orders:Insert";
        public const string Update = "Order:Orders:Update";
        public const string Delete = "Order:Orders:Delete";
    }

    [DisplayName("Kademeli İndirim Ayarları")]
    public class TieredDiscountSettings
    {
        public const string Navigation = "Order:TieredDiscountSettings:Navigation";
        public const string Read = "Order:TieredDiscountSettings:Read";
        public const string Insert = "Order:TieredDiscountSettings:Insert";
        public const string Update = "Order:TieredDiscountSettings:Update";
        public const string Delete = "Order:TieredDiscountSettings:Delete";
    }
}
