using E_Commerce.Core.DTO;
using E_Commerce.Core.interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace E_Commerce.Api.Controllers
{

    public class PaymentController : APIBaseController
    {
        private readonly IPaymentService _paymentService;
        private readonly IConfiguration _configuration;

        public PaymentController(IPaymentService paymentService, IConfiguration configuration)
        {
            _paymentService = paymentService;
            _configuration = configuration;
        }
        const string endpointSecret = "whsec_b083537b688510087e5b485bb06e926a8eac36ce1fc88e0e501bb688b241b486";

        [HttpPost]
        public async Task<ActionResult<CustomerCartDto>> CreatePaymentIntent(CustomerCartDto customerCartDto)
        {
            var cart = await _paymentService.CreateOrUpdateIntentForExistingOrder(customerCartDto);
            return Ok(cart);
        }


        // This is your Stripe CLI webhook secret for testing your endpoint locally.

        [HttpPost("Webhook")]
        public async Task<IActionResult> Index()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            try
            {
                var stripeEvent = EventUtility.ConstructEvent(json,
                    Request.Headers["Stripe-Signature"], endpointSecret);

                // Handle the event
                if (stripeEvent.Type == Events.PaymentIntentSucceeded)
                {
                }
                // ... handle other event types
                else
                {
                    Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
                }

                return Ok();
            }
            catch (StripeException e)
            {
                return BadRequest();
            }
        }



    }
}
