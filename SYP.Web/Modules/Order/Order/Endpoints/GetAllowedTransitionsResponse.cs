using System.Collections.Generic;

namespace SYP.Order.Endpoints;

public class GetAllowedTransitionsResponse
{
    public List<AllowedTransitionItem> Transitions { get; set; } = [];
}

public class AllowedTransitionItem
{
    public int Status { get; set; }
    public string Label { get; set; }
    public bool RequiresReason { get; set; }
}
