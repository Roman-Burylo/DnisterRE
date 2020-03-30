using Bll.DTO;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bll.Interfaces
{
    public interface IAuthenticationService
    {
        Task<string> Authenticate(User user);
        Task<AuthUser> GetAuthUser();
        Task<AuthUser> TryGetAuthUser();
    }
}