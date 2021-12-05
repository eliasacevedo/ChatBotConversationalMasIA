using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Models;
using Services.AuthenticationManager;
using Services.DoRequest;

namespace Services.FihogarService{
    public class FihogarService : IFihogarService
    {
        private readonly IDoRequest doRequest;
        private readonly IConfiguration configuration;
        private readonly IAuthenticationManager authenticationManager;
        private const string FIHOGAR_KEY_FILE_TOKEN = "FihogarToken";
        private const string FIHOGAR_KEY_FILE_PROVIDER = "FihogarProvider";
        private const string FIHOGAR_KEY_FILE_USERNAME = "FihogarUsername";
        private const string FIHOGAR_KEY_FILE_PASSWORD = "FihogarPassword";
        private const string FIHOGAR_ACCESS_KEY_PATH = "https://api.uat.4wrd.tech:8243/token";
        private const string FIHOGAR_AUTORIZE_PROVIDER_PATH = "https://api.uat.4wrd.tech:8243/authorize/2.0/token";
        private const string FIHOGAR_GET_ACCOUNT_PATH= "https://api.uat.4wrd.tech:8243/manage-accounts/api/2.0/accounts/";
        
        private string tokenId;
        public string TokenId
        {
            get { return tokenId; }
            set { tokenId = value; }
        }
        
        private string authorizationToken;
        public string AuthorizationToken
        {
            get { 
                return authorizationToken; 
            }
            set { authorizationToken = value; }
        }

        public FihogarService(IDoRequest request, IConfiguration configuration, IAuthenticationManager authenticationManager)
        {
            doRequest = request;
            this.configuration = configuration;
            this.authenticationManager = authenticationManager;
        }

        public async Task<AccessToken> AccessToken(string grantType, string token)
        {
            const string path = FIHOGAR_ACCESS_KEY_PATH;
            var headers = new Dictionary<string, string>();
            headers.Add("Authorization", configuration[FIHOGAR_KEY_FILE_TOKEN]);

            var form = new Dictionary<string, string>();
            form.Add("grant_type", "client_credentials");

            return await doRequest.Post<AccessToken>(path, headers, form);
        }

        public async Task<AccessTokenExtended> AutorizeProvider(string provider, string username, string password, string grantType, string token)
        {
            string path = $"{FIHOGAR_AUTORIZE_PROVIDER_PATH}?provider={configuration[FIHOGAR_KEY_FILE_PROVIDER]}";
            var headers = new Dictionary<string, string>();
            headers.Add("Authorization", authenticationManager.TokenId);

            var form = new Dictionary<string, string>();
            form.Add("grant_type", "password");
            form.Add("username", configuration[FIHOGAR_KEY_FILE_USERNAME]);
            form.Add("password", configuration[FIHOGAR_KEY_FILE_PASSWORD]);

            return await doRequest.Post<AccessTokenExtended>(path, headers, form);
        }

        public async Task<AccountDetails> GetAccount(string provider, string tokenId, string authorization)
        {
            string path = $"{FIHOGAR_GET_ACCOUNT_PATH}?provider={configuration[FIHOGAR_KEY_FILE_PROVIDER]}";
            var headers = new Dictionary<string, string>();
            headers.Add("token-id", authenticationManager.TokenId);
            headers.Add("Authorization", $"Bearer {authenticationManager.AuthorizationToken}");

            return await doRequest.Get<AccountDetails>(path, headers);
        }
    }
}