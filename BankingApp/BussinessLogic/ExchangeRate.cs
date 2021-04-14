using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BankingApp.BussinessLogic
{
    public class ExchangeRate:IExchangeRate
    {
        
        private readonly IConfiguration _config;
        
        public ExchangeRate(IConfiguration config)
        {
            _config = config;
        }
    /// <summary>
    /// Gets the exchange rate for given amoun and for currency
    /// </summary>
    /// <param name="CurrencyCode"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
        public async Task<double> GetExchangeRate(string BaseCurrencyCode, string ToCurrencyCode)
        {
            double rate = 0.00000;
            string baseUrl = _config.GetValue<string>("App:ExchangeRateBaseURL");
            string fullUrl = baseUrl + string.Format("{0}?base={1}&symbols={2}", DateTime.Now.ToString("yyyy-MM-dd"), BaseCurrencyCode,ToCurrencyCode);

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(fullUrl);
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    rate = (double) JObject.Parse(jsonString)["rates"][ToCurrencyCode];                    
                }
                else
                {
                    throw new Exception("Request failed:" + response.ReasonPhrase);
                }
            }
            return rate;
        }

    }
}
