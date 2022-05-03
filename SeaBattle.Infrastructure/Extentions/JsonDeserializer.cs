using System;
using System.Collections.Generic;
using System.Text;

namespace SeaBattle.Infrastructure.Extentions
{
    public static class JsonDeserializer
    {
        public static string[,] ToDoubleArray(this string json)
        {
            var jsonFieled = json.Replace("[", "").Replace("\"", "").Replace("]", "").Split(',');
            var fieldSize = (int)Math.Sqrt(jsonFieled.Length);
            var field = new string[fieldSize, fieldSize];
            var indexer = 0;
            for (int i = 0; i < fieldSize; i++)
            {
                for (int j = 0; j < fieldSize; j++)
                {
                    field[i, j] = jsonFieled[indexer];
                    indexer++;
                }
            }
            return field;
        }
    }
}
