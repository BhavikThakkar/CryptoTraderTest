# CryptoTrader.Tax Technical Interview

Your task for this part of the interview is to build out a simple billing service and payments form using Blazor and ASP.NET Core (two of our most widely used technologies in our production web app).

This repo provides all the boiler-plate code needed to complete this task as well as example services & components.

We hope you can spend about two hours on this project. If you can finish faster — great! If not, limit yourself and don't spend much longer than 3 hours max.


## Setup Steps

1. [.NET Core SDK 3.1](https://dotnet.microsoft.com/download/dotnet-core/)
2. [Node LTS](https://nodejs.org/en/)
3. [Visual Studio Community or VS Code](https://visualstudio.microsoft.com/downloads/)
4. Fork this repository
5. Open in Visual Studio

## Solution Overview

The solution is split up into three different projects:
1. CryptoTax.Data - Contains the Entity Framework context and domain models
2. CryptoTax.Web - Server side Blazor server application that contains business services and front-end components
3. CryptoTax.Test - NUnit testing project for all the other projects

To run the project, simply set `CryptoTax.Web` as the startup project in Visual Studio and start with a debugger. Upon after launching, you will be taken to a simplified report dashboard:

![Dashboard](https://i.imgur.com/sN3xmuZ.png)

# Your Task

At the moment, a user on this website is able to click the **View Report** button on the dashboard see their gains and losses without paying for the report. Your task is to fix that. 

### Part 1 - Implement Billing Service

We have provided an `IBillingService` interface with method signatures required to check if a user has access to a report and to calculate the price.


**Requirements:**

 - Read the pricing rules defined here: https://cryptotrader.tax/pricing (each tier is already saved on the `BillingTiers` Table)
 - Implement all given methods on the `IBillingService` interface to follow the given pricing rules (pay close attention to what transaction types count toward each billing tier)

**What we are looking for:**

 - Follow existing patterns & conventions in the solution
 - Check for edge cases / validate requests / handle exceptions
 - Include comments in code on non-trivial calculations / operations
 - Unit tests to cover both happy path and edge cases
 	- Ex: Scenario when a user imports more trades after purchasing a report

### Part 2 - Create Payment Form

The next step for this project is to utilize the service you just implement and wire it up to the Blazor web app so all the necessary checks are ran when a user clicks the **View Report** button.

When a user does not have access, they should be directed to a payment page. We want you to implement this payment page and payment form. 

**Requirements:**

 - Check if a user has access to a report after a user clicks the View
   Report button
  - If the access check succeeds, display the report details page.
  - If the access check fails, display the purchase form which should include the report price and prompt for:
	  - Credit card number
	  - Exiration date
	  - Security code
  - On valid submit, consume the provided `IPaymentService`
  	- No changes are needed to the `IPaymentService`
  - After successfully submitting payment, the user should be directed to the report detail page for that report.

**What we are looking for:**
- Easy to use and  accessible form style using [TailwindCSS](https://tailwindcss.com/) (already setup in the project)
- Edge cases tested and user inputs are validated on the payments form
- Handle exceptions thrown from `IPaymentService`


# Submitting

Once you have completed implementing this billing feature, send an email to **lucas@cryptotrader.tax** with a link to your forked repository. Our next step will be to schedule a call together to dicuss your implementation.
