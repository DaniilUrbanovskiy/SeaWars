using System;
using System.Collections.Generic;
using System.Text;

namespace SeaBattle.Infrastructure.Extentions
{
    public static class ExtensionsForChar
    {
        public static char GetRandomLetter(this char let)
        {
            Random random = new Random();
            int num = random.Next(97, 107);
            let = (char)(num);
            return let;
        }
    }
}
