namespace SetGetGo.Sdk.Models
{
    public class Payment
    {
        /// <summary>
        /// The amount.
        /// </summary>
        public decimal Amount { get; internal set; }

        /// <summary>
        /// The merchant address.
        /// </summary>
        public string MerchAddress { get; internal set; }

        /// <summary>
        /// The callback.
        /// </summary>
        public string Callback { get; internal set; }

        /// <summary>
        /// The affiliate identifier.
        /// </summary>
        public string AffiliateId { get; internal set; }
    }
}
