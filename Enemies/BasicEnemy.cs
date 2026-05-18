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
            : base(path, animation, speed: 0.15f)
        {
            MaxHealth = 100;
            Health = MaxHealth;
            Reward = 10;
            Scale = 1f;
        }
    }
}
