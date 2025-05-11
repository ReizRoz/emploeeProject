using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectEmploee.Core.DTOs;
using ProjectEmploee.Core.Entities;
using ProjectEmploee.Core.Enum;
using ProjectEmploee.Core.Services;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectEmploee.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize(Roles="SystemAdministrator")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userServices;
        private readonly IMapper _mapper;
        public UserController(IUserService userServices, IMapper mapper)
        {
            _userServices = userServices;
            _mapper = mapper;
        }
        // GET: api/<UserController>
        [HttpGet]
        public async Task<ActionResult> GetAllAsync()
        {
            var userAll = await _userServices.GetAllAsync();
            return Ok(userAll);
        }

        // PUT api/<UserController>/5
        [HttpGet("IdUser/{id}")]
        public async Task<ActionResult> GetByIdAsync(int id)
        {
            var userById = await _userServices.GetByIdAsync(id);
            return Ok(userById);
        }
        [HttpGet("Name/{name}")]
        public async Task<ActionResult> GetByNameAsync(string name)
        {
            var userByName = await _userServices.GetByNameAsync(name);
            return Ok(userByName);
        }
        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] UserPostDTO u)
        {

            var user=_mapper.Map<User>(u);
            var newUser = await _userServices.PostAsync(user);
            return Ok(newUser);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutAsync(int id, [FromBody] UserPostDTO u)
        {
            var user = _mapper.Map<User>(u);
            var userPut = await _userServices.PutAsync(id, user);
            return Ok(userPut);
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            await _userServices.DeleteAsync(id);
            return Ok();
        }
    }
}
