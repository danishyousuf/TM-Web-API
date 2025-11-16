using TMCC.Repository.IRepository;
using TMCC.Services.IServices;

public class SecretService : ISecretService
{
    private readonly ISecretRepository _repo;

    public SecretService(ISecretRepository repo)
    {
        _repo = repo;
    }

    public async Task<string> GetClientId()
    {
        var encrypted = await _repo.GetSecretAsync("AzureClientId");
        return AesEncryptionHelper.Decrypt(encrypted);
    }

    public async Task<string> GetTenantId()
    {
        var encrypted = await _repo.GetSecretAsync("AzureTenantId");
        return AesEncryptionHelper.Decrypt(encrypted);
    }

    public async Task<string> GetClientSecret()
    {
        var encrypted = await _repo.GetSecretAsync("AzureClientSecret");
        return AesEncryptionHelper.Decrypt(encrypted);
    }
}
