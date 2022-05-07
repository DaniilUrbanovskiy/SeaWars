using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using SeaBattle.ConsoleUi.Requests;
using SeaBattle.Infrastructure.Common;
using SeaBattle.Infrastructure.Extentions;

namespace SeaBattle.ConsoleUi
{
    class Program
    {       
        static async Task Main(string[] args)
        {
            Console.WriteLine("Press (Enter) to start game:");
            Console.ReadLine();
            Console.Clear();

            Console.WriteLine("Choose option:\n1. Play with computer\n2. Play with real player");
            byte botOrPlayer = byte.Parse(Console.ReadLine());
            Console.Clear();

            if (botOrPlayer == 1)
            {
                var field = await RequestModel.GetField(1);
                ShowField(field);

                Console.WriteLine("Choose option:\n1. Generate by yourself\n2. Generate by random");
                byte userChoice = byte.Parse(Console.ReadLine());
                if (userChoice == 1)
                {
                    field = await InicializeByYourself(field, 1);
                }
                else
                {
                    field = await RequestModel.GetInitField(1);
                }
                Console.Clear();
                ShowField(field);

                Console.WriteLine("Press (Enter) to show enemy's field:");
                Console.ReadLine();
                Console.Clear();

                var enemyField = await RequestModel.GetField(2);
                enemyField = await RequestModel.GetInitField(2);
                ShowField(enemyField, isEnemyField: true);

                Console.WriteLine("Enter point to attack enemy's ship:");

                int movesCounter = 0;
                bool isGameEnded = false;
                while (isGameEnded == false)
                {
                    movesCounter++;
                    AttackStatus attackStatus = 0;
                    if (movesCounter % 2 != 0)
                    {
                        string startPoint = string.Empty;
                        while (attackStatus != AttackStatus.Missed)
                        {
                            Console.Clear();
                            ShowField(enemyField, isEnemyField: true);
                            Console.WriteLine("Enter point to attack enemy's ship:");
                            startPoint = Console.ReadLine();
                            AttackResponse attackResponse = RequestModel.SetPoint(enemyField, startPoint, 1).Result;
                            attackStatus = attackResponse.AttackStatus;
                            enemyField = attackResponse.Field.ToDoubleDimension();
                            isGameEnded = bool.Parse(await RequestModel.GetGameStatus(2));
                            Console.Clear();
                            if (isGameEnded == true)
                            {
                                break;
                            }
                        }
                        if (isGameEnded == true)
                        {
                            break;
                        }
                        ShowField(enemyField, isEnemyField: true);
                        Console.WriteLine($"You attacked ({startPoint})\n");
                        Console.WriteLine("Press (Enter) to give enemy his move:");
                        Console.ReadLine();
                    }
                    else
                    {
                        string startPoint = string.Empty;
                        string nextPointToAttack = string.Empty;
                        while (attackStatus != AttackStatus.Missed)
                        {
                            if (attackStatus != AttackStatus.Failed)
                            {
                                Console.Clear();
                                ShowField(field);
                            }
                            if (attackStatus != AttackStatus.Failed)
                            {
                                Thread.Sleep(1000);
                            }
                            if (attackStatus != AttackStatus.Hitted)
                            {
                                string hittedShip = await RequestModel.GetShipStatus(field);
                                if (hittedShip != null)
                                {
                                    startPoint = await RequestModel.SmartAttack(field, hittedShip);
                                }
                                if (hittedShip == null)
                                {
                                    startPoint = await RequestModel.GetRandomPoint();
                                }
                            }
                            if (attackStatus == AttackStatus.Hitted)
                            {
                                startPoint = await RequestModel.SmartAttack(field, nextPointToAttack);
                            }
                            AttackResponse attackResponse = RequestModel.SetPoint(field, startPoint, 2).Result;
                            attackStatus = attackResponse.AttackStatus;
                            field = attackResponse.Field.ToDoubleDimension();
                            if (attackStatus == AttackStatus.Hitted)
                            {
                                nextPointToAttack = startPoint;
                            }
                            isGameEnded = isGameEnded = bool.Parse(await RequestModel.GetGameStatus(1));
                            Console.Clear();
                            if (isGameEnded == true)
                            {
                                break;
                            }
                        }
                        if (isGameEnded == true)
                        {
                            break;
                        }
                        ShowField(field);
                        Console.WriteLine($"Enemy attacked ({startPoint})\n");
                        Console.WriteLine("Press (Enter) to attack the enemy:");
                        Console.ReadLine();
                    }
                }
                if (movesCounter % 2 != 0)
                {
                    Console.Clear();
                    ShowField(enemyField, isEnemyField: true);
                    Console.WriteLine("You win!");
                    Console.ReadLine();
                }
                else
                {
                    Console.Clear();
                    ShowField(field);
                    Console.WriteLine("You lose!");
                    Console.ReadLine();
                }
            }
            else
            {
                Console.WriteLine("Choose option:\n1.Create game\n2.Connect to game");
                int userChoice = int.Parse(Console.ReadLine());
                int userChoiceDec = default;
                if (userChoice == 1)
                {
                    userChoiceDec = 2;
                }
                else
                {
                    userChoiceDec = 1;
                }  
                Console.Clear();

                if (userChoice == 1)
                {
                    int gameId = await RequestModel.GetGameId();
                    Console.WriteLine($"Your game ID: {gameId}\nWaiting second player connection...");
                    bool isConnected = false;
                    while (isConnected == false)
                    {
                        Thread.Sleep(500);
                        isConnected = bool.Parse(await RequestModel.GetConnectionStatus());
                    }
                    Console.WriteLine("Connected!");
                }
                else
                {
                    string message = "Try again!";
                    while (message == "Try again!")
                    {
                        Console.Clear();
                        Console.WriteLine("Enter game ID:");
                        int gameId = int.Parse(Console.ReadLine());
                        message = await RequestModel.ConnectToGame(gameId);
                        Console.WriteLine(message);
                        Thread.Sleep(1000);
                    }
                }
                Console.WriteLine("Press Enter to start!");
                Console.Clear();
                var field = await RequestModel.GetField(userChoice);
                ShowField(field);

                Console.WriteLine("Choose option:\n1. Generate by yourself\n2. Generate by random");
                byte userChoiceHowToGenerate = byte.Parse(Console.ReadLine());
                if (userChoiceHowToGenerate == 1)
                {
                    field = await InicializeByYourself(field, userChoice);
                }
                else
                {
                    field = await RequestModel.GetInitField(userChoice);
                }
                Console.Clear();
                ShowField(field);

                Console.WriteLine("Press Enter if you are ready!");
                Console.ReadLine();
                await RequestModel.SetReadyStatus(userChoice);
                bool isEnemyReady = false;
                while (isEnemyReady == false)
                {
                    Console.Clear();
                    Console.WriteLine("Wait for your enemy...");
                    isEnemyReady = bool.Parse(await RequestModel.GetReadyStatus(userChoiceDec));
                    Thread.Sleep(500);
                }
                Console.Clear();
                var enemyField = await RequestModel.GetEnemyField(userChoiceDec);
                ShowField(enemyField, isEnemyField: true);
                Console.ReadLine();
            }             
        }
        private static async Task<string[,]> InicializeByYourself(string[,] field, int whosField)
        {
            string[,] lastAttempt = field;
            int c = 1;
            for (int i = 4; i > 0; i--)
            {
                for (int j = 0; j < c; j++)
                {
                    while (true)
                    {
                        Console.WriteLine($"Enter start point for {i}-deck ship(example: 1a)");
                        string startPoint = Console.ReadLine();
                        bool position = false;
                        if (i != 1)
                        {
                            Console.WriteLine("Enter (true), if you want locate your ship horizontal, and (false), if vertical:");
                            position = bool.Parse(Console.ReadLine());
                        }
                        field = await RequestModel.PutShip(i, startPoint, position, whosField);
                        if (field != null)
                        {
                            break;
                        }
                        Console.Clear();
                        ShowField(lastAttempt);
                    }
                    Console.Clear();
                    ShowField(field);
                    lastAttempt = field;
                }
                c++;
            }
            return field;
        }
        public static void ShowField(string [,] field, bool isEnemyField = false)
         {
            for (int i = 0; i < 14; i++)
            {
                for (int j = 0; j < 14; j++)
                {
                    if (isEnemyField == false)
                    {
                        if (field[i, j] == "0")
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write(field[i, j] + " ");
                            Console.ForegroundColor = ConsoleColor.White;
                            continue;
                        }
                    }
                    else
                    {
                        if (field[i, j] == "0")
                        {
                            Console.Write("#" + " ");                            
                            continue;
                        }
                    }
                    if (field[i, j] == "*")
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(field[i, j] + " ");
                        Console.ForegroundColor = ConsoleColor.White;
                        continue;
                    }
                    if (field[i, j] == "O")
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(field[i, j] + " ");
                        Console.ForegroundColor = ConsoleColor.White;
                        continue;
                    }
                    if (field[i, j] == "-")
                        Console.Write(field[i, j] + "-");
                    else
                        Console.Write(field[i, j] + " ");
                }
                Console.WriteLine();
            }
        }      
    }
}








