using OrderService.Models;
using OrderService.Models.Dtos;
using OrderService.Services.IServices;
using OrderService.Data;
using E_MessageBus;
using Microsoft.EntityFrameworkCore;
using Stripe;
using Stripe.Checkout;

namespace OrderService.Services
{
    public class OrdersService : IOrder
    {
        private readonly AppDbContext _context;
        private readonly IProduct _productService;
        private readonly IUser _userService;
        private readonly IMessageBus _messageBus;
        public OrdersService(AppDbContext context, IProduct productService, IUser user, IMessageBus message)
        {
            _context = context;
            _productService = productService;
            _userService = user;
            _messageBus = message;

        }
        public async Task<string> AddOrder(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return "Order Added Successfully!!";
        }

        public async Task<List<Order>> GetAllOrders(Guid userId)
        {
            return await _context.Orders.Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task<Order> GetOrderById(Guid Id)
        {
            return await _context.Orders.Where(x => x.OrderId == Id).FirstOrDefaultAsync();
        }

        public async Task<StripeRequestDto> MakePayments(StripeRequestDto stripeRequestDto)
        {
            var order = await _context.Orders.Where(x => x.OrderId == StripeRequestDto.OrderId).FirstOrDefaultAsync();
            var product = await _productService.GetById(order.ProductId);
            var options = new SessionCreateOptions()
            {
                SuccessUrl = stripeRequestDto.ApprovedUrl,
                CancelUrl = stripeRequestDto.CancelUrl,
                Mode = "payment",
                LineItems = new List<SessionLineItemOptions>()
            };
            var item = new SessionLineItemOptions()
            {
                PriceData = new SessionLineItemPriceDataOptions()
                {
                    UnitAmount = (long)order.TotalAmount * 100,
                    Currency = "$",

                    ProductData = new SessionLineItemPriceDataProductDataOptions()
                    {
                        Name = product.ProductName,
                        Description = product.ProductDescription,
                        Images = new List<string> { "https://www.google.com/imgres?imgurl=https%3A%2F%2Fwww.macworld.com%2Fwp-content%2Fuploads%2F2023%2F11%2FiPhone-14-Plus-review-hero.jpg%3Fquality%3D50%26strip%3Dall%26w%3D1024&tbnid=Ke07k41wfSrjJM&vet=12ahUKEwjT-OC76raDAxXxvScCHSLvC2cQMygIegUIARC_AQ..i&imgrefurl=https%3A%2F%2Fwww.macworld.com%2Farticle%2F2104222%2Fiphone-15-plus-review.html&docid=tH8lw9jIcKQ40M&w=1024&h=716&q=iphone%2015&ved=2ahUKEwjT-OC76raDAxXxvScCHSLvC2cQMygIegUIARC_AQ" }
                    }
                },
                Quantity = 1


            };

            options.LineItems.Add(item);

 

            var DiscountObj = new List<SessionDiscountOptions>()
            {
                new SessionDiscountOptions()
                {
                    Coupon=order.CouponCode
                }
            };

            if (order.Discount > 0)
            {
                options.Discounts = DiscountObj;

            }
            var service = new SessionService();
            Session session = service.Create(options);

            stripeRequestDto.StripeSessionUrl = session.Url;
            stripeRequestDto.StripeSessionId = session.Id;

            order.StripeSessionId = session.Id;
            order.Status = "Ongoing";
            await _context.SaveChangesAsync();

            return stripeRequestDto;
        }

        public async Task saveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ValidatePayments(Guid OrderId)
        {
            var ordering = await _context.Orders.Where(x => x.OrderId == OrderId).FirstOrDefaultAsync();

            var service = new SessionService();
            Session session = service.Get(ordering.StripeSessionId);

            PaymentIntentService paymentIntentService = new PaymentIntentService();

            PaymentIntent paymentIntent = paymentIntentService.Get(session.PaymentIntentId);

            if (paymentIntent.Status == "succeeded")
            {
                //Payment Is Successfull

                ordering.Status = "Paid";
                ordering.PaymentIntent = paymentIntent.Id;
                await _context.SaveChangesAsync();
                var user = await _userService.GetUserById(ordering.UserId.ToString());

                if (string.IsNullOrWhiteSpace(user.Email))
                {
                    return false;
                }
                else
                {
                    var reward = new RewardsDto()
                    {
                        OrderId = ordering.OrderId,
                        TotalAmount = ordering.TotalAmount,
                        Name = user.Name,
                        Email = user.Email

                    };
                    await _messageBus.PublishMessage(reward, "Order added");
                }

                return true;

            }
            return false;
        }

    }
}
