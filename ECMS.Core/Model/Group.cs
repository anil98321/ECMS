namespace ECMS.Core.Model;

public record Group
{
    public int GroupId { get; set; }

    public string GroupName { get; set; } = null!;

    public DateTime? CreatedDate { get; set; }

    public bool IsActive { get; set; }
}
