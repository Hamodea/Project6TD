using Project6TD.Enemies;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Project6TD.Waves
{
    public class Wave
    {
        public int EnemyCount { get; }
        public int SpawnIntervalMs { get; }
        public EnemyType enemyType { get; }

        public Wave(int enemyCount, int spawnIntervalMs, EnemyType enemyType)
        {
            EnemyCount = enemyCount;
            SpawnIntervalMs = spawnIntervalMs;
            this.enemyType = enemyType;
        }
    }
}

