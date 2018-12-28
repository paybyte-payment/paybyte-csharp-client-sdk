# Getting started
The C# SDK allowes you to accept Bitcoin payments from your C# .NET app using the PayByte REST API.
The SDK has been designed to act as a wrapper for all the main PayByte functionalities, 
allowing the developer to focus on the actual purchasing flow rather than on the API's implementation details.

The SDK is also available as a NuGet download from this URL:  [tbd]

## Create a new payment

```csharp
var sgg = new SetGetGo(isTestnet: true);
var payment = new Payment
{
   Amount = (decimal)0.0332,
   ApiKey = "[insert api key]",
   Coin = "BTC",
   Callback = "https://test.com/callback?invoiceId=123"  
};
var paymentResponse = await sgg.CreatePaymentAsync(payment);
```

The paymentResponse will contain the JSON representation of the payment response. 

## Get a payment data

Simply provide the payment address to retrieve all data related to a transaction.

```csharp
var sgg = new SetGetGo(isTestnet: true);
var paymentId = "cf565888-28f5-430e-85af-b34b945ce20f";
var paymentData = await sgg.GetPaymentAsync(paymentId: paymentId);
```

The SDK will return a JObject representation of the transaction data.

## Get rates

Get the exchange rates of the BTC against the provided currency code.

```csharp
var sgg = new SetGetGo(isTestnet: true); 
var rates = await sgg.GetRateAsync(currency: "GBP");
```
