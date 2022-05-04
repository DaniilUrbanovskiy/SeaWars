using SeaBattle.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SeaBattle.Application.Core
{
    public class Attack
    {
        //public AttackStatus AttackTheShip(Ships ship, FieldHandler enemysField, FieldHandler enemysFieldHiden, string pointToAtatck, int movesCounter)
        //{
        //    Point point = ship.ConvertStringToPoint(pointToAtatck);
        //    int horizontalPoint = point.X;
        //    int verticalPoint = point.Y;

        //    if (enemysField.MainField[verticalPoint, horizontalPoint] == "*")
        //    {
        //        return AttackStatus.Failed;
        //    }
        //    if (enemysField.MainField[verticalPoint, horizontalPoint] == "O")
        //    {
        //        return AttackStatus.Failed;
        //    }
        //    if (enemysField.MainField[verticalPoint, horizontalPoint] == "#")
        //    {
        //        enemysField.MainField[verticalPoint, horizontalPoint] = "*";
        //        if (movesCounter % 2 != 0)
        //        {
        //            enemysFieldHiden.MainField[verticalPoint, horizontalPoint] = "*";
        //        }
        //    }
        //    if (enemysField.MainField[verticalPoint, horizontalPoint] == "0")
        //    {
        //        enemysField.MainField[verticalPoint, horizontalPoint] = "O";
        //        if (movesCounter % 2 != 0)
        //        {
        //            enemysFieldHiden.MainField[verticalPoint, horizontalPoint] = "O";
        //        }
        //        bool isHorizontal = false;
        //        int decksCounter = default;
        //        int firstShipDeck = default;
        //        bool isKilled = KilledOrNot(enemysField, horizontalPoint, verticalPoint, ref isHorizontal, ref decksCounter, ref firstShipDeck);
        //        if (isKilled == true)
        //        {
        //            ShipsFuneral(enemysField, enemysFieldHiden, movesCounter, horizontalPoint, verticalPoint, isHorizontal, decksCounter, firstShipDeck);
        //            return AttackStatus.Killed;
        //        }
        //        else
        //        {
        //            return AttackStatus.Hitted;

        //        }
        //    }
        //    return AttackStatus.Missed;
        //}
        //public bool KilledOrNot(FieldHandler enemysField, int horizontalPoint, int verticalPoint, ref bool isHorizontal, ref int decksCounter, ref int firstShipDeck)
        //{
        //    int countOfStepsInOneDirection = 0;
        //    int counter = 0;
        //    while (enemysField.MainField[verticalPoint, horizontalPoint + counter] == "O" | enemysField.MainField[verticalPoint, horizontalPoint + counter] == "0")
        //    {
        //        counter--;
        //        firstShipDeck = -counter;
        //    }
        //    while (enemysField.MainField[verticalPoint, horizontalPoint + counter + 1] == "O" | enemysField.MainField[verticalPoint, horizontalPoint + counter + 1] == "0")
        //    {
        //        if (enemysField.MainField[verticalPoint, horizontalPoint + counter + 1] == "0")
        //        {
        //            return false;
        //        }
        //        counter++;
        //        countOfStepsInOneDirection++;
        //        decksCounter++;
        //    }
        //    if (countOfStepsInOneDirection > 1)
        //    {
        //        isHorizontal = true;
        //    }
        //    counter = 0;
        //    while (enemysField.MainField[verticalPoint + counter, horizontalPoint] == "O" | enemysField.MainField[verticalPoint + counter, horizontalPoint] == "0")
        //    {
        //        counter--;
        //        if (isHorizontal == false)
        //        {
        //            firstShipDeck = -counter;
        //        }
        //    }
        //    while (enemysField.MainField[verticalPoint + counter + 1, horizontalPoint] == "O" | enemysField.MainField[verticalPoint + counter + 1, horizontalPoint] == "0")
        //    {
        //        if (enemysField.MainField[verticalPoint + counter + 1, horizontalPoint] == "0")
        //        {
        //            return false;
        //        }
        //        counter++;
        //        decksCounter++;
        //    }

        //    decksCounter -= 1;
        //    return true;
        //}
        //private void ShipsFuneral(FieldHandler enemysField, FieldHandler enemysFieldHiden, int movesCounter, int horizontalPoint, int verticalPoint, bool isHorizontal, int decksCounter, int firstShipDeck)
        //{
        //    firstShipDeck = firstShipDeck - 1;

        //    int Y_Axis_ShipsCount = verticalPoint + 1;
        //    int X_Axis_ShipsCount = horizontalPoint + decksCounter - firstShipDeck;

        //    int FirstDeckIn_Y_Axis = verticalPoint - 1;
        //    int FirstDeckIn_X_Axis = horizontalPoint - 1 - firstShipDeck;

        //    if (isHorizontal is false)
        //    {
        //        FirstDeckIn_Y_Axis = verticalPoint - 1 - firstShipDeck;
        //        FirstDeckIn_X_Axis = horizontalPoint - 1;

        //        Y_Axis_ShipsCount = verticalPoint + decksCounter - firstShipDeck;
        //        X_Axis_ShipsCount = horizontalPoint + 1;
        //    }

        //    for (int N = FirstDeckIn_Y_Axis; N <= Y_Axis_ShipsCount; N++)
        //    {
        //        for (int M = FirstDeckIn_X_Axis; M <= X_Axis_ShipsCount; M++)
        //        {
        //            if (N == 12 || N == 1) continue;
        //            if (M == 12 || M == 1) continue;
        //            if (movesCounter % 2 != 0)
        //            {
        //                if (enemysFieldHiden.MainField[N, M] == "O")
        //                {
        //                    continue;
        //                }
        //                enemysFieldHiden.MainField[N, M] = "*";
        //            }
        //            if (enemysField.MainField[N, M] == "O")
        //            {
        //                continue;
        //            }
        //            enemysField.MainField[N, M] = "*";
        //        }
        //    }
        //}
        //public string SmartAttack(Ships ship, FieldHandler field, string startPoint)
        //{
        //    Point point = ship.ConvertStringToPoint(startPoint);
        //    int horizontalPoint = point.X;
        //    int verticalPoint = point.Y;
        //    string pointToAttack = string.Empty;

        //    pointToAttack = CheckElementAroundPoint(field, ship, verticalPoint, horizontalPoint, pointToAttack, "O", "O");
        //    if (pointToAttack != null)
        //    {
        //        pointToAttack = CheckElementAroundPoint(field, ship, verticalPoint, horizontalPoint, pointToAttack, "#", "0");
        //    }
        //    if (pointToAttack == null)
        //    {
        //        pointToAttack = CreateAttackPointForHittedShips(field, ship, verticalPoint, horizontalPoint, pointToAttack);
        //    }
        //    return pointToAttack;
        //}
        //public string CheckElementAroundPoint(FieldHandler field, Ships ship, int verticalPoint, int horizontalPoint, string pointToAttack, string mark, string markTwo)
        //{
        //    int y = default;
        //    int z = 0;
        //    int c = -1;
        //    for (int i = 0; i < 4; i++)
        //    {
        //        if (field.MainField[verticalPoint + c, horizontalPoint + z] == mark | field.MainField[verticalPoint + c, horizontalPoint + z] == markTwo)
        //        {
        //            if (mark == "O")
        //            {
        //                return null;
        //            }
        //            pointToAttack = ship.ConvertPointToString(horizontalPoint + z, verticalPoint + c);
        //            return pointToAttack;
        //        }
        //        if (i == 0)
        //        {
        //            y = z;
        //            z = c;
        //            c = y;
        //        }
        //        if (i == 1)
        //        {
        //            c++;
        //            z++;
        //        }
        //        if (i == 2)
        //        {
        //            y = z;
        //            z = c;
        //            c = y;
        //        }
        //    }
        //    return pointToAttack;
        //}
        //public string CreateAttackPointForHittedShips(FieldHandler field, Ships ship, int verticalPoint, int horizontalPoint, string pointToAttack)
        //{
        //    int y = default;
        //    int z = 0;
        //    int c = -1;
        //    for (int i = 0; i < 4; i++)
        //    {
        //        if (field.MainField[verticalPoint + c, horizontalPoint + z] == "O")
        //        {
        //            if (verticalPoint == verticalPoint + c)
        //            {
        //                if (field.MainField[verticalPoint + c, horizontalPoint + z + 1] == "#" | field.MainField[verticalPoint + c, horizontalPoint + z + 1] == "0")
        //                {
        //                    pointToAttack = ship.ConvertPointToString(horizontalPoint + z + 1, verticalPoint + c);
        //                    return pointToAttack;
        //                }
        //                if (field.MainField[verticalPoint + c, horizontalPoint + z + 1] == "O")
        //                {
        //                    if (field.MainField[verticalPoint + c, horizontalPoint + z + 2] == "#" | field.MainField[verticalPoint + c, horizontalPoint + z + 2] == "0")
        //                    {
        //                        pointToAttack = ship.ConvertPointToString(horizontalPoint + z + 2, verticalPoint + c);
        //                        return pointToAttack;
        //                    }
        //                    if (field.MainField[verticalPoint + c, horizontalPoint + z + 2] == "*")
        //                    {
        //                        pointToAttack = ship.ConvertPointToString(horizontalPoint + z - 2, verticalPoint + c);
        //                        return pointToAttack;
        //                    }
        //                    if (field.MainField[verticalPoint + c, horizontalPoint + z + 2] == "|")
        //                    {
        //                        pointToAttack = ship.ConvertPointToString(horizontalPoint + z - 2, verticalPoint + c);
        //                        return pointToAttack;
        //                    }
        //                }
        //                if (field.MainField[verticalPoint + c, horizontalPoint + z + 1] == "|")
        //                {
        //                    pointToAttack = ship.ConvertPointToString(horizontalPoint + z - 2, verticalPoint + c);
        //                    return pointToAttack;
        //                }
        //                if (field.MainField[verticalPoint + c, horizontalPoint + z + 1] == "*")
        //                {
        //                    pointToAttack = ship.ConvertPointToString(horizontalPoint + z - 2, verticalPoint + c);
        //                    return pointToAttack;
        //                }
        //            }
        //            if (horizontalPoint == horizontalPoint + z)
        //            {
        //                if (field.MainField[verticalPoint + c + 1, horizontalPoint + z] == "#" | field.MainField[verticalPoint + c + 1, horizontalPoint + z] == "0")
        //                {
        //                    pointToAttack = ship.ConvertPointToString(horizontalPoint + z, verticalPoint + c + 1);
        //                    return pointToAttack;
        //                }
        //                if (field.MainField[verticalPoint + c + 1, horizontalPoint + z] == "O")
        //                {
        //                    if (field.MainField[verticalPoint + c + 2, horizontalPoint + z] == "#" | field.MainField[verticalPoint + c + 2, horizontalPoint + z] == "0")
        //                    {
        //                        pointToAttack = ship.ConvertPointToString(horizontalPoint + z, verticalPoint + c + 2);
        //                        return pointToAttack;
        //                    }
        //                    if (field.MainField[verticalPoint + c + 2, horizontalPoint + z] == "*")
        //                    {
        //                        pointToAttack = ship.ConvertPointToString(horizontalPoint + z, verticalPoint + c - 2);
        //                        return pointToAttack;
        //                    }
        //                    if (field.MainField[verticalPoint + c + 2, horizontalPoint + z] == "-")
        //                    {
        //                        pointToAttack = ship.ConvertPointToString(horizontalPoint + z, verticalPoint + c - 2);
        //                        return pointToAttack;
        //                    }
        //                }
        //                if (field.MainField[verticalPoint + c + 1, horizontalPoint + z] == "-")
        //                {
        //                    pointToAttack = ship.ConvertPointToString(horizontalPoint + z, verticalPoint + c - 2);
        //                    return pointToAttack;
        //                }
        //                if (field.MainField[verticalPoint + c + 1, horizontalPoint + z] == "*")
        //                {
        //                    pointToAttack = ship.ConvertPointToString(horizontalPoint + z, verticalPoint + c - 2);
        //                    return pointToAttack;
        //                }
        //            }
        //        }
        //        if (i == 0)
        //        {
        //            y = z;
        //            z = c;
        //            c = y;
        //        }
        //        if (i == 1)
        //        {
        //            c++;
        //            z++;
        //        }
        //        if (i == 2)
        //        {
        //            y = z;
        //            z = c;
        //            c = y;
        //        }
        //    }
        //    return pointToAttack;
        //}
        //public bool KilledOrNotSimplified(FieldHandler enemysField, int horizontalPoint, int verticalPoint)
        //{
        //    int counter = 0;
        //    while (enemysField.MainField[verticalPoint, horizontalPoint + counter] == "O" | enemysField.MainField[verticalPoint, horizontalPoint + counter] == "0")
        //    {
        //        counter--;
        //    }
        //    while (enemysField.MainField[verticalPoint, horizontalPoint + counter + 1] == "O" | enemysField.MainField[verticalPoint, horizontalPoint + counter + 1] == "0")
        //    {
        //        if (enemysField.MainField[verticalPoint, horizontalPoint + counter + 1] == "0")
        //        {
        //            return false;
        //        }
        //        counter++;
        //    }
        //    counter = 0;
        //    while (enemysField.MainField[verticalPoint + counter, horizontalPoint] == "O" | enemysField.MainField[verticalPoint + counter, horizontalPoint] == "0")
        //    {
        //        counter--;
        //    }
        //    while (enemysField.MainField[verticalPoint + counter + 1, horizontalPoint] == "O" | enemysField.MainField[verticalPoint + counter + 1, horizontalPoint] == "0")
        //    {
        //        if (enemysField.MainField[verticalPoint + counter + 1, horizontalPoint] == "0")
        //        {
        //            return false;
        //        }
        //        counter++;
        //    }
        //    return true;
        //}
    }
}
