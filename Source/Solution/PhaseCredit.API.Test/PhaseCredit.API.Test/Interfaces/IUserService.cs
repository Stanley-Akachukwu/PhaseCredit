using PhaseCredit.Core.DTOs.Users;

namespace PhaseCredit.API.Test.Interfaces
{
    public interface IUserService
    {
        Task<UsersResponse> GetListAsync(string accessToken);
    }
}
