using Bll.DTO;
using Bll.Interfaces;
using System;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using DAL.UnitOfWork;
using SendGrid;
using Common;

namespace Bll.Services
{
    public class ConfirmationService : IConfirmationService
    {
        private readonly IHostingEnvironment _env;
        private readonly IUnitOfWork _unitOfWork;
        private readonly EmailSettings _emailSettings;
        private readonly SendGridClient _emailClient;
        IUse

        public ConfirmationService(IHostingEnvironment env, IUnitOfWork unitOfWork, IOptions<EmailSettings> emailSettings, IConfiguration configuration)
        {
            _env = env;
            _unitOfWork = unitOfWork;
            _emailSettings = emailSettings.Value;
            var apiKey = configuration.GetSection("send_grip_api_key").Value; ;
            _emailClient = new SendGridClient(apiKey);
        }


        public async Task SendEmail(string ConfirmNum, string UserEmail, string PathTo)
        {
            string body = string.Empty;
            using (StreamReader reader =
            new StreamReader(Path.Combine(_env.ContentRootPath, "Templates", PathTo)))
            {
                body = await reader.ReadToEndAsync();
            }

            var user = await _unitOfWork.Repository<UserDetails>().Get(u => u.User.Email == UserEmail);
            var url = "https://dnisterRE.azurewebsites.net";
            if (_env.IsDevelopment())
            {
                url = "http://localhost:5000";
            }

            body = body.Replace("{EMAIL}", UserEmail);
            body = body.Replace("{NUMBER}", ConfirmNum);
            body = body.Replace("{FIRSTNAME}", user.FirstName);
            body = body.Replace("{URL}", url);

            var from = new EmailAddress(_emailSettings.Email, "UTTMM");
            var to = new EmailAddress(UserEmail);

            var subject = "UTTMM";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, "", body);
            var response = await _emailClient.SendEmailAsync(msg);
        }

        public async Task ConfirmUser(ConfirmUserDto confirmUser)
        {
            var user = await _unitOfWork.Repository<User>().Get(u => u.Email == confirmUser.Email);
            var conf = await _unitOfWork.Repository<UserConfirmation>().Get(u => u.Id == user.Id);
            if (confirmUser.ConfirmationNumber == conf.Number.ToString() && conf.ConfDateTime <= DateTime.Now.AddMinutes(30)) user.RoleId = (await _unitOfWork.Repository<Role>().Get(r => r.Name == "Guest")).Id;
            _unitOfWork.Repository<User>().Update(user);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task ConfirmPassword(ConfirmPasswordChangingDto confirmUser)
        {
            var user = await _unitOfWork.Repository<User>().Get(u => u.Email == confirmUser.Email);
            var conf = await _unitOfWork.Repository<UserConfirmation>().Get(u => u.Id == user.Id);
            if (confirmUser.ConfirmationNumber == conf.Number.ToString() && conf.ConfDateTime <= DateTime.Now.AddMinutes(30))
            {
                user.PasswordHash = PasswordHasher.HashPassword(confirmUser.Password);
                await _unitOfWork.SaveChangesAsync();
            }
        }
    }
}
