using Project6TD.Enemies;

using System.Collections.Generic;

namespace Project6TD.Waves
{
    public class WaveManager
    {
        private List<Wave> waves = new();
        private int currentWaveIndex = -1;

        private EnemyManager enemyManager;
        

        public int CurrentWaveNumber => currentWaveIndex + 1;

        public bool HasMoreWaves =>
            currentWaveIndex + 1 < waves.Count;

        public bool IsWaveRunning { get; private set; }

        public WaveManager(EnemyManager enemyManager)
        {
            this.enemyManager = enemyManager;

            
            waves.Add(new Wave(4, 1000, EnemyType.Strong));   // Wave 1
            waves.Add(new Wave(5, 500, EnemyType.Normal));   // Wave 2 (svårare)
        }

        public void StartNextWave()
        {
            if (!HasMoreWaves || IsWaveRunning)
                return;

            currentWaveIndex++;

            Wave wave = waves[currentWaveIndex];

            enemyManager.StartWave(
                wave.EnemyCount,
                wave.SpawnIntervalMs,
                wave.enemyType
            );

            IsWaveRunning = true;
        }

        public void StopWave()
        {
            // Public method to stop the wave
            IsWaveRunning = false;

            // Optionally reset enemy spawns / clear existing enemies
            enemyManager.Reset();
        }
        // Rest the wave
        public void Reset()
        {
            currentWaveIndex = -1;
            IsWaveRunning = false;
        }

        public void Update()
        {
            if (IsWaveRunning && enemyManager.IsWaveFinished)
            {
                IsWaveRunning = false;
            }
        }
    }
}
