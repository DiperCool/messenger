namespace Web.Models.Models
{
    public class ReturnTokensModel
    {
        public ReturnTokensModel(string _token, string _refreshtoken)
        {
            Token=_token;
            RefreshToken=_refreshtoken;
        }

        public ReturnTokensModel(){}

        public string Token{get;set;}
        public string RefreshToken{get;set;}
    }
}