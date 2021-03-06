using System.Net.Http;
using System.Threading.Tasks;
using SeaBattle.Infrastructure.Extentions;
using SeaBattle.Infrastructure.Domain;
using SeaBattle.Infrastructure.Common;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace SeaBattle.ConsoleUi.Requests
{
    public static class RequestModel
    {
        private static readonly string basePath;
        private static HttpClient client = new HttpClient();

        static RequestModel()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("app.json", optional: false)
                .Build();

            basePath = config.GetSection("ConnectionStrings")["ApiPath"];
        }

        public static async Task<string[,]> GetField(int whoseField, int gameId)
        { 
            var response = await client.GetAsync($"{basePath}field/{whoseField}/{gameId}");
            return response.Content.ReadAsStringAsync().Result.ToDoubleArray();
        }

        public static async Task<string[,]> GetInitField(int whoseField, int gameId)
        {
            var response = await client.GetAsync($"{basePath}field/randinit/{whoseField}/{gameId}");
            return response.Content.ReadAsStringAsync().Result.ToDoubleArray();
        }

        public static async Task<string> GetGameStatus(int gameId)
        {
            var response = await client.GetAsync($"{basePath}status/getgamestatus/{gameId}");
            return response.Content.ReadAsStringAsync().Result;
        }

        public static async Task<string[,]> PutShip(int deckCount, string point, bool position, int whoseField, int gameId)
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

            var response = await client.PostAsync($"{basePath}field/owninit/{whoseField}/{gameId}", httpContent);
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

            var response = await client.PostAsync($"{basePath}status/getshipstatus", httpContent).Result.Content.ReadAsStringAsync();
            var responseBody = JsonConvert.DeserializeObject<string>(response);
            return responseBody;
        }

        public static async Task<AttackResponse> SetPoint(string[,] field, string startPoint, int movesCounter, int gameId)
        {
            AttackOptions attackOptions = new AttackOptions();
            attackOptions.Field = field.ToJaggedArray();
            attackOptions.StartPoint = startPoint;
            attackOptions.MovesCounter = movesCounter;

            string json = JsonConvert.SerializeObject(attackOptions);

            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"{basePath}attack/setpoint/" + gameId, httpContent).Result.Content.ReadAsStringAsync();
            var responseBody = JsonConvert.DeserializeObject<AttackResponse>(response);
            return responseBody;
        }

        public static async Task<string> SmartAttack(string[,] field, string startPoint)
        {
            AttackOptions attackOptions = new AttackOptions();
            attackOptions.Field = field.ToJaggedArray();
            attackOptions.StartPoint = startPoint;

            string json = JsonConvert.SerializeObject(attackOptions);

            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"{basePath}attack/SmartAttack", httpContent).Result.Content.ReadAsStringAsync();
            var responseBody = JsonConvert.DeserializeObject<string>(response);
            return responseBody;
        }

        public static async Task<string> GetRandomPoint()
        {
            var response = await client.GetAsync($"{basePath}game/getrandompoint").Result.Content.ReadAsStringAsync();
            var responseBody = JsonConvert.DeserializeObject<string>(response);
            return responseBody;
        }

        public static async Task<int> GetGameId()
        {
            var response = await client.GetAsync($"{basePath}game/getgameid").Result.Content.ReadAsStringAsync();
            return int.Parse(response);
        }

        public static async Task<string[,]> GetEnemyField(int whoseField, int gameId)
        {
            var response = await client.GetAsync($"{basePath}field/getenemyfield/{whoseField}/{gameId}").Result.Content.ReadAsStringAsync();
            return response.ToDoubleArray();
        }

        public static async Task SetReadyStatus(int whoseField, int gameId)
        {
            var response = await client.PostAsync($"{basePath}status/setreadystatus/{whoseField}/{gameId}", null).Result.Content.ReadAsStringAsync();
        }

        public static async Task<string> GetReadyStatus(int whoseField, int gameId)
        {
            var response = await client.GetAsync($"{basePath}status/getreadystatus/{whoseField}/{gameId}").Result.Content.ReadAsStringAsync();
            return response;
        }

        public static async Task<string> GetConnectionStatus(int gameId)
        {
            var response = await client.GetAsync($"{basePath}game/getconnectionstatus/" + gameId).Result.Content.ReadAsStringAsync();
            return response;
        }

        public static async Task<string> ConnectToGame(int enteredGameId)
        {
            string json = JsonConvert.SerializeObject(enteredGameId);

            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"{basePath}game/connecttogame", httpContent).Result.Content.ReadAsStringAsync();
            var responseBody = JsonConvert.DeserializeObject<string>(response);
            return responseBody;
        }

        public static async Task SetAttackCondition(int userChoice, int gameId)
        {
            var response = await client.PostAsync($"{basePath}attack/setattackcondition/{userChoice}/{gameId}", null).Result.Content.ReadAsStringAsync();
        }

        public static async Task<string> GetAttackCondition(int userChoice, int gameId)
        {
            var response = await client.GetAsync($"{basePath}attack/getattackcondition/{userChoice}/{gameId}").Result.Content.ReadAsStringAsync();
            return response;
        }

        public static async Task RemoveGameFromStorage(int gameId)
        {
            var response = await client.PostAsync($"{basePath}attack/removegamefromstorage/{gameId}", null).Result.Content.ReadAsStringAsync();
        }

        public static async Task SetLastAttackPoint(string startPoint,int gameId)
        {
            string json = JsonConvert.SerializeObject(startPoint);

            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"{basePath}attack/setlastattackpoint/{gameId}", httpContent).Result.Content.ReadAsStringAsync();
        }

        public static async Task<string> GetLastAttackPoint(int gameId)
        {
            var response = await client.GetAsync($"{basePath}attack/getlastattackpoint/{gameId}").Result.Content.ReadAsStringAsync();
            return response;
        }



    }
}
