
namespace HotelBusiness.Models
{
    public class PaymentRequest
    {
        public string OrderId { get; set; }
        public decimal Amount { get; set; }
        public string OrderInfo { get; set; }
        public string BankCode { get; set; }
    }
}