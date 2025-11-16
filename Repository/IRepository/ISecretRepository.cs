namespace TMCC.Repository.IRepository
{
    public interface ISecretRepository
    {
        Task<string> GetSecretAsync(string secretName);
    }
}
