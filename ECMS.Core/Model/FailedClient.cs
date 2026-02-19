namespace ECMS.Core.Model;
public struct FailedClient
{
    public int ClientId { get; set; }
    public string ClientName { get; set; }
    public string Email { get; set; }
    public string ErrorMessage { get; set; }
}
