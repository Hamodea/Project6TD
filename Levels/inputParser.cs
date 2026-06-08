using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Project6TD.Levels
{
    public static class inputParser
    {
        public static int parseInt(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                throw new FormatException($"Invalid integer token (empty or whitespace).");

            if (!int.TryParse(str.Trim(), out int x))
                throw new FormatException($"Invalid integer token: '{str}'.");

            return x;
        }

        public static int[] parse_ints(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return Array.Empty<int>();

            string[] strings = str.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            int[] ints = new int[strings.Length];

            for (int i = 0; i < strings.Length; i++)
            {
                ints[i] = parseInt(strings[i]);
            }
            return ints;
        }

        public static Vector2 parse_Vector2(string line)
        {
            Debug.WriteLine("Got " + line);
            int[] ints = parse_ints(line);

            if (ints.Length < 2)
                throw new ArgumentException($"Expected 2 integers (x,y) but got {ints.Length}. Line: '{line}'");

            Debug.WriteLine("Parsed " + ints[0].ToString() + ", " + ints[1].ToString());
            return new Vector2(ints[0], ints[1]);
        }
    }
}
