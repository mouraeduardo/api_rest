using api_rest.Domain.Models;
using api_rest.Domain.Services;
using api_rest.Extensions;
using api_rest.Resources;
using api_rest.Util;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading.Tasks;

namespace api_rest.Controllers
{
    
    [Route("api/[controller]")]
    public class TokenController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public TokenController(IConfiguration configuration, IUserService userService, IMapper mapper)
        {
            _mapper = mapper;
            _userService = userService;
            _configuration = configuration;
        }

        /// <summary>
        /// Realliza uma solicitação de login no sistema
        /// </summary>
        /// <param name="dados do login"></param>
        /// <returns>Token de autenticação</returns>
        [SwaggerResponse(statusCode:200 , description: "Sucesso ao Realizar login", type: typeof(OkObjectResult))]

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> ResquestTokenAsync([FromBody] AuthenticationResources resources)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState.GetErrorMessages());
                }

                var user = _mapper.Map<AuthenticationResources, User>(resources);
                var result = await _userService.FirstOrDefaultAsync(user.Username, user.Password);

                if (!result.Success)
                {
                    return BadRequest("Erro ao tentar realizar login");
                }

                var token = CryptoFunctions.GerarToken(user, _configuration["SecurityKey"]);

                return Ok(new
                {
                    token
                });

            }
            catch (Exception)
            {
                var message = "Error login";
                return BadRequest(new { error = true, result = new { message } });
            }

        }
    }
}
