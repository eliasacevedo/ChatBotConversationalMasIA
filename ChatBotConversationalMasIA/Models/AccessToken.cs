namespace Models
{
    public class AccessToken
    {
        public string Accesstoken { get; set; }
        public string Scope { get; set; }
        public string Tokentype { get; set; }
        public int ExpiresIn { get; set; }
    }

    public class AccessTokenExtended : AccessToken {
        public Header Header { get; set; }
    }

}