using Newtonsoft.Json.Linq;
using SetGetGo.Sdk.Models;
using System.Threading.Tasks;

namespace SetGetGo.Sdk
{
    interface ISetGetGo
    {
        bool IsTestnet { get; set; }

        /// <summary>
        /// Creates a new payment.
        /// </summary>
        /// <param name="payment">The payment data.</param>
        /// <returns>The <see cref="JObject"/> representation of the payment response.</returns>
        Task<JObject> CreatePaymentAsync(Payment payment);

        Task<JObject> GetPaymentAsync(string paymentAddress);

        Task<JObject> GetRateAsync(string currencyCode);
    }
}
