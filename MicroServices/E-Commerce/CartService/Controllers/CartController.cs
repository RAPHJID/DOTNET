using CartService.Models.Dtos;
using CartService.Models;
using CartService.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace CartService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICart _cartService;

        public CartController(ICart cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("{cartId}")]
        public async Task<IActionResult> GetCart(string cartId)
        {
            var cart = await _cartService.GetCart(cartId);
            return Ok(cart);
        }

        [HttpPost("addItem")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddItemToCart(AddCart cart)
        {
            var result = await _cartService.AddItemToCart(cart);
            return Ok(result);
        }

        [HttpDelete("removeItem")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveItemFromCart( AddCart cart)
        {
            var result = await _cartService.RemoveItemFromCart(cart);
            return Ok(result);
        }

        [HttpPut("updateQuantity")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCartItemQuantity(QuantityRequest request)
        {
            var result = await _cartService.UpdateCartItemQuantity(request);
            return Ok(result);
        }

        [HttpPost("applyCoupon")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ApplyCoupon(CouponRequest request)
        {
            await _cartService.ApplyCoupon(request);
            return Ok("Coupon applied successfully");
        }

        [HttpPost("checkout/{cartId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CheckoutCart(string cartId)
        {
            await _cartService.CheckOutCart(cartId);
            return Ok("Cart checked out successfully");
        }
    }
}
