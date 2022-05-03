using System;
using System.Net.Http;
using System.Threading.Tasks;
using SeaBattle.ConsoleUi.Requests;
using SeaBattle.Infrastructure.Extentions;

namespace SeaBattle.ConsoleUi
{
    class Program
    {       
        static void Main(string[] args)
        {
            Console.WriteLine("Press (Enter) to start game:");
            Console.ReadLine();
            Console.Clear();

            var field = RequestModel.GetField();
            ShowField(field.Result);

            Console.WriteLine("Choose option:\n1. Generate by yourself\n2. Generate by random");
            byte userChoice = byte.Parse(Console.ReadLine());
            if (userChoice == 1)
            {
                field = 
            }
            else
            {
                field = RequestModel.GetInitField(1);
            }
            Console.Clear();
            ShowField(field.Result);

            Console.WriteLine("Press (Enter) to show enemy's field:");
            Console.ReadLine();
            Console.Clear();



        }           
        public static void ShowField(string [,] field)
        {
            for (int i = 0; i < 14; i++)
            {
                for (int j = 0; j < 14; j++)
                {
                    if (field[i, j] == "0")
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(field[i, j] + " ");
                        Console.ForegroundColor = ConsoleColor.White;
                        continue;
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









//public static string[,] InicializeFieldByYourself(string[,] field, string startPoint, bool isHorizontal)
//{
//    int c = 1;
//    for (int i = 4; i > 0; i--)
//    {
//        for (int j = 0; j < c; j++)
//        {
//            bool conditionForRestart = false;
//            while (conditionForRestart == false)
//            {
//                Console.WriteLine($"Enter start point for {i}-deck ship(example: 1a)");
//                string startPoint = Console.ReadLine();
//                bool position = default;
//                if (i != 1)
//                {
//                    Console.WriteLine("Enter (true), if you want locate your ship horizontal, and (false), if vertical:");
//                    position = bool.Parse(Console.ReadLine());
//                }
//                conditionForRestart = Ships.CreateShip(startPoint, field, i, position);
//                Console.Clear();
//            }
//        }
//        c++;
//    }
//    return field;
//}