using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using EasyFast.Application.Users.Dto;

namespace EasyFast.Application.Users
{
    public interface IUserAppService : IApplicationService
    {
        Task ProhibitPermission(ProhibitPermissionInput input);

        Task RemoveFromRole(long userId, string roleName);

        Task<ListResultDto<UserListDto>> GetUsers();

        Task CreateUser(CreateUserInput input);
    }
}