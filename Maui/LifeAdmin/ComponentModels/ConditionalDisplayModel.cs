namespace LifeAdmin.ComponentModels;

public class ConditionalDisplayModel
{
    public List<DisplayCondition> DisplayConditions { get; set; } = [];
}
public class DisplayCondition
{
    public required string TargetKey { get; set; }
    public required string ComparisonValue { get; set; }
    public string? ComparisonOperator { get; set; } = "EQUALS";
}
