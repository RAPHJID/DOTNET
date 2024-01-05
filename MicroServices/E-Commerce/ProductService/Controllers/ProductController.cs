using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductService.Models;
using ProductService.Models.Dtos;
using ProductService.Services.IServices;

namespace ProductService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProduct _productService;
        private readonly IMapper _mapper;
        private readonly ResponseDto _responseDto;

        public ProductController(IMapper mapper, IProduct product)
        {
            _productService = product;
            _mapper = mapper;
            _responseDto = new ResponseDto();
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDto>> AddProduct(AddProductDto addProductDto)
        {
            var product = _mapper.Map<Product>(addProductDto);
            var res = await _productService.AddProduct(product);
            _responseDto.Result = res;
            return Created("", _responseDto);
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDto>> getProduct()
        {
            var res = await _productService.GetAllProducts();
            _responseDto.Result = res;
            return Ok(_responseDto);
        }

        [HttpGet("{Id}")]
        public async Task <ActionResult<ResponseDto>> getProduct(Guid Id)
        {
            var res = await _productService.GetProduct(Id);
            if(res == null)
            {
                _responseDto.Errormessage = "Product Not Found!!";
                return NotFound(_responseDto);
            }
            _responseDto.Result = res;
            return Ok(_responseDto);
            

        }
        [HttpDelete("{Id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseDto>> DeleteProduct(Guid Id)
        {
            var coupon = await _productService.GetProduct(Id);
            if (coupon == null)
            {
                _responseDto.Result = "Product Not Found";
                _responseDto.IsSuccess = false;
                return NotFound(_responseDto);
            }
            var res = await _productService.DeleteProduct(coupon);

          

            _responseDto.Result = res;
            return Ok(_responseDto);
        }

        [HttpPut("{Id}")]
       //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseDto>> updateProduct(Guid Id, AddProductDto UProduct)
        {
            var product = await _productService.GetProduct(Id);
            if (product == null)
            {
                _responseDto.Result = "Product Not Found";
                _responseDto.IsSuccess = false;
                return NotFound(_responseDto);
            }
            _mapper.Map(UProduct, product);
            var res = await _productService.UpdateProduct();
        
            _responseDto.Result = res;
            return Ok(_responseDto);
        }




    }
}
