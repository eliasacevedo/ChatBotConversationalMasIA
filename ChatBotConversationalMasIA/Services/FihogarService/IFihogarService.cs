using System.Threading.Tasks;
using Models;

namespace Services.FihogarService
{
    public interface IFihogarService{
        Task<AccessToken> AccessToken(string grantType, string token);
        Task<AccessTokenExtended> AutorizeProvider(string provider, string username, string password, string grantType, string token);
        Task<AccountDetails> GetAccount(string provider, string tokenId, string authorization);
    }

}