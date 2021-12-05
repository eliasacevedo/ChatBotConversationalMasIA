namespace Services.AuthenticationManager
{
    public interface IAuthenticationManager{
        string RefreshAuthorizationToken();
        string RefreshTokenId();
        string TokenId { get; set; }
        string AuthorizationToken { get; set; }

    }
}