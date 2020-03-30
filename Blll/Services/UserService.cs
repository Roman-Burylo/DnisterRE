﻿using AutoMapper;
using Bll.DTO;
using Bll.Interfaces;
using Common;
using DAL.Entities;
using DAL.Repository;
using DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<User> _userRepository;
        private readonly IGenericRepository<UserDetails> _userDetailsRepository;
        private readonly IGenericRepository<Role> _roleRepository;
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userRepository = _unitOfWork.GetRepository<User>();
            _userDetailsRepository = _unitOfWork.GetRepository<UserDetails>();
            _roleRepository = _unitOfWork.GetRepository<Role>();
        }

        public async Task<UserAllDto> GetMyProfile(Guid id)
        {
            try
            {
                var user = await _userRepository.Get(e => e.Id == id);

                var userDetails = await _userDetailsRepository.GetAll()
                    .Include(ud => ud.User)
                    .SingleOrDefaultAsync(ud => ud.UserDetailsId == id);
                var detailsMapper = new MapperConfiguration(cfg =>
                    cfg.CreateMap<UserDetails, UserAllDto>()
                        .ForMember(userAllDto => userAllDto.Email,
                            opts => opts.MapFrom(details => details.User.Email))
                        .ForMember(userAllDto => userAllDto.Id, opt => opt.MapFrom(details => details.UserDetailsId)))
                    .CreateMapper();
                var userAll = detailsMapper.Map<UserDetails, UserAllDto>(userDetails);
                return userAll;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error when getting user by {nameof(id)}={id}: ", ex);
            }
        }

        public async Task<UserDetailsDto> GetUserDetailsById(Guid id)
        {
            try
            {
                var user = await _userDetailsRepository.Get(u => u.UserDetailsId == id);

                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<UserDetails, UserDetailsDto>()).CreateMapper();
                var userDto = mapper.Map<UserDetails, UserDetailsDto>(user);

                return userDto;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error when getting user by {nameof(id)}={id}: ", ex);
            }
        }

        public async Task<UsersListDto> GetAllUsers()
        {
            var users = await _userDetailsRepository.GetAll().ToListAsync();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<UserDetails, UsersDto>()).CreateMapper();
            var usersDto = mapper.Map<IEnumerable<UserDetails>, IEnumerable<UsersDto>>(users);
            var usersList = new UsersListDto()
            {
                UserList = usersDto.ToList(),
                CountOfItems = await _userRepository.GetAll().CountAsync()
            };

            return usersList;
        }


        public async Task Update(UserDetailsDto user, Guid id)
        {
            try
            {
                var updateUser = await _userDetailsRepository.Get(u => u.UserDetailsId == id);

                updateUser.FirstName = user.FirstName;
                updateUser.LastName = user.LastName;
                updateUser.PhoneNumber = user.PhoneNumber;
                updateUser.BirthDay = user.BirthDay;
                updateUser.City = user.City;
                updateUser.ShortInformation = user.ShortInformation;

                //if (user.Image != null)
                //{
                //    var imageUri = await _fileService.SaveFile(user.Image.OpenReadStream(),
                //        Path.GetExtension(user.Image.FileName));
                //    updateUser.ImageURl = imageUri.ToString();
                //}

                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Cannot update the {nameof(UserDetails)}.", ex);
            }
        }

        public async Task<bool> ChangeRole(Guid userId, Guid newRole)
        {
            var user = await _userRepository.Get(u => u.Id == userId);
            user.RoleId = newRole;
            _userRepository.Update(user);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<AdminUserViewDto> AdminUserView(Guid userId)
        {
            var userDetails = await _userDetailsRepository.Get(u => u.UserDetailsId == userId);
            var user = await _userRepository.Get(u => u.Id == userId);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<UserDetails, AdminUserViewDto>()).CreateMapper();
            var userDto = mapper.Map<UserDetails, AdminUserViewDto>(userDetails);
            userDto.RoleName = (await _roleRepository.Get(r => r.Id == user.RoleId)).Name;
            return userDto;
        }

        public async Task<IEnumerable<RolesListDto>> GetAllRols()
        {
            var Roles = await _roleRepository.GetAllAsync();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Role, RolesListDto>()).CreateMapper();
            var rolessDto = mapper.Map<IEnumerable<Role>, IEnumerable<RolesListDto>>(Roles);
            return rolessDto;
        }

        public async Task<Guid> GetUserRole(Guid userId)
        {
            var user = await _userRepository.Get(u => u.Id == userId);
            return user.RoleId;
        }

    }
}