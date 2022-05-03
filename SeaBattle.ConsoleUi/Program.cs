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
