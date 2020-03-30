using AutoMapper;
using Bll.DTO;
using Bll.Interfaces;
using Common.Helpers.PasswordHelper;
using DAL.Entities;
using DAL.Repository;
using DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Bll.Services
{
    public class UserManagementService : IUserManagmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileService;
        private readonly IConfirmationService _confirmation;
        private readonly IGenericRepository<UserDetails> _userDetailsRepository;
        private readonly IGenericRepository<User> _userRepository;
        private readonly IGenericRepository<Role> _roleRepository;
        private readonly IGenericRepository<UserConfirmation> _userConfirmationRepository;

        public UserManagementService(IUnitOfWork unitOfWork, IFileService fileService,
            IConfirmationService confirmation)
        {
            _unitOfWork = unitOfWork;
            _fileService = fileService;
            _confirmation = confirmation;
            _userDetailsRepository = _unitOfWork.GetRepository<UserDetails>();
            _userRepository = _unitOfWork.GetRepository<User>();
        }

        public async Task<User> GetUser(string email, string password)
        {
            var user = await _userRepository.GetAll().Include(x => x.Role)
                .FirstOrDefaultAsync(u => u.Email == email);
            if (PasswordHasher.VerifyHashedPassword(user.PasswordHash, password))
            {
                return user;
            }

            throw new Exception($"Cannot get user with {nameof(email)}: {email}.");
        }

        public async Task<User> GetUserById(Guid id)
        {
            var user = await _userRepository.Get(u => u.Id == id);
            return user;
        }

        public async Task AddUser(UserAllDto newUser)
        {
            try
            {
                if (newUser != null)
                {
                    var mapper = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<UserAllDto, UserDetails>();
                        cfg.CreateMap<UserAllDto, User>();
                    }).CreateMapper();

                    var userDetails = mapper.Map<UserAllDto, UserDetails>(newUser);

                    if (newUser.Image != null)
                    {
                        var imageUri = await _fileService.SaveFile(newUser.Image.OpenReadStream(),
                            Path.GetExtension(newUser.Image.FileName));
                        userDetails.ImageURl = imageUri.ToString();
                    }

                    var user = mapper.Map<UserAllDto, User>(newUser);
                    user.RoleId = (await _roleRepository.Get(r => r.Name == "Unconfirmed")).Id;
                    user.PasswordHash = PasswordHasher.HashPassword(newUser.Password);
                    user.Details = userDetails;
                    Random rand = new Random();
                    int random = rand.Next(10000000, 100000000);

                    var confUser = new UserConfirmation()
                    {
                        Number = random,
                        ConfDateTime = DateTime.Now
                    };

                    user.Confirmation = confUser;

                    _userDetailsRepository.Add(userDetails);
                    _userRepository.Add(user);
                    _userConfirmationRepository.Add(confUser);

                    await _unitOfWork.SaveChangesAsync();
                    await _confirmation.SendEmail(confUser.Number.ToString(), user.Email, "welcome.html");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{1}", ex);
            }
        }


        public async Task PasswordConfirmationRequest(string email)
        {
            var user = await _userRepository.Get(u => u.Email == email);
            var conf = await _userConfirmationRepository.Get(u => u.Id == user.Id);
            Random rand = new Random();
            int random = rand.Next(10000000, 100000000);
            conf.Number = random;
            conf.ConfDateTime = DateTime.Now;
            await _unitOfWork.SaveChangesAsync();
            await _confirmation.SendEmail(random.ToString(), email, "password.html");
        }
    }
}
