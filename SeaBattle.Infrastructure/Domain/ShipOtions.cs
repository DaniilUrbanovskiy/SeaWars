using System;
using System.Collections.Generic;
using System.Text;

namespace SeaBattle.Infrastructure.Domain
{
    public class ShipOtions
    {
        public int DecksCount { get; set; }
        public string Point { get; set; }
        public Direction Direction { get; set; }
    }
}
