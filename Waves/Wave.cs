using Project6TD.Enemies;
using System.Collections.Generic;

namespace Project6TD.Waves
{
    public class Wave
    {
        public List<EnemyType> EnemyTypes { get; }
        public int SpawnIntervalMs { get; }

        public Wave(List<EnemyType> enemyTypes, int spawnIntervalMs)
        {
            EnemyTypes = enemyTypes;
            SpawnIntervalMs = spawnIntervalMs;
        }
    }
}