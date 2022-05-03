using SeaBattle.Application.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace SeaBattle.Application.DataAccess
{
    public static class DataStorage
    {
        public static Field Field { get; set; } = new Field();
        public static Field EnemyField { get; set; } = new Field();
    }
}