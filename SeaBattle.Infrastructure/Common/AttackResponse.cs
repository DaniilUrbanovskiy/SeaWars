using System;
using System.Collections.Generic;
using System.Text;

namespace SeaBattle.Infrastructure.Common
{
    public class AttackResponse
    {
        public AttackStatus AttackStatus { get; set; }
        public string[][] EnemyFieldHiden { get; set; }
    }
}
