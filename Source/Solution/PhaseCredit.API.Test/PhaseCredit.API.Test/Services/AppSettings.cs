using PhaseCredit.API.Test.Interfaces;

namespace PhaseCredit.API.Test.Services
{
    public class AppSettings: IAppSettings
    {
        public readonly IConfiguration _config;
        public AppSettings(IConfiguration config)
        {
            _config = config;
        }

        //------------PhaseCreditAPI Base Url----------------------
        public string PhaseCreditAPIUrl => _config["ServiceUrlSettings:PhaseCreditAPIUrl"];

        //------------PhaseCreditAPI Base Url----------------------


        //------------PhaseCreditAPI Endpoints----------------------
        public string PhaseCreditAPIGetUsers => _config["ServiceUrlSettings:Endpoints:PhaseCreditAPIGetUsers"];

        //------------PhaseCreditAPI Endpoints----------------------
    }
}


