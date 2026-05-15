namespace SYP.Customer;

public partial class UserStatusFormatterAttribute : CustomFormatterAttribute
{
    public const string Key = "SYP.Customer.UserStatusFormatter";

    public UserStatusFormatterAttribute()
        : base(Key)
    {
    }
}