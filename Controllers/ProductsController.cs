using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using Shop.API.Entities;
using Shop.API.Models;
using Shop.API.ResourceParameters;
using Shop.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shop.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductsController(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }



        /// Get all the products in the shop
        [HttpGet()]
        [HttpHead]
        public ActionResult <IEnumerable<ProductDto>> GetProducts()
        {
            var productsFromRepo = _productRepository.GetProducts();

            //Ok Status code
            return Ok(_mapper.Map<IEnumerable<ProductDto>>(productsFromRepo));
        }

        /// Get a single product by Id
        [HttpGet("{productId}", Name = "GetProduct")]
        public IActionResult GetProduct(Guid productId)
        {

            if (_productRepository.ProductExists(productId))
            {

                var productFromRepo = _productRepository.GetProduct(productId);

                //Ok Status code
                return Ok(_mapper.Map<ProductDto>(productFromRepo));
            }

            //Not Found Status code
            else return NotFound();
        }

        /// Get the products that contains certain description or type
        [HttpGet("search")]
        public IActionResult GetProducts([FromQuery] ProductsResourceParameters productsResourceParameters)
        {
            var productsFromRepo = _productRepository.GetProducts(productsResourceParameters);

            //Not Found Status code
            if (productsFromRepo.Count()==0) return NotFound();

            //Ok Status code
            else return Ok(_mapper.Map<IEnumerable<ProductDto>>(productsFromRepo));
        }

        /// Add a new product in the shop
        [HttpPost()]
        public ActionResult<ProductDto> AddProduct([FromBody] ProductForCreationDto productForCreationDto)
        {
            var product = _mapper.Map<Entities.Product>(productForCreationDto);
            _productRepository.AddProduct(product);
            
            if(!_productRepository.Save())
            throw new Exception("Creating an product failed on save.");

            var productToReturn = _mapper.Map<ProductDto>(product);

            return CreatedAtRoute("GetProduct",
                new { productId = productToReturn.id },
                productToReturn);
        }

        //Update a product in the shop
        [HttpPut("{productId}")]
        public IActionResult UpdateProduct([FromRoute] Guid productId,[FromBody] ProductForUpdateDto productForUpdateDto)
        {
            if (_productRepository.ProductExists(productId))
            {
                var productFromRepo = _productRepository.GetProduct(productId);
                _mapper.Map(productForUpdateDto,productFromRepo);

                _productRepository.UpdateProduct();

                //Successful Status code
                return NoContent();
            }

            //Not Found Status code
            else return NotFound();
               
        }

        //Delete a product in the shop
        [HttpDelete("{productId}")]
        public IActionResult DeleteProduct(Guid productId)
        {
            if (_productRepository.ProductExists(productId))
            {
                _productRepository.DeleteProduct(productId);
                _productRepository.Save();

                //Successful Status code
                return NoContent();
            }

            //Not Found Status code
            else return NotFound();

        }

        //Supporting Options
        [HttpOptions]
        public IActionResult GetProductsOptions()
        {
            Response.Headers.Add("Allow", "GET,OPTIONS,POST,PUT,DELETE,PATCH");
            return Ok();
        }

        [HttpPatch("{productId}", Name = "PartiallyUpdateProduct")]
        public IActionResult PartiallyUpdateProduct(Guid productId,
            [FromBody] JsonPatchDocument<ProductForUpdateDto> patchDoc)
        {
            if (_productRepository.ProductExists(productId))
            {
                var productFromRepo = _productRepository.GetProduct(productId);
                var productToPatch = _mapper.Map<ProductForUpdateDto>(productFromRepo);

                patchDoc.ApplyTo(productToPatch,ModelState);

                //Validate model
                if(!TryValidateModel(productToPatch))
                {
                    return ValidationProblem(ModelState);
                }

                _mapper.Map(productToPatch,productFromRepo);

                _productRepository.UpdateProduct();


                //Successful Status code
                return NoContent();
            }

            //Not Found Status code
            else return NotFound();
        }

        // Returning validation problems from Controller
        public override ActionResult ValidationProblem(
            [ActionResultObjectValue] ModelStateDictionary modelStateDictionary)
        {
            var options = HttpContext.RequestServices
                .GetRequiredService<IOptions<ApiBehaviorOptions>>();
            return (ActionResult)options.Value.InvalidModelStateResponseFactory(ControllerContext);
        }

    }
}
