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
            var response = await client.GetAsync("https://localhost:44373/field");
            return response.Content.ReadAsStringAsync().Result.ToDoubleArray();
        }
        public static async Task<string[,]> GetInitField(int whooseField)
        {
            var response = await client.GetAsync("https://localhost:44373/field/randinit/"+whooseField);
            return response.Content.ReadAsStringAsync().Result.ToDoubleArray();
        }
        public static async Task<string[,]> PutShip()
        {
            var response = await client.PostAsync("https://localhost:44373/field/owninit");
            return response.Content.ReadAsStringAsync().Result.ToDoubleArray();
        }

    }
}
