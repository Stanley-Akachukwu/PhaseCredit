namespace PhaseCredit.API.Common
{
    public interface IAppSettings
    {
        string IdentityServerUrl { get; }
        string ClientId { get; }
        string ClientSecret { get; }
        string Scope { get; }
 
    }
}


