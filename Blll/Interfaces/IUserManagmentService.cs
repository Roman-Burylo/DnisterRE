using Bll.DTO;
using DAL.Entities;
using System;
using System.Threading.Tasks;

namespace Bll.Interfaces
{
    public interface IUserManagmentService
    {
        Task<User> GetUser(string email, string password);

        Task<User> GetUserById(Guid id);

        Task AddUser(UserAllDto NewUser);

        Task PasswordConfirmationRequest(string Email);
    }
}