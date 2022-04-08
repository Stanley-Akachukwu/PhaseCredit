
namespace PhaseCredit.Core.DTOs.Authentications
{
    public class UserLoginResponse : Response
    {
        public Guid Id { get; set; }
        public string AccessToken { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
