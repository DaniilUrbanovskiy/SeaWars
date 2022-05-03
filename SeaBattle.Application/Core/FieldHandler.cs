using SeaBattle.Infrastructure.Extentions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SeaBattle.Application.Core
{
    public static class FieldHandler
    {     
        public static string[,] CreateField()
        {
            return new string[,]
            {
                {" ", " ", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", " ", " "},
                {" ", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-", " "},
                {"A", "|", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "|", " "},
                {"B", "|", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "|", " "},
                {"C", "|", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "|", " "},
                {"D", "|", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "|", " "},
                {"E", "|", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "|", " "},
                {"F", "|", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "|", " "},
                {"G", "|", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "|", " "},
                {"H", "|", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "|", " "},
                {"I", "|", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "|", " "},
                {"J", "|", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "|", " "},
                {" ", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-", " "},
                {" ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " "},
            };
        }
       
        public static string[,] InicializeFieldByRandom(string[,] field)
        {
            Random random = new Random();
            int c = 1;
            for (int i = 4; i > 0; i--)
            {
                for (int j = 0; j < c; j++)
                {
                    bool conditionForRestart = false;
                    while (conditionForRestart == false)
                    {
                        int num = random.Next(1, 11);
                        char let = new char().GetRandomLetter();
                        string startPoint = $"{Convert.ToString(num) + let}";
                        bool position = random.Next(1, 3) == 1 ? true : false;
                        conditionForRestart = Ships.CreateShip(startPoint, field, i, position);
                    }
                }
                c++;
            }
            return field;
        }

        public static bool InicializeFieldByYourself(string[,] field, string point,int decksCount, bool isHorizontal)
        {
            return Ships.CreateShip(point, field, decksCount, isHorizontal);
        }
    }
}
