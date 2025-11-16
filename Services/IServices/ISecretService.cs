namespace TMCC.Services.IServices
{
    public interface ISecretService
    {
        Task<string> GetClientId();
        Task<string> GetTenantId();
        Task<string> GetClientSecret();
    }
}
