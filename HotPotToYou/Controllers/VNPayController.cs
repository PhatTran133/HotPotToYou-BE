using HotPotToYou.Service.VNPay;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text;
using System.Web;

namespace HotPotToYou.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VNPayController : ControllerBase
    {
        // VNPay configuration
        public static string VnpPayUrl { get; } = "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html";
        public static string VnpReturnUrl { get; } = "http://localhost:8088/payment/return"; // Replace with your actual return URL after successful payment
        public static string VnpTmnCode { get; } = "P8Y3QRZ3"; // Replace with your actual VNPay TmnCode
        public static string SecretKey { get; } = "BDH8UD3Z9R70XJLIE5DGLLVNMOZFJTH2";
        public string VnpVersion { get; set; } = "2.1.0";
        public string VnpCommand { get; set; } = "pay";
        public string OrderType { get; set; } = "other"; // Update to match your order type logic

        private IHttpContextAccessor _httpContextAccessor;

        public VNPayController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost("Payment")]
        public IActionResult Payment([FromBody] VNPayPaymentRequest request)
        {
            string clientIPAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();

            PayLib pay = new PayLib();

            pay.AddRequestData("vnp_Version", VnpVersion);
            pay.AddRequestData("vnp_Command", VnpCommand);
            pay.AddRequestData("vnp_TmnCode", VnpTmnCode);
            pay.AddRequestData("vnp_Amount", request.Amount.ToString());
            pay.AddRequestData("vnp_BankCode", "");
            pay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
            pay.AddRequestData("vnp_CurrCode", "VND");
            pay.AddRequestData("vnp_IpAddr", clientIPAddress);
            pay.AddRequestData("vnp_Locale", "vn");
            pay.AddRequestData("vnp_OrderInfo", request.OrderInfo);
            pay.AddRequestData("vnp_OrderType", OrderType);
            pay.AddRequestData("vnp_ReturnUrl", VnpReturnUrl);
            pay.AddRequestData("vnp_TxnRef", Guid.NewGuid().ToString()); // Generate a unique transaction reference

            string paymentUrl = pay.CreateRequestUrl(VnpPayUrl, SecretKey);

            return Ok(new { PaymentUrl = paymentUrl });
        }

        [HttpGet("PaymentConfirm")]
        public IActionResult PaymentConfirm()
        {
            if (_httpContextAccessor.HttpContext.Request.Query.Count > 0)
            {
                var queryString = _httpContextAccessor.HttpContext.Request.QueryString.Value;
                var json = HttpUtility.ParseQueryString(queryString);

                long orderId = Convert.ToInt64(json["vnp_TxnRef"]);
                string orderInfo = json["vnp_OrderInfo"].ToString();
                long vnpayTranId = Convert.ToInt64(json["vnp_TransactionNo"]);
                string vnp_ResponseCode = json["vnp_ResponseCode"].ToString();
                string vnp_SecureHash = json["vnp_SecureHash"].ToString();
                var pos = queryString.IndexOf("&vnp_SecureHash");

                bool checkSignature = ValidateSignature(queryString.Substring(1, pos - 1), vnp_SecureHash, SecretKey);

                if (checkSignature && VnpTmnCode == json["vnp_TmnCode"].ToString())
                {
                    if (vnp_ResponseCode == "00")
                    {
                        // Payment successful
                        return Ok(new { Message = "Payment successful", OrderInfo = orderInfo });
                    }
                    else
                    {
                        // Payment failed
                        return BadRequest(new { Message = "Payment failed", ResponseCode = vnp_ResponseCode });
                    }
                }
                else
                {
                    // Invalid signature
                    return BadRequest(new { Message = "Invalid signature" });
                }
            }

            // No query string or invalid request
            return BadRequest(new { Message = "Invalid request" });
        }

        private bool ValidateSignature(string rspraw, string inputHash, string secretKey)
        {
            string myChecksum = PayLib.HmacSHA512(secretKey, rspraw);
            return myChecksum.Equals(inputHash, StringComparison.InvariantCultureIgnoreCase);
        }
    }

    public class VNPayPaymentRequest
    {
        public decimal Amount { get; set; }
        public string OrderInfo { get; set; }
        // Add more properties as needed for your payment request
    }
}
