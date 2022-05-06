using SeaBattle.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace SeaBattle.Application.Core
{
    public static class AvaibilityValidation
    {
        public static bool CanPutShip(string[,] field, int verticalPoint, int horizontalPoint)
        {
            int y = 0;
            int c = -1;
            int z = -1;
            for (int i = 0; i < 8; i++)
            {
                if (field[verticalPoint + c, horizontalPoint + z] == "0")
                {
                    return false;
                }
                if (i == 2)
                {
                    y = c;
                    c = z;
                    z = y;
                    continue;
                }
                if (i > 4)
                {
                    c--;
                    if (i > 5)
                    {
                        z--;
                        y = c;
                        c = z;
                        z = y;
                    }
                }
                else
                {
                    z++;
                }
            }
            return true;
        }
        public static bool CanPutShip(string[,] field, int verticalPoint, int horizontalPoint, bool position)
        {
            int y = 0;
            int c = -1;
            int z = -1;
            for (int i = 0; i < 8; i++)
            {
                if (field[verticalPoint + c, horizontalPoint + z] == "0")
                {
                    if (position == true)
                    {
                        if (c != 0 | z != -1)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (c != -1 | z != 0)
                        {
                            return false;
                        }
                    }
                }
                if (i == 2)
                {
                    y = c;
                    c = z;
                    z = y;
                    continue;
                }
                if (i > 4)
                {
                    c--;
                    if (i > 5)
                    {
                        z--;
                        y = c;
                        c = z;
                        z = y;
                    }
                }
                else
                {
                    z++;
                }
            }
            return true;
        }
        public static bool IsGameEnded(Field field)
        {
            for (int i = 0; i < 14; i++)
            {
                for (int j = 0; j < 14; j++)
                {
                    if (field.MainField[i, j] == "0")
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public static string IsHittedShip(string[,] field)
        {
            string isHttedShip = string.Empty;
            bool isHittedOnField = false;
            for (int i = 0; i < 14; i++)
            {
                for (int j = 0; j < 14; j++)
                {
                    if (field[i, j] == "O")
                    {
                        isHittedOnField = Attack.KilledOrNotSimplified(field, j, i);
                        if (isHittedOnField == false)
                        {
                            isHttedShip = Ships.ConvertPointToString(j, i);
                            return isHttedShip;
                        }
                    }
                }
            }
            return null;
        }
    }
}
