namespace ECMS.API.DTO;
public struct FailedClientDto
{
    public int ClientId { get; set; }
    public string ClientName { get; set; }
    public string Email { get; set; }
    public string ErrorMessage { get; set; }
}
