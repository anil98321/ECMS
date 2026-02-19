namespace ECMS.Core.EFStructures.Entities;

public partial class ClientGroup
{
    public int ClientGroupId { get; set; }

    public int ClientId { get; set; }

    public int GroupId { get; set; }

    public virtual Client Client { get; set; } = null!;

    public virtual Group Group { get; set; } = null!;
}
