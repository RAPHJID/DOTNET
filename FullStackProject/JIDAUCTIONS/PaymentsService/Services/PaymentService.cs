using AuctionMessageBus;
using Microsoft.EntityFrameworkCore;
using PaymentsService.Data;
using PaymentsService.Models;
using PaymentsService.Models.Dtos;
using PaymentsService.Services.IServices;
using Stripe.Checkout;
using Stripe;

namespace PaymentsService.Services
{
    public class PaymentService : IPayment
    {
        private readonly AppDbContext _appDbContext;
        private readonly IArt _artService;
        private readonly IBidder _bidder;
        private readonly IBid _bidService;
        private readonly ResponseDto _responseDto;
        private readonly IMessageBus _messageBus;

        public PaymentService(AppDbContext appDbContext, IArt artService, IBidder bidder, IMessageBus message, IBid bidService)
        {
            _appDbContext = appDbContext;
            _artService = artService;
            _bidder = bidder;
            _responseDto = new ResponseDto();
            _messageBus = message;
            _bidService = bidService;

        }

        public async Task<string> AddPayment(Payment payment)
        {
            _appDbContext.Payments.Add(payment);
            await _appDbContext.SaveChangesAsync();
            return "Payment added Successfully!!";
        }

        public async Task<List<Payment>> GetAll(Guid bidderId)
        {
            return await _appDbContext.Payments.Where(x => x.BidderId == bidderId).ToListAsync();
        }

        public async Task<Payment> GetPaymentById(Guid Id)
        {
           return await _appDbContext.Payments.Where(x=>x.PaymentId == Id).FirstOrDefaultAsync();
        }

        public async Task<Payment> GetOrderbyBidId(Guid bidId)
        {
            var bidorder = await _appDbContext.Payments.Where(x => x.BidId == bidId).FirstOrDefaultAsync();
            if (bidorder != null)
            {
                _responseDto.ErrorMessage = "Bid id exists";

            }
            return bidorder;

        }

        public async Task<StripeRequestDto> MakePayments(StripeRequestDto stripeRequestDto)
        {
            var payment = await _appDbContext.Payments.Where(x => x.PaymentId == stripeRequestDto.PaymentId).FirstOrDefaultAsync();
            var art = await _artService.GetById(payment.ArtId);
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
                    UnitAmount = (long)payment.Amount * 100,
                    Currency = "kes",

                    ProductData = new SessionLineItemPriceDataProductDataOptions()
                    {
                        Name = art.Name,
                        Description = art.Description,
                        Images = new List<string> { "https://i.pinimg.com/236x/1d/82/bf/1d82bf92a53618c731321f67fc3b1273.jpg" }
                    }
                },
                Quantity = 1

            };
            options.LineItems.Add(item);

            var service = new SessionService();
            Session session = service.Create(options);

            // URL//ID

            stripeRequestDto.StripeSessionUrl = session.Url;
            stripeRequestDto.StripeSessionId = session.Id;

            payment.StripeSessionId = session.Id;
            payment.Status = "Ongoing";
            await _appDbContext.SaveChangesAsync();

            return stripeRequestDto;

        }

        public async Task saveChanges()
        {
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<bool> ValidatePayments(Guid PaymentId)
        {
            var payment = await _appDbContext.Payments.Where(x => x.PaymentId == PaymentId).FirstOrDefaultAsync();

            var service = new SessionService();
            Session session = service.Get(payment.StripeSessionId);

            PaymentIntentService paymentIntentService = new PaymentIntentService();

            PaymentIntent paymentIntent = paymentIntentService.Get(session.PaymentIntentId);

            if (paymentIntent.Status == "succeeded")
            {
                //the payment was success

                payment.Status = "Paid";
                payment.PaymentIntent = paymentIntent.Id;
                await _appDbContext.SaveChangesAsync();
                var user = await _bidder.GetUserById(payment.BidderId.ToString());

                if (string.IsNullOrWhiteSpace(user.Email))
                {
                    return false;
                }
                else
                {
                    
                    await _messageBus.PublishMessage("Payment Added");
                }

                
                return true;

            }
            return false;
        }

       
    }
    
}
