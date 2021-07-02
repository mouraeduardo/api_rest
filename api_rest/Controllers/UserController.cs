using api_rest.Domain.Models;
using api_rest.Domain.Services;
using api_rest.Extensions;
using api_rest.Resources;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_rest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        //[Authorize()]
        [HttpGet]
        public async Task<IEnumerable<UserResources>> GetAllAsync()
        {
            var users = await _userService.ListAsync();
            var resources = _mapper.Map<IEnumerable<User>, IEnumerable<UserResources>>(users);

            return resources;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveUserResources resources) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            var users = _mapper.Map<SaveUserResources, User>(resources);
            var result = await _userService.SaveAsync(users);

            if (!result.Success)
            {
                BadRequest(result.Message);
            }

            var userResources = _mapper.Map<User, SaveUserResources>(result.User);
            return Ok(userResources);

        }
        
        [Authorize()]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveUserResources resources)
        {
            if (!ModelState.IsValid)
            {
                 return BadRequest(ModelState.GetErrorMessages());
            }

            var user = _mapper.Map<SaveUserResources, User>(resources);
            var result = await _userService.UpdateAsync(user, id);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var userResources = _mapper.Map<User, SaveUserResources>(result.User);
            return Ok(userResources);
        }

        [Authorize()]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _userService.DeleteAsync(id);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var userResource = _mapper.Map<User, UserResources>(result.User);
            return Ok(userResource);
        }
    }
}
