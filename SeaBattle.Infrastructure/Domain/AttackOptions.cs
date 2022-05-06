using System;
using System.Collections.Generic;
using System.Text;

namespace SeaBattle.Infrastructure.Domain
{
    public class AttackOptions
    {
        public string[][] Field { get; set; }
        public string StartPoint { get; set; }
        public int MovesCounter { get; set; }
    }
}
