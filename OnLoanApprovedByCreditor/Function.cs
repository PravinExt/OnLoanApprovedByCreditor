using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Amazon.Lambda.Core;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace OnLoanApprovedByCreditor
{
    public class Function
    {
        private IConfiguration _configuration;
        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        //public Loan FunctionHandler(Loan input, ILambdaContext context)
        //{
        //    Loan loandetail = new Loan();
        //    loandetail.Update_LoanInfo(input);

        //    return input;
        //}

        public async Task FunctionHandler(Loan input, ILambdaContext context)
        {
            input.LoanApplication_Status = 8;
            input.LoanApplication_BankerComment = "Closed by External Service";

            string json = JsonConvert.SerializeObject(input);
            StringContent data = new StringContent(json, Encoding.UTF8, "application/json");

            string CallBackUrl = Environment.GetEnvironmentVariable("LoanApprovalURL");
            //string CallBackUrl = "https://g9yh14f7ve.execute-api.ap-south-1.amazonaws.com/Authorizeddev/loanapproval/banker";
            string url = CallBackUrl + "/" + input.External_ID.ToString();
            var client = new HttpClient();

            Uri myURI = new Uri(url);
            //Need Banker Authorization access token
            client.DefaultRequestHeaders.Add("Authorization", "eyJraWQiOiJrSURWK2lTS0RvMU5mMGJHbjhSUlpiMnNIdE1jYVJmS2JZMDB6eWVXM29rPSIsImFsZyI6IlJTMjU2In0.eyJzdWIiOiI5ZWIzYzZlNy1lMjM5LTRlZTQtYWUxOC1mMjNiNmU0NzhkN2UiLCJldmVudF9pZCI6IjRjNDUwOTk0LWRlN2QtNDE5Ny04YWJhLWRmMmU2YTFkZTRlOSIsInRva2VuX3VzZSI6ImFjY2VzcyIsInNjb3BlIjoiYXdzLmNvZ25pdG8uc2lnbmluLnVzZXIuYWRtaW4gcGhvbmUgb3BlbmlkIHByb2ZpbGUgZW1haWwiLCJhdXRoX3RpbWUiOjE2MzQwOTc1NDQsImlzcyI6Imh0dHBzOlwvXC9jb2duaXRvLWlkcC5hcC1zb3V0aC0xLmFtYXpvbmF3cy5jb21cL2FwLXNvdXRoLTFfaEVqYXo4Z3FzIiwiZXhwIjoxNjM0MTgzOTQ0LCJpYXQiOjE2MzQwOTc1NDQsInZlcnNpb24iOjIsImp0aSI6ImM3MjQ2YTdmLWYwYTAtNGMzNS04N2Y3LWMzNWVhNDNiMmU4MSIsImNsaWVudF9pZCI6IjM0c280amVyNDR1MnAzOWZmanFibG43Y3Z0IiwidXNlcm5hbWUiOiJwcmF2aW4ifQ.w9o8eRY75Fv0MtFCjeBMGa9RHiKed1p-bPxhvAn4DF38waxWRVZA3X5UgvrzkKsdxs4C43zgFOLVdwtwErS6Z5AqltWOkrg5fU2QYYmHhqGnbmPYSSkVmUA602H8cFptoZX5hpcSfIopCPn4O0Sbfh1BLGY5kzhfhIUvFbQSH3Iy6MPcsGlUwFTKSIyCvFXEYDZgSGsV1NmUCpsWRv3rkwuA1ioMr6sSimjbX3gCvZNEp8fur8b29GPmInwd2W5Y-aSyUTLSXRfMKL-YAY08UCiDg9OnxkU1l2SIsa830BhXQaTqN09nSyfGs5fuDps_NgYlwVmEeE7w_hfuwqFScA");

            var response = await client.PutAsync(myURI.AbsoluteUri, data);

        }
    }


}
