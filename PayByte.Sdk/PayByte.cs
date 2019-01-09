using Newtonsoft.Json.Linq;
using PayByte.Sdk.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace PayByte.Sdk
{
    /// <summary>
    /// 
    /// </summary>
    public class PayByte : IPayByte
    {
        /// <summary>
        /// True to interact with the Testnet, False to interact with Mainnet.
        /// </summary>
        public bool IsTestnet { get; set; }

        /// <summary>
        /// The singleton object representing the HTTP client.
        /// </summary>
        private static HttpClient httpClient;

        /// <summary>
        /// The base URL.
        /// </summary>
        private string baseUrl = "https://paybyte.io";

        /// <summary>
        /// Initializes a new instance of the PayByte object.
        /// </summary>
        /// <param name="isTestNet">True if working on testnet, False otherwise.</param>
        public PayByte(bool isTestNet)
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
            if (string.IsNullOrWhiteSpace(payment.ApiKey) || payment.Amount <= 0 || string.IsNullOrWhiteSpace(payment.Coin))
            {
                throw new System.ArgumentNullException("Invalid input params");
            }

            var queryString = $"amount={payment.Amount}&api_key={payment.ApiKey}&testnet={this.IsTestnet}&coin={payment.Coin}";

            if (!string.IsNullOrWhiteSpace(payment.Callback))
            {
                queryString += $"&callback={payment.Callback}";
            }

            if (!string.IsNullOrWhiteSpace(payment.AffiliateId))
            {
                queryString += $"&affId={payment.AffiliateId}";
            }

            if (!string.IsNullOrWhiteSpace(payment.ReturnUrl))
            {
                queryString += $"&return_url={payment.ReturnUrl}";
            }

            using (var resp = await httpClient.GetAsync($"{baseUrl}/api/create-payment?{queryString}"))
            {
                resp.EnsureSuccessStatusCode();
                return JObject.Parse(await resp.Content.ReadAsStringAsync());
            } 
        }

        /// <summary>
        /// Gets the payment data about a specified payment address.
        /// </summary>
        /// <param name="paymentAddress">The payment address.</param>
        /// <returns>The <see cref="JObject"/> representation of the payment status.</returns>
        public async Task<JObject> GetPaymentAsync(string paymentId)
        {
            if (string.IsNullOrWhiteSpace(paymentId))
            {
                throw new System.ArgumentNullException("Invalid payment id.");
            }

            var queryString = $"payment_id={paymentId}&testnet={this.IsTestnet}"; 

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
