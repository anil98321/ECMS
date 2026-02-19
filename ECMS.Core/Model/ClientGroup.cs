namespace ECMS.Core.Model;

public record ClientGroup
{
    public int ClientGroupId { get; set; }

    public int ClientId { get; set; }

    public int GroupId { get; set; }
}
