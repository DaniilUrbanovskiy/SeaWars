using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using SeaBattle.Infrastructure.Extentions;
using SeaBattle.Infrastructure.Domain;
using SeaBattle.Infrastructure.Common;
using Newtonsoft.Json;

namespace SeaBattle.ConsoleUi.Requests
{
    public static class RequestModel
    {
        private static readonly string basePath = "https://localhost:44373/";
        static HttpClient client = new HttpClient();

        public static async Task<string[,]> GetField(int whoseField)
        {
            var response = await client.GetAsync($"{basePath}field/"+whoseField);
            return response.Content.ReadAsStringAsync().Result.ToDoubleArray();
        }

        public static async Task<string[,]> GetInitField(int whoseField)
        {
            var response = await client.GetAsync($"{basePath}field/randinit/"+whoseField);
            return response.Content.ReadAsStringAsync().Result.ToDoubleArray();
        }

        public static async Task<string> GetGameStatus(int whoseField)
        {
            var response = await client.GetAsync($"{basePath}condition/getgamestatus/" + whoseField);
            return response.Content.ReadAsStringAsync().Result;
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

            var response = await client.PostAsync($"{basePath}field/owninit", httpContent);
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsStringAsync().Result.ToDoubleArray();
            }
            else
            {
                return null;
            }
            
        }

        public static async Task<string> GetShipStatus(string[,] field)
        {
            string json = JsonConvert.SerializeObject(field.ToJaggedArray());

            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"{basePath}condition/getshipstatus", httpContent);

            return response.Content.ReadAsStringAsync().Result;
        }

        public static async Task<AttackResponse> SetPoint(string[,] enemyFieldHiden, string startPoint, int movesCounter)
        {
            AttackOptions attackOptions = new AttackOptions();
            attackOptions.EnemyFieldHiden = enemyFieldHiden.ToJaggedArray();
            attackOptions.StartPoint = startPoint;
            attackOptions.MovesCounter = movesCounter;

            string json = JsonConvert.SerializeObject(attackOptions);

            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"{basePath}attack/setpoint", httpContent).Result.Content.ReadAsStringAsync();
            var responseBody = JsonConvert.DeserializeObject<AttackResponse>(response);
            return responseBody;
        }

        public static async Task<string> SmartAttack(string[,] field, string startPoint)
        {
            AttackOptions attackOptions = new AttackOptions();
            attackOptions.EnemyFieldHiden = field.ToJaggedArray();
            attackOptions.StartPoint = startPoint;

            string json = JsonConvert.SerializeObject(attackOptions);

            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"{basePath}attack/SmartAttack", httpContent).Result.Content.ReadAsStringAsync();
            return response;
        }

        


    }
}
