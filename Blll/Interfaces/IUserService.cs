using Bll.DTO;
using Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bll.Interfaces
{
    public interface IUserService
    {
        Task<UserAllDto> GetMyProfile(Guid id);

        Task<UserDetailsDto> GetUserDetailsById(Guid id);

        Task Update(UserDetailsDto user, Guid id);

        Task<UsersListDto> GetAllUsers();

        Task<bool> ChangeRole(Guid userId, Guid newRole);

        Task<AdminUserViewDto> AdminUserView(Guid userId);

        Task<IEnumerable<RolesListDto>> GetAllRols();

        Task<Guid> GetUserRole(Guid userId);
    }
}