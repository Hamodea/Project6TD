using CatmullRom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project6TD.Enemies
{
    public class BasicEnemy : Enemy
    {
        public BasicEnemy(CatmullRomPath path, Animation animation)
            : base(path, animation, speed: 0.07f)
        {
            MaxHealth = 80;
            Health = MaxHealth;
            Reward = 10;
            Scale = 0.90f;
        }
    }
}
