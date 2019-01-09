namespace PayByte.Sdk.Models
{
    public class Payment
    {
        /// <summary>
        /// The amount.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// The callback.
        /// </summary>
        public string Callback { get; set; }

        /// <summary>
        /// The affiliate identifier.
        /// </summary>
        public string AffiliateId { get; set; }

        /// <summary>
        /// The coin identifier.
        /// </summary>
        public string Coin { get; set; }

        /// <summary>
        /// The merchant API key.
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        /// The return URL.
        /// </summary>
        public string ReturnUrl { get; set; }
    }
}
