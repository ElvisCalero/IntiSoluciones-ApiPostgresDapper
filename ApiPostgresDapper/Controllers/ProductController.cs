using ApiPostgresDapper.Domain.Entities;
using ApirPostgresDapper.Infraestructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ApiPostgresDapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository) => _productRepository = productRepository;
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _productRepository.GetAll());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _productRepository.GetById(id));
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Product entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _productRepository.Add(entity);
            return Created("Created", result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Product entity)
        {
            if (id != entity.Id)
                return BadRequest();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _productRepository.Update(entity);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _productRepository.Delete(id);
            return NoContent();
        }
    }
}
