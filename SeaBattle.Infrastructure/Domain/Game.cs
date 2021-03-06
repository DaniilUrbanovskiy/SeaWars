using System;
using System.Collections.Generic;
using System.Text;

namespace SeaBattle.Infrastructure.Domain
{
    public class Game
    {
        public Field Field { get; set; } = new Field();
        public Field EnemyField { get; set; } = new Field();
        public int GameId { get; set; }
        public bool IsConnected { get; set; }
        public bool IsFirstUserAttackFinished { get; set; }
        public bool IsSecondUserAttackFinished { get; set; }
        public string LastAttackPoint { get; set; } = null;
    }
}
