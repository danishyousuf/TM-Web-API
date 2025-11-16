using Dapper;
using TMCC.Db_Helper;
using TMCC.Repository.IRepository;

namespace TMCC.Repository
{
    public class SecretRepository : ISecretRepository
    {
        private readonly DapperHelper _dapper;

        public SecretRepository(DapperHelper dapper)
        {
            _dapper = dapper;
        }

        public async Task<string> GetSecretAsync(string secretName)
        {
            return await _dapper.QueryFirstOrDefaultAsync<string>(
                "SELECT SecretValue FROM AppSecrets WHERE SecretName = @name",
                new { name = secretName }
            );
        }
    }
}
