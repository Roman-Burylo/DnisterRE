using DAL.Entities;
using DAL.Repository;
using DAL.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DnisterRE.Controllers
{
    [Route("api/userRoles")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IGenericRepository<Role> _userRoleRepository;

        public UserRoleController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userRoleRepository = unitOfWork.GetRepository<Role>();
        }

        [HttpGet]
        public IQueryable GetAll()
        {
            return _userRoleRepository.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            if (id == default)
            {
                throw new ArgumentException($"{nameof(id)} can`t be 0");
            }

            try
            {
                var dishtype = await _userRoleRepository.Get(e => e.Id == id);
                return Ok(dishtype);
            }
            catch (Exception ex)
            {
                ex.Data["id"] = id;
                throw;
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody]Role userRole)
        {        
            try
            {
                _userRoleRepository.Add(userRole);
                await _unitOfWork.SaveChangesAsync();
                return Ok(userRole);
            }
            catch (Exception ex)
            {
                ex.Data["userRole"] = userRole;
                throw;
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateById(Guid id, [FromBody]Role userRole)
        {
            if (id == default)
            {
                throw new ArgumentException($"{nameof(id)} can`t be 0");
            }

            try
            {
                var currentUserRole = await _userRoleRepository.Get(e => e.Id == id);

                if (currentUserRole == null)
                {
                    throw new NullReferenceException($"Error while updating userRole. UserRole with {nameof(id)}={id} not found");
                }

                currentUserRole.Name = userRole.Name;
                _userRoleRepository.Update(currentUserRole);
                await _unitOfWork.SaveChangesAsync();
                return Ok(currentUserRole);
            }
            catch (Exception ex)
            {
                ex.Data["userRole"] = userRole;
                throw;
            }
        }

        [HttpDelete("remove/{id}")]
        public async Task<IActionResult> RemoveById(Guid id)
        {
            if (id == default)
            {
                throw new ArgumentException($"{nameof(id)} can`t be 0");
            }

            try
            {
                Role userRole = await _userRoleRepository.Get(e => e.Id == id);
                _userRoleRepository.Remove(userRole);
                await _unitOfWork.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                ex.Data["id"] = id;
                throw;
            }
        }
    }
}