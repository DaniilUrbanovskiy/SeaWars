using SeaBattle.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace SeaBattle.Application.DataAccess
{
    public static class DataStorage
    {
        public static Dictionary<int, Game> Games { get; set; } = new Dictionary<int, Game>();      
    }
}