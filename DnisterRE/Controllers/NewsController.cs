using DAL.Entities;
using DAL.Repository;
using DAL.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DnisterRE.Controllers
{
    [ApiController]
    [Route("api/news")]
    public class NewsController : ControllerBase
    {
        private readonly IGenericRepository<News> _newsRepository;

        private readonly IUnitOfWork _unitOfWork;

        public NewsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _newsRepository = unitOfWork.GetRepository<News>();
        }

        [HttpGet]
        public IQueryable GetAll()
        {
            return _newsRepository.GetAll();
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
                var news = await _newsRepository.Get(x => x.Id == id);
                return Ok(news);
            }
            catch (Exception ex)
            {
                ex.Data["id"] = id;
                throw;
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAsync([FromBody]News news)
        {
            if (news == null)
            {
                throw new ArgumentException($"{nameof(news)} can't be null");
            }

            try
            {
                _newsRepository.Add(news);
                await _unitOfWork.SaveChangesAsync();
                return Ok(news);
            }
            catch
            {
                throw new Exception($"Error while adding news nameof {nameof(news)}");
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateById(int id, [FromBody]News news)
        {
            if (id == default)
            {
                throw new ArgumentException($"{nameof(id)} cannot be 0");
            }

            try
            {
                var currentNews = await _newsRepository.Get(x => x.Id == id);

                if (currentNews == null)
                {
                    throw new NullReferenceException($"Error while updating news. News with {nameof(id)}={id} not found");
                }

                currentNews.Name = news.Name;
                currentNews.Description = news.Name;
                

                _newsRepository.Update(currentNews);
                await _unitOfWork.SaveChangesAsync();

                return Ok(currentNews);
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
                var user = await _newsRepository.Get(x => x.Id == id);
                _newsRepository.Remove(user);
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