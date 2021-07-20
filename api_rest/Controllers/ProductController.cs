using api_rest.Domain.Models;
using api_rest.Domain.Services;
using api_rest.Extensions;
using api_rest.Resources;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace api_rest.Controllers
{
    [Route("/api/[Controller]")]
    [Authorize()]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        /// <summary>
        /// Realiza um Listagem de produtos cadastrados
        /// </summary>
        /// <returns> retorna Lista de produtos</returns>
        /// <response code="200"></response>
        [SwaggerResponse(statusCode:200 , description: "Sucesso ao realizar busca de produtos", type : typeof(SaveProductResources))]
        [SwaggerResponse(statusCode: 401, description: "Usuario não Autorizado")]
        [HttpGet]
        public async Task<IEnumerable<ProductResources>> GetAllAsync()
        {
            var Products = await _productService.ListAsync();
            var productsResources = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductResources>>(Products);
            return productsResources;
        }


        /// <summary>
        /// Realiza o cadastro de novos produtos no banco de dados
        /// </summary>
        /// <param name="Novo Produto"></param>
        /// <returns> dados do produto salvo </returns>
        [SwaggerResponse(statusCode:200 , description: "Sucesso ao Criar um novo produto", type : typeof(SaveProductResources))]
        [SwaggerResponse(statusCode: 401, description: "Usuario não Autorizado")]

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveProductResources resources)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            var products = _mapper.Map<SaveProductResources, Product>(resources);
            var result = await _productService.SaveAsync(products);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            //var productResourses = _mapper.Map<Product, ProductResources>(result.Product);
            return Ok(result.Product);
        }

        /// <summary>
        ///  Realiza uma atualização no produto escolhido
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Novos Dados"></param>
        /// <returns>dados atualizados do produto</returns>
        [SwaggerResponse(statusCode:200 , description: "Sucesso ao atualizar um produto", type : typeof(SaveProductResources))]
        [SwaggerResponse(statusCode: 401, description: "Usuario não Autorizado")]
        [SwaggerResponse(statusCode: 400, description: "Erro encontrado", type: typeof(string))]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveProductResources resources)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            var product = _mapper.Map<SaveProductResources, Product>(resources);
            var result = await _productService.UpdateAsync(product, id);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var productResources = _mapper.Map<Product, ProductResources>(result.Product);
            return Ok(productResources);
        }

        /// <summary>
        /// Realizar a remoção de um produto determinado
        /// </summary>
        /// <param name="id"></param>
        /// <returns> dados do produto removido </returns>
        [SwaggerResponse(statusCode: 200, description: "Sucesso ao deletar um produto", type : typeof(SaveProductResources))]
        [SwaggerResponse(statusCode: 401, description: "Usuario não Autorizado")]
        [SwaggerResponse(statusCode: 400, description: "Erro encontrado", type : typeof(string))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _productService.DeleteAsync(id);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var productResource = _mapper.Map<Product, ProductResources>(result.Product);
            return Ok(productResource);
        }
    }
}
