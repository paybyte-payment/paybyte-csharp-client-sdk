using Newtonsoft.Json.Linq;
using SetGetGo.Sdk.Models;
using System.Threading.Tasks;

namespace SetGetGo.Sdk
{
    interface ISetGetGo
    {
        /// <summary>
        /// True to interact with the Testnet, False to interact with Livenet.
        /// </summary>
        bool IsTestnet { get; set; }

        /// <summary>
        /// Creates a new payment.
        /// </summary>
        /// <param name="payment">The payment data.</param>
        /// <returns>The <see cref="JObject"/> representation of the payment response.</returns>
        Task<JObject> CreatePaymentAsync(Payment payment);

        /// <summary>
        /// Gets the payment data by payment address.
        /// </summary>
        /// <param name="paymentAddress">The payment address.</param>
        /// <returns>The <see cref="JObject"/> representation of the payment status.</returns>
        Task<JObject> GetPaymentAsync(string paymentAddress);

        /// <summary>
        /// Gets the BTC exchange rate against the input currency code.
        /// </summary>
        /// <param name="currencyCode">The currency code.</param>
        /// <returns>The <see cref="JObject"/> representation of the rate data.</returns>
        Task<JObject> GetRateAsync(string currencyCode);
    }
}
