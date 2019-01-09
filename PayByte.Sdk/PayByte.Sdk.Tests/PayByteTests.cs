using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using PayByte.Sdk.Models;
using System.Threading.Tasks;

namespace PayByte.Sdk.Tests
{
    [TestClass]
    public class PayByteTests
    {
        private string merchantApiKey = "[insert your merchant api key here]";

        /// <summary>
        /// Test to ensure that the create payment completes successfully.
        /// </summary>
        /// <returns>A <see cref="Task"/> representation.</returns>
        [TestMethod]
        public async Task Test_CreatePaymentAsync_Success()
        {
            var payByte = new PayByte(isTestNet: true);

            var payment = new Payment
            {
                Amount = (decimal)0.0332,
                ApiKey = merchantApiKey,
                Coin = "BTC"
            };

            var resp = await payByte.CreatePaymentAsync(payment);

            Assert.AreEqual(resp["transaction"]["amount"].Value<decimal>(), payment.Amount, "the amount should match.");
            Assert.AreEqual(resp["transaction"]["status"].Value<string>(), "pending", "the status should be pending.");
            Assert.IsTrue(!string.IsNullOrWhiteSpace(resp["transaction"]["payment-address"].Value<string>()), "the payment address should not be empty.");
            Assert.AreEqual(resp["error"].Value<string>(), "ok", "the payment should have been correctly created.");
        }

        /// <summary>
        /// Test to ensure that the get payment completes successfully.
        /// </summary>
        /// <returns>A <see cref="Task"/> representation.</returns>
        [TestMethod]
        public async Task Test_GetPaymentAsync_Success()
        {
            var payByte = new PayByte(isTestNet: true);

            var payment = new Payment
            {
                Amount = (decimal)0.0332,
                ApiKey = merchantApiKey,
                Coin = "BTC"
            };

            // Create a  new payment.
            var resp = await payByte.CreatePaymentAsync(payment);

            // Get the data for the the payment that have been created.
            var status = await payByte.GetPaymentAsync(resp["transaction"]["payment-id"].Value<string>());

            Assert.AreEqual(status["transaction"]["payment-id"].Value<string>(), resp["transaction"]["payment-id"].Value<string>(), "the payment identifier should match.");
            Assert.AreEqual(status["transaction"]["amount"].Value<decimal>(), payment.Amount, "the amount should match.");
            Assert.AreEqual(status["transaction"]["status"].Value<string>(), "pending", "the status should be pending.");
            Assert.IsTrue(!string.IsNullOrWhiteSpace(status["transaction"]["payment-address"].Value<string>()), "the payment address should not be empty.");
            Assert.AreEqual(status["error"].Value<string>(), "ok", "the payment should have been correctly created.");
        }

        /// <summary>
        /// Test to ensure that get rate completes successfully.
        /// </summary>
        /// <returns>A <see cref="Task"/> representation.</returns>
        [TestMethod]
        public async Task Test_GetRateAsync_Success()
        {
            var payByte = new PayByte(isTestNet: true);

            var rate = await payByte.GetRateAsync("GBP");

            Assert.AreEqual(rate["rate"]["name"].Value<string>(),"GBP", "the currency code should be GBP.");
            Assert.AreEqual(rate["error"].Value<string>(), "ok", "the get reate should have been successfull.");
        }
    }
}
