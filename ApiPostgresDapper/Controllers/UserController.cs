using ApiPostgresDapper.Domain.Entities;
using ApirPostgresDapper.Infraestructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ApiPostgresDapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository) => _userRepository = userRepository;
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _userRepository.GetAll());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _userRepository.GetById(id));
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] User entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _userRepository.Add(entity);
            return Created("Created", result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] User entity)
        {
            if (id != entity.Id)
                return BadRequest();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _userRepository.Update(entity);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _userRepository.Delete(id);
            return NoContent();
        }
    }
}
