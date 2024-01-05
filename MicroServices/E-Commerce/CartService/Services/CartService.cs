using CartService.Data;
using CartService.Models;
using CartService.Models.Dtos;
using CartService.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace CartService.Services
{
    public class CartService : ICart
    {
        private readonly AppDbContext _appDbContext;
        public CartService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<string> AddItemToCart(AddCart cart)
        {
            var carts = await GetOrCreateCart(cart.CartId);
            //item exists?
            var existingItem = carts.Find(x => x.ProductId == cart.ProductId);
            if (existingItem != null)
            {
                existingItem.Quantity += cart.Quantity;
            }
            else
            {
                carts.Items.Add(cart);
            }
            carts.TotalPrice = CalculateTotalPrice(carts.Items);
            await _appDbContext.SaveChangesAsync();
            return "Item added To Cart!!";
        }

        public async Task ApplyCoupon(CouponRequest request)
        {
            var cart = await GetOrCreateCart(request.CartId);
            //Coupon Logic

            cart.TotalPrice = CalculateTotalPrice(cart.Items);
            await _appDbContext.SaveChangesAsync(); 
        }

        public async Task CheckOutCart(string cartId)
        {
            var cart = await GetCart(cartId);

            cart.Items.Clear();
            _appDbContext.Carts.Remove(cart);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<Cart> GetCart(string cartId)
        {
            var cart = await _appDbContext.Carts
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.CartId == cartId);

            // If the cart doesn't exist, create a new one
            if (cart == null)
            {
                cart = new Cart { CartId = cartId };
                _appDbContext.Carts.Add(cart);
                await _appDbContext.SaveChangesAsync();
            }

            return cart;
        }

        public async Task<string> RemoveItemFromCart(AddCart cart)
        {
            var carts = await GetCart(cart.CartId);

            // Remove the item from the cart
            carts.Items.RemoveAll(item => item.ProductId == cart.ProductId);
            carts.TotalPrice = CalculateTotalPrice(carts.Items);
            await _appDbContext.SaveChangesAsync();

            return "Item removed from cart successfully";
        }

        public async Task<string> UpdateCartItemQuantity(QuantityRequest request)
        {
            var cart = await GetCart(request.CartId);

          
            var cartItem = cart.Items.Find(item => item.ProductId == request.ProductId);

            if (cartItem != null)
            {
                cartItem.Quantity = request.Quantity;
            }
            cart.TotalPrice = CalculateTotalPrice(cart.Items);

            await _appDbContext.SaveChangesAsync();

            return "Cart item quantity updated successfully";
        }
       
        private decimal CalculateTotalPrice(List<CartItem> items)
        {
            return items.Sum(item => item.Price * item.Quantity);
        }

        
        private async Task<Cart> GetOrCreateCart(string cartId)
        {
            var cart = await _appDbContext.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.CartId == cartId);

            // creates a new one if doesn't exist
            if (cart == null)
            {
                cart = new Cart { CartId = cartId };
                _appDbContext.Carts.Add(cart);
                await _appDbContext.SaveChangesAsync();
            }

            return cart;
        }
    }
}
