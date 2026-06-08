using Project6TD.Enemies;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Project6TD.Waves
{
    public class WaveManager
    {
        private List<Wave> waves = new();
        private int currentWaveIndex = -1;
        private int currentWaveNumber = 0;
        private int maxWaves = 2;

        private EnemyManager enemyManager;
        public int CurrentWaveNumber => currentWaveNumber;
        public int MaxWaves => maxWaves;
        //public int CurrentWaveNumber => currentWaveIndex + 1;

        public bool HasMoreWaves =>
            currentWaveIndex + 1 < waves.Count;

        public bool IsWaveRunning { get; private set; }

        public WaveManager(EnemyManager enemyManager)
        {
            this.enemyManager = enemyManager;

            
            waves.Add(new Wave(new List<EnemyType> { EnemyType.Normal, EnemyType.Normal, EnemyType.Strong, EnemyType.Normal, EnemyType.Normal }, 1000));   // Wave 1
            waves.Add(new Wave(new List<EnemyType>{EnemyType.Normal, EnemyType.Normal, EnemyType.Strong, EnemyType.Normal,EnemyType.Strong }, 900));   // Wave 2 (svårare)
        }

        //public void StartNextWave()
        //{
        //    if (!HasMoreWaves || IsWaveRunning)
        //        return;

        //    currentWaveIndex++;

        //    Wave wave = waves[currentWaveIndex];

        //    enemyManager.StartWave(
        //        wave.EnemyTypes,
        //        wave.SpawnIntervalMs
        //     );

        //    IsWaveRunning = true;
        //}
        public void StartNextWave()
        {
            if (IsWaveRunning)
                return;

            if (currentWaveNumber >= maxWaves)
                return;   
            currentWaveNumber++;

            List<EnemyType> enemyTypes = GenerateWave(currentWaveNumber);

            int spawnInterval = 1200 - currentWaveNumber * 100;

            enemyManager.StartWave(enemyTypes, spawnInterval);

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
            currentWaveNumber = 0;
            IsWaveRunning = false;
        }

        public void Update()
        {
            if (IsWaveRunning && enemyManager.IsWaveFinished)
            {
                IsWaveRunning = false;
            }
        }
        private List<EnemyType> GenerateWave(int waveNumber)
        {
            List<EnemyType> enemies = new();

            switch (waveNumber)
            {
                case 1:
                    enemies.AddRange(Enumerable.Repeat(EnemyType.Normal, 6));
                    break;

                case 2:
                    enemies.AddRange(Enumerable.Repeat(EnemyType.Normal, 8));
                    enemies.AddRange(Enumerable.Repeat(EnemyType.Strong, 2));
                    break;

                case 3:
                    enemies.AddRange(Enumerable.Repeat(EnemyType.Normal, 6));
                    enemies.AddRange(Enumerable.Repeat(EnemyType.Strong, 2));
                    break;

                case 4:
                    enemies.AddRange(Enumerable.Repeat(EnemyType.Normal, 8));
                    enemies.AddRange(Enumerable.Repeat(EnemyType.Strong, 3));
                    break;

                case 5:
                    enemies.AddRange(Enumerable.Repeat(EnemyType.Normal, 10));
                    enemies.AddRange(Enumerable.Repeat(EnemyType.Strong, 5));
                    break;
            }

            return enemies;
        }
    }
}
