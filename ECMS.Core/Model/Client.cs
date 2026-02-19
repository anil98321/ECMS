namespace ECMS.Core.Model;

public record Client
{
    public int ClientId { get; set; }

    public string ClientName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateTime? CreatedDate { get; set; }

    public bool IsActive { get; set; }

}
