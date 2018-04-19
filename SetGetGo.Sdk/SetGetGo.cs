using Newtonsoft.Json.Linq;
using SetGetGo.Sdk.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace SetGetGo.Sdk
{
    /// <summary>
    /// 
    /// </summary>
    public class SetGetGo : ISetGetGo
    {
        /// <summary>
        /// True if testnet, False otherwise.
        /// </summary>
        public bool IsTestnet { get; set; }

        /// <summary>
        /// The singleton object of the HTTP client.
        /// </summary>
        private static HttpClient httpClient;

        /// <summary>
        /// The base URL.
        /// </summary>
        private string baseUrl = "https://setgetgo.com";

        /// <summary>
        /// Initializes a new instance of the SetGetGo object.
        /// </summary>
        /// <param name="isTestNet">True if working on testnet, False otherwise.</param>
        public SetGetGo(bool isTestNet)
        {
            this.IsTestnet = isTestNet;

            if (httpClient == null)
            {
                httpClient = new HttpClient();
            }
        } 

        /// <summary>
        /// Creates a new payment.
        /// </summary>
        /// <param name="payment">The payment data.</param>
        /// <returns>The <see cref="JObject"/> representation of the payment response.</returns>
        public async Task<JObject> CreatePaymentAsync(Payment payment)
        {
            if (string.IsNullOrWhiteSpace(payment.MerchAddress) || payment.Amount <= 0)
            {
                throw new System.ArgumentNullException("Invalid merchant address or amount values");
            }

            var queryString = $"amount={payment.Amount}&merch_addr={payment.MerchAddress}&testnet={this.IsTestnet}";

            if (!string.IsNullOrWhiteSpace(payment.Callback))
            {
                queryString += $"&callback={payment.Callback}";
            }

            if (!string.IsNullOrWhiteSpace(payment.AffiliateId))
            {
                queryString += $"&affId={payment.AffiliateId}";
            }

            using (var resp = await httpClient.GetAsync($"{baseUrl}/api/create-payment?{queryString}"))
            {
                resp.EnsureSuccessStatusCode();
                return JObject.Parse(await resp.Content.ReadAsStringAsync());
            } 
        }

        /// <summary>
        /// Gets the payment data by payment address.
        /// </summary>
        /// <param name="paymentAddress">The payment address.</param>
        /// <returns>The <see cref="JObject"/> representation of the payment status.</returns>
        public async Task<JObject> GetPaymentAsync(string paymentAddress)
        {
            if (string.IsNullOrWhiteSpace(paymentAddress))
            {
                throw new System.ArgumentNullException("Invalid payment address.");
            }

            var queryString = $"payment_addr={paymentAddress}&testnet={this.IsTestnet}"; 

            using (var resp = await httpClient.GetAsync($"{baseUrl}/api/get-payment-status?{queryString}"))
            {
                resp.EnsureSuccessStatusCode();
                return JObject.Parse(await resp.Content.ReadAsStringAsync());
            }
        }

        /// <summary>
        /// Gets the BTC exchange rate against the input currency code.
        /// </summary>
        /// <param name="currencyCode">The currency code.</param>
        /// <returns>The <see cref="JObject"/> representation of the rate data.</returns>
        public async Task<JObject> GetRateAsync(string currencyCode)
        {
            if (string.IsNullOrWhiteSpace(currencyCode))
            {
                throw new System.ArgumentNullException("Invalid currency code.");
            }

            var queryString = $"currency={currencyCode}";

            using (var resp = await httpClient.GetAsync($"{baseUrl}/api/get-rate?{queryString}"))
            {
                resp.EnsureSuccessStatusCode();
                return JObject.Parse(await resp.Content.ReadAsStringAsync());
            }
        }
    }
}
