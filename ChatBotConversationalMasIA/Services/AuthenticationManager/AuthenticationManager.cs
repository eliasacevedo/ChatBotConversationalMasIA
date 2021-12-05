using Microsoft.Extensions.Configuration;
using Services.FihogarService;

namespace Services.AuthenticationManager
{
    public class AuthenticationManager: IAuthenticationManager{
        private IConfiguration configuration;
        public AuthenticationManager(IConfiguration configuration)
        {
            
            this.configuration = configuration;

        }
        

        public string RefreshAuthorizationToken()
        {
            throw new System.NotImplementedException();
        }

        public string RefreshTokenId()
        {
            throw new System.NotImplementedException();
        }

    }
}
