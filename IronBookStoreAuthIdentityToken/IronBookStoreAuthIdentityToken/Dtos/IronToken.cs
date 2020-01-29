namespace IronBookStoreAuthIdentityToken.Dtos
{
    public class IronToken
    {
        public string Token { get; private set; }

        public IronToken(string token)
        {
            Token = token;
        }
    }
}
