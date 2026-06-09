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
        public StrongEnemy(CatmullRomPath path, Animation animation): base (path, animation, speed: 0.06f)
        {
            MaxHealth = 80;
            Health = MaxHealth;
            Reward = 20;
            Scale = 0.50f;
        }
    }
}
