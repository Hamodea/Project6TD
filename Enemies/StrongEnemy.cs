using CatmullRom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project6TD.Enemies
{
    public class StrongEnemy : Enemy 
    {
        public StrongEnemy(CatmullRomPath path, Animation animation): base (path, animation, speed: 0.05f)
        {
            MaxHealth = 160;
            Health = MaxHealth;
            Reward = 25;
            Scale = 0.60f;
        }
    }
}
