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

            //Derived obj = new Derived();

            //string a = obj.GetToken();

            string CallBackUrl = "https://g9yh14f7ve.execute-api.ap-south-1.amazonaws.com/Authorizeddev/loanapproval/banker";
            string url = CallBackUrl + "/" + input.External_ID.ToString();
            var client = new HttpClient();

            Uri myURI = new Uri(url);
            //Need Banker Authorization access token
            //string AccessToken = _configuration.GetSection("BankerAccessToken").Value.ToString();
            client.DefaultRequestHeaders.Add("Authorization", "eyJraWQiOiJrSURWK2lTS0RvMU5mMGJHbjhSUlpiMnNIdE1jYVJmS2JZMDB6eWVXM29rPSIsImFsZyI6IlJTMjU2In0.eyJzdWIiOiI5ZWIzYzZlNy1lMjM5LTRlZTQtYWUxOC1mMjNiNmU0NzhkN2UiLCJ0b2tlbl91c2UiOiJhY2Nlc3MiLCJzY29wZSI6ImF3cy5jb2duaXRvLnNpZ25pbi51c2VyLmFkbWluIHBob25lIG9wZW5pZCBwcm9maWxlIGVtYWlsIiwiYXV0aF90aW1lIjoxNjMzNjgwMjQ5LCJpc3MiOiJodHRwczpcL1wvY29nbml0by1pZHAuYXAtc291dGgtMS5hbWF6b25hd3MuY29tXC9hcC1zb3V0aC0xX2hFamF6OGdxcyIsImV4cCI6MTYzMzc2NjY0OSwiaWF0IjoxNjMzNjgwMjQ5LCJ2ZXJzaW9uIjoyLCJqdGkiOiJiZDc0OGIwMC1jMjEzLTRhMWEtODY4MS04ZGM4ZDhkMzJhZTkiLCJjbGllbnRfaWQiOiIzNHNvNGplcjQ0dTJwMzlmZmpxYmxuN2N2dCIsInVzZXJuYW1lIjoicHJhdmluIn0.QYeynIjFbvGH0l-ydVRFbLOI2M3XP_EIOx9mWXKqKizxpffzSiZE9i0ciENKF2OgcIaJQjgTH_eNGdpwm3F76mIlFyedSLSAKMS6a7UNNWXbpkqOPp9ZcrYPmj2Y3NxqkwamFp9rWKu5sE2rdwP5pp_fQnRF3Cxwa4UGGppFqapqQiWkkZwDQSuKpASy9rGhRpQjCbcoblFKnbWwYi7nAmRlu7cDmR3GWpqApOAdX3j6hD3AlHm82lWJt78uvs8SiafCRh22mM3D9ASZs9IguKTuj5dr3cXi-amtjKrchNRkoLCY4LJNcD9upL_sKKqjMhU-WMmFXU-KQ1iQBktFbQ");

            var response = await client.PutAsync(myURI.AbsoluteUri, data);

        }
    }

    
}
