namespace PhaseCredit.API.Common
{
    public class AppSettings : IAppSettings
    {
        public readonly IConfiguration _config;
        public AppSettings(IConfiguration config)
        {
            _config = config;
        }
        //------------Duende Identity Server----------------------
        public string IdentityServerUrl => _config["IdentityServer:Url"];
        public string ClientId => _config["IdentityServer:phaseCreditAPI:ClientId"];
        public string ClientSecret => _config["IdentityServer:phaseCreditAPI:ClientSecret"];
        public string Scope => _config["IdentityServer:phaseCreditAPI:Scope"];
        //------------Duende Identity Server----------------------
    }
}
 