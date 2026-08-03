using DriveHubMongo.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Razorpay.Api;

namespace DriveHubMongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly RazorpaySettings _razorpay;

        public PaymentController(IOptions<RazorpaySettings> razorpay)
        {
            _razorpay = razorpay.Value;
        }

        [HttpPost("create-order")]
        public IActionResult CreateOrder(CreateOrderRequest request)
        {
            RazorpayClient client = new RazorpayClient(
                _razorpay.Key,
                _razorpay.Secret);

            Dictionary<string, object> options = new Dictionary<string, object>();

            options.Add("amount", request.Amount * 100); // ₹99 => 9900 paise
            options.Add("currency", "INR");
            options.Add("receipt", Guid.NewGuid().ToString());

            Order order = client.Order.Create(options);

            return Ok(new
            {
                orderId = order["id"].ToString(),
                amount = Convert.ToInt32(order["amount"]),
                currency = order["currency"].ToString(),
                key = _razorpay.Key
            });
        }
    }
}
