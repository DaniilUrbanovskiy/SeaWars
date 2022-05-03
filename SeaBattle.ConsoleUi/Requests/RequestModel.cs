using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using SeaBattle.Infrastructure.Extentions;

namespace SeaBattle.ConsoleUi.Requests
{
    public static class RequestModel
    {
        static HttpClient client = new HttpClient();
        public static async Task<string[,]> GetField()
        {
            var response = await client.GetAsync("https://localhost:44362/field");
            return response.Content.ReadAsStringAsync().Result.ToDoubleArray();
        }
    }
}
