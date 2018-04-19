using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using SetGetGo.Sdk.Models;

namespace SetGetGo.Sdk.Tests
{
    [TestClass]
    public class SetGetGoTests
    {
        /// <summary>
        /// Test to ensure that the create payment completes successfully.
        /// </summary>
        /// <returns>A <see cref="Task"/> representation.</returns>
        [TestMethod]
        public async Task Test_CreatePaymentAsync_Success()
        {
            var sgg = new SetGetGo(isTestNet: true);

            var payment = new Payment
            {
                Amount = (decimal)0.0332,
                MerchAddress = "n1DUF2TJ3iq2mdhLqR4HPgmKrtEfo9ZJeE"
            };

            var resp = await sgg.CreatePaymentAsync(payment);

            Assert.AreEqual(resp["transaction"]["merchant-address"].Value<string>(), payment.MerchAddress, "the merchant address should match.");
            Assert.AreEqual(resp["transaction"]["amount"].Value<decimal>(), payment.Amount, "the amount should match.");
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
            var sgg = new SetGetGo(isTestNet: true);

            var payment = new Payment
            {
                Amount = (decimal)0.0332,
                MerchAddress = "n1DUF2TJ3iq2mdhLqR4HPgmKrtEfo9ZJeE"
            };

            // Create a  new payment.
            var resp = await sgg.CreatePaymentAsync(payment);

            // Get the data for the the payment that have been created.
            var status = await sgg.GetPaymentAsync(resp["transaction"]["payment-address"].Value<string>());

            Assert.AreEqual(status["transaction"]["merchant-address"].Value<string>(), payment.MerchAddress, "the merchant address should match.");
            Assert.AreEqual(status["transaction"]["amount"].Value<decimal>(), payment.Amount, "the amount should match.");
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
            var sgg = new SetGetGo(isTestNet: true);

            var rate = await sgg.GetRateAsync("GBP");

            Assert.AreEqual(rate["rate"]["name"].Value<string>(),"GBP", "the currency code should be GBP.");
            Assert.AreEqual(rate["error"].Value<string>(), "ok", "the get reate should have been successfull.");
        }
    }
}
