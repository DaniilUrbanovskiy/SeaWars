using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using SeaBattle.Infrastructure.Extentions;
using SeaBattle.Infrastructure.Domain;
using Newtonsoft.Json;

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
        public static async Task<string[,]> PutShip(int deckCount, string point, bool position)
        {
            ShipOtions ship = new ShipOtions();
            ship.DecksCount = deckCount;
            ship.Point = point;
            if (position == true)
            {
                ship.Direction = Direction.Horizontal;
            }
            else
            {
                ship.Direction = Direction.Vertical;
            }
            string json = JsonConvert.SerializeObject(ship);

            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://localhost:44373/field/owninit", httpContent);
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsStringAsync().Result.ToDoubleArray();
            }
            else
            {
                return null;
            }
            
        }

    }
}
