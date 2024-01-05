using CartService.Models;
using CartService.Models.Dtos;

namespace CartService.Services.IServices
{
    public interface ICart
    {
        Task<Cart> GetCart (string cartId);
        Task<string> AddItemToCart(AddCart cart);

        Task<string> RemoveItemFromCart (AddCart cart);
        Task<string> UpdateCartItemQuantity(QuantityRequest request);
        Task ApplyCoupon(CouponRequest request);
        Task CheckOutCart (string  cartId);
    }
}
