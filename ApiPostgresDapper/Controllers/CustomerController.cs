using ApiPostgresDapper.Domain.Entities;
using ApirPostgresDapper.Infraestructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPostgresDapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerController(ICustomerRepository customerRepository) => _customerRepository = customerRepository;
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _customerRepository.GetAll());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _customerRepository.GetById(id));
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Customer entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _customerRepository.Add(entity);
            return Created("Created", result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Customer entity)
        {
            if (id != entity.Id)
                return BadRequest();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _customerRepository.Update(entity);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _customerRepository.Delete(id);
            return NoContent();
        }
    }
}
