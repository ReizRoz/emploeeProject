using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectEmploee.Core.DTOs;
using ProjectEmploee.Core.Entities;
using ProjectEmploee.Core.Enum;
using ProjectEmploee.Core.Services;
using System.Data;

namespace ProjectEmploee.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles="SystemAdministrator")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userServices;
        private readonly IMapper _mapper;
        public UserController(IUserService userServices, IMapper mapper)
        {
            _userServices = userServices;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult> GetAllAsync()
        {
            var userAll = await _userServices.GetAllAsync();
            return Ok(userAll);
        }


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


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            await _userServices.DeleteAsync(id);
            return Ok();
        }
    }
}
