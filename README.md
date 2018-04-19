# Getting started
The C# SDK allowes you to accept Bitcoin payments from you C# .NET app using the SetGetGo REST API.
The SDK has been designed to act as a wrapper for all the main SetGetGo functionalities, 
allowing the developer to focus on the actual purchasing flow rather than on the API's implementation details.

The SDK is also available as a NuGet download from this URL:  [tbd]

## Create a payment

```csharp
var sgg = new SetGetGo(isTestnet: true);
var payment = new Payment
{
   Amount = 0.050, 
   MerchAddress = "moJCudny79kaq2ZAjYtLxHYnZAxajjnPzD", 
   Callback = "https://test.com/callback?invoiceId=123"  
};
var paymentResponse = await sgg.CreatePaymentAsync(payment);
```

The paymentResponse will contain the JSON representation of the payment response. 

## Retrieve a payment

Simply provide the payment address to retrieve all data related to a transaction.

```csharp
var sgg = new SetGetGo(isTestnet: true);
var paymentAddress = "mifPy1geLcVi1FuaeRkVHpV6uy1oxmTJMK";
var paymentData = await sgg.GetPaymentAsync(paymentAddress: paymentAddress);
```

The SDK will return a JObject representation fo the transaction data.
