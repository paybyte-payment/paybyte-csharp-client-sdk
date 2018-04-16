using SetGetGo.Sdk.Models;

namespace SetGetGo.Sdk
{
    interface ISetGetGo
    {
        bool IsTestnet { get; set; }

        PaymentResponse CreatePayment(Payment payment);

        PaymentResponse GetPayment(string paymentAddress);

        Rate GetRate(string currencyCode);

    }
}
