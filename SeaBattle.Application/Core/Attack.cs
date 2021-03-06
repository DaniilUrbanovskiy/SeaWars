using SeaBattle.Infrastructure.Common;
using SeaBattle.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SeaBattle.Application.Core
{
    public class Attack
    {
        public static AttackStatus AttackTheShip(Field field, string pointToAtatck)
        {
            Point point = Ships.ConvertStringToPoint(pointToAtatck);
            int horizontalPoint = point.X;
            int verticalPoint = point.Y;

            if (field.MainField[verticalPoint, horizontalPoint] == "*")
            {
                return AttackStatus.Failed;
            }
            if (field.MainField[verticalPoint, horizontalPoint] == "O")
            {
                return AttackStatus.Failed;
            }
            if (field.MainField[verticalPoint, horizontalPoint] == "#")
            {
                field.MainField[verticalPoint, horizontalPoint] = "*";
            }
            if (field.MainField[verticalPoint, horizontalPoint] == "0")
            {
                field.MainField[verticalPoint, horizontalPoint] = "O";

                bool isHorizontal = false;
                int decksCounter = default;
                int firstShipDeck = default;
                bool isKilled = KilledOrNot(field, horizontalPoint, verticalPoint, ref isHorizontal, ref decksCounter, ref firstShipDeck);
                if (isKilled == true)
                {
                    ShipsFuneral(field, horizontalPoint, verticalPoint, isHorizontal, decksCounter, firstShipDeck);
                    return AttackStatus.Killed;
                }
                else
                {
                    return AttackStatus.Hitted;

                }
            }
            return AttackStatus.Missed;
        }
        public static bool KilledOrNot(Field field, int horizontalPoint, int verticalPoint, ref bool isHorizontal, ref int decksCounter, ref int firstShipDeck)
        {
            int countOfStepsInOneDirection = 0;
            int counter = 0;
            while (field.MainField[verticalPoint, horizontalPoint + counter] == "O" | field.MainField[verticalPoint, horizontalPoint + counter] == "0")
            {
                counter--;
                firstShipDeck = -counter;
            }
            while (field.MainField[verticalPoint, horizontalPoint + counter + 1] == "O" | field.MainField[verticalPoint, horizontalPoint + counter + 1] == "0")
            {
                if (field.MainField[verticalPoint, horizontalPoint + counter + 1] == "0")
                {
                    return false;
                }
                counter++;
                countOfStepsInOneDirection++;
                decksCounter++;
            }
            if (countOfStepsInOneDirection > 1)
            {
                isHorizontal = true;
            }
            counter = 0;
            while (field.MainField[verticalPoint + counter, horizontalPoint] == "O" | field.MainField[verticalPoint + counter, horizontalPoint] == "0")
            {
                counter--;
                if (isHorizontal == false)
                {
                    firstShipDeck = -counter;
                }
            }
            while (field.MainField[verticalPoint + counter + 1, horizontalPoint] == "O" | field.MainField[verticalPoint + counter + 1, horizontalPoint] == "0")
            {
                if (field.MainField[verticalPoint + counter + 1, horizontalPoint] == "0")
                {
                    return false;
                }
                counter++;
                decksCounter++;
            }

            decksCounter -= 1;
            return true;
        }
        private static void ShipsFuneral(Field field, int horizontalPoint, int verticalPoint, bool isHorizontal, int decksCounter, int firstShipDeck)
        {
            firstShipDeck = firstShipDeck - 1;

            int Y_Axis_ShipsCount = verticalPoint + 1;
            int X_Axis_ShipsCount = horizontalPoint + decksCounter - firstShipDeck;

            int FirstDeckIn_Y_Axis = verticalPoint - 1;
            int FirstDeckIn_X_Axis = horizontalPoint - 1 - firstShipDeck;

            if (isHorizontal is false)
            {
                FirstDeckIn_Y_Axis = verticalPoint - 1 - firstShipDeck;
                FirstDeckIn_X_Axis = horizontalPoint - 1;

                Y_Axis_ShipsCount = verticalPoint + decksCounter - firstShipDeck;
                X_Axis_ShipsCount = horizontalPoint + 1;
            }

            for (int N = FirstDeckIn_Y_Axis; N <= Y_Axis_ShipsCount; N++)
            {
                for (int M = FirstDeckIn_X_Axis; M <= X_Axis_ShipsCount; M++)
                {
                    if (N == 12 || N == 1) continue;
                    if (M == 12 || M == 1) continue;
                    if (field.MainField[N, M] == "O")
                    {
                        continue;
                    }
                    field.MainField[N, M] = "*";
                }
            }
        }
        public static string SmartAttack(string[,] field, string startPoint)
        {
            Point point = Ships.ConvertStringToPoint(startPoint);
            int horizontalPoint = point.X;
            int verticalPoint = point.Y;
            string pointToAttack = string.Empty;

            pointToAttack = CheckElementAroundPoint(field, verticalPoint, horizontalPoint, pointToAttack, "O", "O");
            if (pointToAttack != null)
            {
                pointToAttack = CheckElementAroundPoint(field, verticalPoint, horizontalPoint, pointToAttack, "#", "0");
            }
            if (pointToAttack == null)
            {
                pointToAttack = CreateAttackPointForHittedShips(field, verticalPoint, horizontalPoint, pointToAttack);
            }
            return pointToAttack;
        }
        public static string CheckElementAroundPoint(string[,] field, int verticalPoint, int horizontalPoint, string pointToAttack, string mark, string markTwo)
        {
            int y = default;
            int z = 0;
            int c = -1;
            for (int i = 0; i < 4; i++)
            {
                if (field[verticalPoint + c, horizontalPoint + z] == mark | field[verticalPoint + c, horizontalPoint + z] == markTwo)
                {
                    if (mark == "O")
                    {
                        return null;
                    }
                    pointToAttack = Ships.ConvertPointToString(horizontalPoint + z, verticalPoint + c);
                    return pointToAttack;
                }
                if (i == 0)
                {
                    y = z;
                    z = c;
                    c = y;
                }
                if (i == 1)
                {
                    c++;
                    z++;
                }
                if (i == 2)
                {
                    y = z;
                    z = c;
                    c = y;
                }
            }
            return pointToAttack;
        }
        public static string CreateAttackPointForHittedShips(string[,] field, int verticalPoint, int horizontalPoint, string pointToAttack)
        {
            int y = default;
            int z = 0;
            int c = -1;
            for (int i = 0; i < 4; i++)
            {
                if (field[verticalPoint + c, horizontalPoint + z] == "O")
                {
                    if (verticalPoint == verticalPoint + c)
                    {
                        if (field[verticalPoint + c, horizontalPoint + z + 1] == "#" | field[verticalPoint + c, horizontalPoint + z + 1] == "0")
                        {
                            pointToAttack = Ships.ConvertPointToString(horizontalPoint + z + 1, verticalPoint + c);
                            return pointToAttack;
                        }
                        if (field[verticalPoint + c, horizontalPoint + z + 1] == "O")
                        {
                            if (field[verticalPoint + c, horizontalPoint + z + 2] == "#" | field[verticalPoint + c, horizontalPoint + z + 2] == "0")
                            {
                                pointToAttack = Ships.ConvertPointToString(horizontalPoint + z + 2, verticalPoint + c);
                                return pointToAttack;
                            }
                            if (field[verticalPoint + c, horizontalPoint + z + 2] == "*")
                            {
                                pointToAttack = Ships.ConvertPointToString(horizontalPoint + z - 2, verticalPoint + c);
                                return pointToAttack;
                            }
                            if (field[verticalPoint + c, horizontalPoint + z + 2] == "|")
                            {
                                pointToAttack = Ships.ConvertPointToString(horizontalPoint + z - 2, verticalPoint + c);
                                return pointToAttack;
                            }
                        }
                        if (field[verticalPoint + c, horizontalPoint + z + 1] == "|")
                        {
                            pointToAttack = Ships.ConvertPointToString(horizontalPoint + z - 2, verticalPoint + c);
                            return pointToAttack;
                        }
                        if (field[verticalPoint + c, horizontalPoint + z + 1] == "*")
                        {
                            pointToAttack = Ships.ConvertPointToString(horizontalPoint + z - 2, verticalPoint + c);
                            return pointToAttack;
                        }
                    }
                    if (horizontalPoint == horizontalPoint + z)
                    {
                        if (field[verticalPoint + c + 1, horizontalPoint + z] == "#" | field[verticalPoint + c + 1, horizontalPoint + z] == "0")
                        {
                            pointToAttack = Ships.ConvertPointToString(horizontalPoint + z, verticalPoint + c + 1);
                            return pointToAttack;
                        }
                        if (field[verticalPoint + c + 1, horizontalPoint + z] == "O")
                        {
                            if (field[verticalPoint + c + 2, horizontalPoint + z] == "#" | field[verticalPoint + c + 2, horizontalPoint + z] == "0")
                            {
                                pointToAttack = Ships.ConvertPointToString(horizontalPoint + z, verticalPoint + c + 2);
                                return pointToAttack;
                            }
                            if (field[verticalPoint + c + 2, horizontalPoint + z] == "*")
                            {
                                pointToAttack = Ships.ConvertPointToString(horizontalPoint + z, verticalPoint + c - 2);
                                return pointToAttack;
                            }
                            if (field[verticalPoint + c + 2, horizontalPoint + z] == "-")
                            {
                                pointToAttack = Ships.ConvertPointToString(horizontalPoint + z, verticalPoint + c - 2);
                                return pointToAttack;
                            }
                        }
                        if (field[verticalPoint + c + 1, horizontalPoint + z] == "-")
                        {
                            pointToAttack = Ships.ConvertPointToString(horizontalPoint + z, verticalPoint + c - 2);
                            return pointToAttack;
                        }
                        if (field[verticalPoint + c + 1, horizontalPoint + z] == "*")
                        {
                            pointToAttack = Ships.ConvertPointToString(horizontalPoint + z, verticalPoint + c - 2);
                            return pointToAttack;
                        }
                    }
                }
                if (i == 0)
                {
                    y = z;
                    z = c;
                    c = y;
                }
                if (i == 1)
                {
                    c++;
                    z++;
                }
                if (i == 2)
                {
                    y = z;
                    z = c;
                    c = y;
                }
            }
            return pointToAttack;
        }
        public static bool KilledOrNotSimplified(string[,] field, int horizontalPoint, int verticalPoint)
        {
            int counter = 0;
            while (field[verticalPoint, horizontalPoint + counter] == "O" | field[verticalPoint, horizontalPoint + counter] == "0")
            {
                counter--;
            }
            while (field[verticalPoint, horizontalPoint + counter + 1] == "O" | field[verticalPoint, horizontalPoint + counter + 1] == "0")
            {
                if (field[verticalPoint, horizontalPoint + counter + 1] == "0")
                {
                    return false;
                }
                counter++;
            }
            counter = 0;
            while (field[verticalPoint + counter, horizontalPoint] == "O" | field[verticalPoint + counter, horizontalPoint] == "0")
            {
                counter--;
            }
            while (field[verticalPoint + counter + 1, horizontalPoint] == "O" | field[verticalPoint + counter + 1, horizontalPoint] == "0")
            {
                if (field[verticalPoint + counter + 1, horizontalPoint] == "0")
                {
                    return false;
                }
                counter++;
            }
            return true;
        }
    }
}
