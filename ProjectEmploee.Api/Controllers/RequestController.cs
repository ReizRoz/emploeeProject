using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectEmploee.Core.DTOs;
using ProjectEmploee.Core.Entities;
using ProjectEmploee.Core.Enum;
using ProjectEmploee.Core.Services;
using ProjectEmploee.Service;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectEmploee.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "SystemAdministrator")]


    public class RequestController : ControllerBase
    {
        private readonly IRequestService _requstService;
        private readonly IMapper _mapper;
        public RequestController(IRequestService requestService, IMapper mapper)
        {
            _requstService = requestService;
            _mapper = mapper;
        }
        // GET: api/<RequestController>
        [HttpGet]
        public async Task<ActionResult> GetAllAsync()
        {
            var requestAll = await _requstService.GetAllAsync();
            return Ok(requestAll);
        }



        // GET api/<RequestController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetByIdAsync(int id)
        {
            var requestById = await _requstService.GetByIdAsync(id);
            return Ok(requestById);
        }



        // POST api/<RequestController>
        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] RequestDTO r)
        {
            var request = _mapper.Map<Request>(r);
            var newRequest = await _requstService.PostAsync(request);
            return Ok(newRequest);
        }


        // PUT api/<RequestController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutAsync(int id, [FromBody] RequestDTO r)
        {
            var request = _mapper.Map<Request>(r);
            var requestPut = await _requstService.PutAsync(id, request);
            return Ok(requestPut);
        }

        // DELETE api/<RequestController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            await _requstService.DeleteAsync(id);
            return Ok();
        }
    }
}


