using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project6TD.System
{
    public class PlayerStats
    {
        public int MaxHP { get; private set; }
        public int CurrentHP { get; private set; }

        public PlayerStats(int hp)
        {
            MaxHP = hp;
            CurrentHP = hp;
        }

        public void TakeDamage(int amount)
        {
            CurrentHP -= amount;

            if (CurrentHP < 0)
                CurrentHP = 0;
        }

        public void Reset()
        {
            CurrentHP = MaxHP;
        }

        public bool IsDead => CurrentHP <= 0;
    }
}
