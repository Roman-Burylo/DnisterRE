using Bll.DTO;
using System.Threading.Tasks;

namespace Bll.Interfaces
{
    public interface IConfirmationService
    {
        Task SendEmail(string ConfirmNUm, string Email, string PathTo);

        Task ConfirmUser(ConfirmUserDto confirmUser);

        Task ConfirmPassword(ConfirmPasswordChangingDto confirmUser);
    }
}