namespace SetGetGo.Sdk.Models
{
    public class Payment
    {
        /// <summary>
        /// The amount.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// The merchant address.
        /// </summary>
        public string MerchAddress { get; set; }

        /// <summary>
        /// The callback.
        /// </summary>
        public string Callback { get; set; }

        /// <summary>
        /// The affiliate identifier.
        /// </summary>
        public string AffiliateId { get; set; }
    }
}
