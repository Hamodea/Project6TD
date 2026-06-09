using Project6TD.Enemies;
using System.Collections.Generic;
using System.Linq;

namespace Project6TD.Waves
{
    public class WaveManager
    {
        private readonly EnemyManager enemyManager;

        private int currentWaveNumber = 0;
        private int maxWaves = 4;

        public int CurrentWaveNumber => currentWaveNumber;
        public int MaxWaves => maxWaves;

        public bool IsWaveRunning { get; private set; }

        public bool HasMoreWaves =>
            currentWaveNumber < maxWaves;

        // delay mellan vågor
        private float waveDelay = 2f;
        private float delayTimer = 0f;

        public WaveManager(EnemyManager enemyManager)
        {
            this.enemyManager = enemyManager;
        }

        public void StartNextWave()
        {
            if (IsWaveRunning)
                return;

            if (!HasMoreWaves)
                return;

            currentWaveNumber++;

            List<EnemyType> enemyTypes =
                GenerateWave(currentWaveNumber);

            int spawnInterval =
                1200 - currentWaveNumber * 100;

            enemyManager.StartWave(enemyTypes, spawnInterval);

            IsWaveRunning = true;
        }

        public void Update(float delta)
        {
            // Om wave körs och alla fiender är klara
            if (IsWaveRunning && enemyManager.IsWaveFinished)
            {
                IsWaveRunning = false;
                delayTimer = waveDelay;  // starta paus
            }

            // Om wave inte körs och det finns fler waves
            if (!IsWaveRunning && HasMoreWaves)
            {
                delayTimer -= delta;

                if (delayTimer <= 0f)
                {
                    StartNextWave();
                }
            }
        }

        public void Reset()
        {
            currentWaveNumber = 0;
            IsWaveRunning = false;
        }

        private List<EnemyType> GenerateWave(int waveNumber)
        {
            List<EnemyType> enemies = new();

            switch (waveNumber)
            {
                case 1:
                    enemies.AddRange(
                        Enumerable.Repeat(EnemyType.Normal, 5));
                    break;

                case 2:
                    enemies.AddRange(
                        Enumerable.Repeat(EnemyType.Normal, 6));
                    enemies.AddRange(
                        Enumerable.Repeat(EnemyType.Strong, 2));
                    enemies.AddRange(
                        Enumerable.Repeat(EnemyType.Normal, 2));
                    break;

                case 3:
                    enemies.AddRange(
                        Enumerable.Repeat(EnemyType.Normal, 6));
                    enemies.AddRange(
                        Enumerable.Repeat(EnemyType.Strong, 3));
                    enemies.AddRange(
                         Enumerable.Repeat(EnemyType.Normal, 2));
                    break;
                case 4:
                    enemies.AddRange(
                        Enumerable.Repeat(EnemyType.Normal, 8));
                    enemies.AddRange(
                        Enumerable.Repeat(EnemyType.Strong, 4));
                    enemies.AddRange(
                         Enumerable.Repeat(EnemyType.Normal, 4));
                    break;
            }

            return enemies;
        }
    }
}