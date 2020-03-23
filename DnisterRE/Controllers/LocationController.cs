using DAL.Entities;
using DAL.Repository;
using DAL.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DnisterRE.Controllers
{
    [ApiController]
    [Route("api/locations")]
    public class LocationController : ControllerBase
    {
        private readonly IGenericRepository<Location> _locationRepository;

        private readonly IUnitOfWork _unitOfWork;

        public LocationController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _locationRepository = unitOfWork.GetRepository<Location>();
        }

        [HttpGet]
        public IQueryable GetAll()
        {
            return _locationRepository.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id == default)
            {
                throw new ArgumentException($"{nameof(id)} can't be 0");
            }

            try
            {
                var location = await _locationRepository.Get(x => x.Id == id);
                return Ok(location);
            }
            catch (Exception ex)
            {
                ex.Data["id"] = id;
                throw;
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAsync([FromBody]Location location)
        {
            if (location == null)
            {
                throw new ArgumentException($"{nameof(location)} can't be null");
            }

            try
            {
                _locationRepository.Add(location);
                await _unitOfWork.SaveChangesAsync();
                return Ok(location);
            }
            catch
            {
                throw new Exception($"Error while adding user nameof {nameof(location)}");
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateById(int id, [FromBody]Location location)
        {
            if (id == default)
            {
                throw new ArgumentException($"{nameof(id)} cannot be 0");
            }

            try
            {
                var currentLocation = await _locationRepository.Get(x => x.Id == id);

                if (currentLocation == null)
                {
                    throw new NullReferenceException($"Error while updating user. User with {nameof(id)}={id} not found");
                }

                currentLocation.Name = location.Name;
                currentLocation.Description = location.Description;
                _locationRepository.Update(currentLocation);
                await _unitOfWork.SaveChangesAsync();

                return Ok(currentLocation);
            }
            catch (Exception ex)
            {
                ex.Data["id"] = id;
                throw;
            }
        }

        [HttpDelete("remove/{id}")]
        public async Task<IActionResult> RemoveById(int id)
        {
            if (id == default)
            {
                throw new ArgumentException($"{nameof(id)} cannot be 0");
            }

            try
            {
                var location = await _locationRepository.Get(x => x.Id == id);
                _locationRepository.Remove(location);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                ex.Data["id"] = id;
                throw;
            }

            return Ok();
        }
    }
}