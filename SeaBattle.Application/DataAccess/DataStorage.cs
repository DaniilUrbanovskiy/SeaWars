using SeaBattle.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace SeaBattle.Application.DataAccess
{
    public static class DataStorage
    {
        public static Field Field { get; set; } = new Field();
        public static Field EnemyField { get; set; } = new Field();
        public static int GameId { get; set; }
        public static bool IsConnected { get; set; }

    }
}