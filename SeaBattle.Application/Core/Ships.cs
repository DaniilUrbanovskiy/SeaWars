using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SeaBattle.Application.Core
{
    public static class Ships
    {
        public static bool CreateShip(string startPoint, string [,] field, int decks, bool position)
        {
            Point point = ConvertStringToPoint(startPoint);
            int horizontalPoint = point.X;
            int verticalPoint = point.Y;

            if (AvaibilityValidation.CanPutShip(field, verticalPoint, horizontalPoint) == true)
            {
                if (field[verticalPoint, horizontalPoint] == "#")
                {
                    field[verticalPoint, horizontalPoint] = "0";
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            for (int i = 1; i < decks; i++)
            {
                int c = 0;
                int z = 0;
                if (position == true)
                {
                    z = i;
                }
                if (position == false)
                {
                    c = i;
                }
                if (AvaibilityValidation.CanPutShip(field, verticalPoint + c, horizontalPoint + z, position) == true)
                {
                    if (field[verticalPoint + c, horizontalPoint + z] != "|" & field[verticalPoint + c, horizontalPoint + z] != "-")
                    {
                        field[verticalPoint + c, horizontalPoint + z] = "0";
                    }
                    else
                    {
                        DeleteShipBeforeRestart(field, position, horizontalPoint, verticalPoint, i, c, z);
                        return false;
                    }
                }
                else
                {
                    DeleteShipBeforeRestart(field, position, horizontalPoint, verticalPoint, i, c, z);
                    return false;
                }
            }
            return true;
        }
        private static void DeleteShipBeforeRestart(string[,] field, bool position, int horizontalPoint, int verticalPoint, int i, int c, int z)
        {
            for (int j = 1; j < i + 1; j++)
            {
                int m = 0;
                int n = 0;
                if (position == true)
                {
                    m = j;
                }
                else
                {
                    n = j;
                }
                field[verticalPoint + c - n, horizontalPoint + z - m] = "#";
            }
        }
        public static Point ConvertStringToPoint(string init)
        {
            int horizontalPoint = default;
            int verticalPoint = default;
            if (init.Substring(0, 2) != "10")
            {
                horizontalPoint = int.Parse(init.Substring(0, 1)) + 1;
                verticalPoint = LettersToNumbers(init.Substring(1, 1));
            }
            else
            {
                horizontalPoint = 11;
                verticalPoint = LettersToNumbers(init.Substring(1, 2));
            }
           
            return new Point(horizontalPoint, verticalPoint);
        }
        public static string ConvertPointToString(int horizontalPoint, int verticalPoint)
        {
            string point = string.Empty;
            string letter = NumbersToLetters(verticalPoint);
            point = $"{horizontalPoint - 1}" + letter;

            
            return point;
        }
        private static string NumbersToLetters(int verticalPoint)
        {
            string[] letters = new string[]
            {
            "a", "b", "c", "d", "e", "f", "g", "h", "i", "j"
            };
            for (int i = 0; i < letters.Length; i++)
            {
                if (i == verticalPoint - 2)
                {
                    return letters[i];
                }
            }
            return "404";
        }
        private static int LettersToNumbers(string a)
        {
            string[] letters = new string[]
            {
                "a", "b", "c", "d", "e", "f", "g", "h", "i", "j"
            };
            for (int i = 0; i < letters.Length; i++)
            {
                if (a.Contains(letters[i]))
                {
                    return i + 2;
                }
            }
            return 404;
        }
    }
}
