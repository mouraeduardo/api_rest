using api_rest.Domain.Models;
using api_rest.Resources;
using api_rest.Services;
using api_rest.Extensions;
using api_rest.Communication;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;



namespace api_rest.Controllers
{
    [Route("/api/[Controller]")]
    [Authorize()]
    public class CategoriesController : Controller
    {
       private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryService categoryService, IMapper mapper)
        {
             _categoryService = categoryService;
            _mapper = mapper;
        }


        /// <summary>
        ///  Realiza um Listagem das categorias cadastrados
        /// </summary>
        /// <returns> lista de categorias </returns>
        [SwaggerResponse(statusCode:200 , description: "Sucesso ao realizar busca em categories", type : typeof(SaveCategoryResources))]
        [SwaggerResponse(statusCode:401 , description: "Usuario não Autorizado")]
        [HttpGet]
        public async Task<IEnumerable<CategoryResources>> GetAllAsync()
        {
            var categories = await _categoryService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryResources>>(categories);

            return resources;
        }



        /// <summary>
        /// Realiza o cadastro de um novo tipo de categoria
        /// </summary>
        /// <param name="Nova Categoria"></param>
        /// <returns>retorna dados da nova categoria</returns>
        [SwaggerResponse(statusCode: 200, description: "Categoria Cadastrado", type: typeof(CategoryResources))]
        [SwaggerResponse(statusCode: 400, description: "Erro Encontrado", type: typeof(string))]
        [SwaggerResponse(statusCode: 401, description: "Usuario não Autorizado")]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveCategoryResources resources)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var category = _mapper.Map<SaveCategoryResources, Category>(resources);
            var result = await _categoryService.SaveAsync(category);

            if (!result.Success)
                return BadRequest(result.Message);

            var categoryResource = _mapper.Map<Category, CategoryResources>(result.Category);
            return Ok(categoryResource);
        }


        /// <summary>
        ///  Realiza uma atualização em uma categoria determinada
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Novos Dados da Categoria"></param>
        /// <returns> retorna a categoria com dados atualizado </returns>
        [SwaggerResponse(statusCode: 200, description: "Categoria Atualizada", type: typeof(CategoryResources))]
        [SwaggerResponse(statusCode: 400, description: "Categoria Nao existe", type: typeof(string))]
        [SwaggerResponse(statusCode: 401, description: "Usuario não Autorizado")]

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveCategoryResources resources)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            var category = _mapper.Map<SaveCategoryResources, Category>(resources);
            var result = await _categoryService.UpdateAsync(id, category);

            if (!result.Success)
            {
                return BadRequest(result.Message);               
            }

            var categoryResource = _mapper.Map<Category, CategoryResources>(result.Category);
            return Ok(categoryResource);
        }


        /// <summary>
        ///  Realiza uma remoção em uma categoria determinada
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dados apagados"></param>
        /// <returns> retorna a categoria removida </returns>
        [SwaggerResponse(statusCode: 200, description: "Categoria Deletada", type: typeof(CategoryResources))]
        [SwaggerResponse(statusCode: 400, description: "Categoria Nao existe", type: typeof(string))]
        [SwaggerResponse(statusCode: 401, description: "Usuario não Autorizado")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _categoryService.DeleteAsync(id);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var categoryResource = _mapper.Map<Category, CategoryResources>(result.Category);
            return Ok(categoryResource);
        }
    }
}
