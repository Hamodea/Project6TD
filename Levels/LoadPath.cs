using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatmullRom;
namespace Project6TD.Levels
{
    public static class LoadPath
    {
        public static void LoadPathFromFile(CatmullRomPath path, string file)
        {
            string[] lines = File.ReadAllLines(file);
            foreach (string line in lines)
            {
                path.AddPoint(inputParser.parse_Vector2(line));
                
            }
        }
    }
}
