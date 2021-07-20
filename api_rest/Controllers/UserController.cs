using api_rest.Domain.Models;
using api_rest.Domain.Services;
using api_rest.Extensions;
using api_rest.Resources;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
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

        /// <summary>
        ///  Realiza um Listagem dos usuarios cadastrados
        /// </summary>
        /// <returns> lista de usuarios </returns>
        [SwaggerResponse(statusCode:200 , description: "Sucesso ao listar os usuario", type : typeof(SaveUserResources))]
        [Authorize()]
        [HttpGet]
        public async Task<IEnumerable<UserResources>> GetAllAsync()
        {
            var users = await _userService.ListAsync();
            var resources = _mapper.Map<IEnumerable<User>, IEnumerable<UserResources>>(users);

            return resources;
        }

        /// <summary>
        /// Realiza o cadastro de um novo usuario
        /// </summary>
        /// <param name="Nova Categoria"></param>
        /// <returns>retorna dados do novo usuario</returns>
        [SwaggerResponse(statusCode:200 , description: "Sucesso ao Criar um usuario", type : typeof(SaveUserResources))]
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
        
        /// <summary>
        ///  Realiza uma atualização em um determinado usuario
        /// </summary>
        /// <param name="id"></param>
        /// <param name="NovosDadosDaCategoria"></param>
        /// <returns> retorna o usuario com dados atualizado </returns>
        [SwaggerResponse(statusCode:200 , description: "Sucesso ao atualizar um usuario", type : typeof(SaveUserResources))]
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

        /// <summary>
        /// Deleta um determinado usuario
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Retorna dados do usuario selecionado </returns>
        [SwaggerResponse(statusCode:200 , description: "Sucesso ao Deletar um usuario", type : typeof(SaveUserResources))]

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
