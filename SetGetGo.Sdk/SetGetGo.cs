using SetGetGo.Sdk.Models;

namespace SetGetGo.Sdk
{
    public class SetGetGo : ISetGetGo
    {
        public bool IsTestnet { get; set; }

        public SetGetGo(bool isTestNet)
        {
            this.IsTestnet = isTestNet;
        } 

        public PaymentResponse CreatePayment(Payment payment)
        {
            var response = new PaymentResponse();

            return response;
        }

        public PaymentResponse GetPayment(string paymentAddress)
        {
            var response = new PaymentResponse();

            return response;
        }

        public Rate GetRate(string currencyCode)
        {
            var rate = new Rate();

            return rate;
        }
    }
}
