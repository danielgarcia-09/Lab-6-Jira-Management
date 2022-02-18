using JiraManagement.Bl.Dto;
using JiraManagement.Model.Models;
using JiraManagement.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace JiraManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class BaseController<T, TDto, TContext> : ControllerBase
       where T : BaseEntity
       where TDto : BaseDto
       where TContext : DbContext
    {
        private readonly IBaseService<T, TDto, TContext> _service;
        public BaseController(IBaseService<T, TDto, TContext> service)
        {
            _service = service;
        }

        [HttpGet]
        public virtual async Task<IActionResult> Get()
        {
            return Ok(_service.Get());
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetById(string id)
        {
            var dto = await _service.GetById(id);

            if (dto is null)
            {
                return NotFound();
            }
            return Ok(dto);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Create(TDto dto)
        {
            var newDto = await _service.Create(dto);

            if (newDto is null)
            {
                return BadRequest();
            }
            return Ok(newDto);
        }

        [HttpPut]
        public virtual async Task<IActionResult> Update(string id, TDto dto)
        {
            var updatedDto = await _service.Update(id, dto);

            if (updatedDto is null)
            {
                return BadRequest();
            }
            return Ok(updatedDto);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var isDeleted = await _service.Delete(id);

            if (isDeleted)
            {
                return NoContent();
            }
            return NotFound();
        }
    }
}
