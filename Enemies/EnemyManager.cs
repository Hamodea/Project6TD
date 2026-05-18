using CatmullRom;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project6TD;
using Project6TD.Enemies;
using Project6TD.Systems;
using System;
using System.Collections.Generic;

public class EnemyManager
{
    List<Enemy> enemies = new();

    int timeSinceLastEnemy = 0;
    int milliSecBetweenCreation = 800;
    int enemiesLeftInWave = 0;

    CatmullRomPath path;
    Animation enemyAnimation;
    EnemyType currentEnemyType;

    // Track whether a wave was actually started
    private bool waveStarted = false;

    private bool enemyEscaped = false;
    private readonly EconomySystem economySystem;

    public EnemyManager(CatmullRomPath path, Animation enemyAnimation, EconomySystem economySystem)
    {
        this.path = path;
        this.enemyAnimation = enemyAnimation;
        this.economySystem = economySystem;
    }

    public void StartWave(int enemyCount, int spawnIntervalMs, EnemyType enemyType)
    {
        enemiesLeftInWave = enemyCount;
        milliSecBetweenCreation = spawnIntervalMs;
        timeSinceLastEnemy = 0;
        currentEnemyType = enemyType;
        waveStarted = true;
        enemyEscaped = false;
        System.Diagnostics.Debug.WriteLine($"EnemyManager: StartWave count={enemyCount}, type={enemyType}");
        Console.WriteLine($"EnemyManager: StartWave count={enemyCount}, type={enemyType}");
    }

    public void Update(GameTime gameTime)
    {
        SpawnEnemies(gameTime);

        for (int i = enemies.Count - 1; i >= 0; i--)
        {
            enemies[i].Update(gameTime);

            if (!enemies[i].IsActive)
            {
                
                
                if (enemies[i].Health <= 0)
                {
                    System.Diagnostics.Debug.WriteLine($"EnemyManager: Enemy died, reward={enemies[i].Reward}");
                    Console.WriteLine($"EnemyManager: Enemy died, reward={enemies[i].Reward}");
                    
                    if (economySystem == null)
                    {
                        System.Diagnostics.Debug.WriteLine("EnemyManager: economySystem is NULL, cannot award reward");
                        Console.WriteLine("EnemyManager: economySystem is NULL, cannot award reward");
                    }
                    else
                    {
                        economySystem.Earn(enemies[i].Reward);
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"EnemyManager: Enemy removed (escaped). Health={enemies[i].Health}");
                    Console.WriteLine($"EnemyManager: Enemy removed (escaped). Health={enemies[i].Health}");
                    enemyEscaped = true;
                }
                AssetsManager.enemyDamage.Play();
                enemies.RemoveAt(i);
            }
        }
    }

    private void SpawnEnemies(GameTime gameTime)
    {
        if (enemiesLeftInWave <= 0)
            return;

        timeSinceLastEnemy += gameTime.ElapsedGameTime.Milliseconds;

        if (timeSinceLastEnemy >= milliSecBetweenCreation)
        {
            timeSinceLastEnemy -= milliSecBetweenCreation;

            Enemy enemy;

            if (currentEnemyType == EnemyType.Strong)
            {
                enemy = new StrongEnemy(
                    path,
                    AssetsManager.Enemy2Walk.Clone()
                );
            }
            else
            {
                //  BasicEnemy 
                enemy = new BasicEnemy(
                    path,
                    enemyAnimation.Clone()
                );
            }

            enemies.Add(enemy);
            enemiesLeftInWave--;
            System.Diagnostics.Debug.WriteLine($"EnemyManager: Spawned enemy, remaining={enemiesLeftInWave}");
            Console.WriteLine($"EnemyManager: Spawned enemy, remaining={enemiesLeftInWave}");
        }
    }

    public bool IsWaveFinished =>
        waveStarted && enemiesLeftInWave <= 0 && enemies.Count == 0;

    public bool HasEnemyEscaped => enemyEscaped;

    public Enemy GetClosestEnemy(Vector2 position)
    {
        Enemy closest = null;
        float shortestDistance = float.MaxValue;

        foreach (var enemy in enemies)
        {
            float dist = Vector2.Distance(position, enemy.Position);

            if (dist < shortestDistance)
            {
                shortestDistance = dist;
                closest = enemy;
            }
        }

        return closest;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (Enemy enemy in enemies)
        {
            enemy.Draw(spriteBatch);
            DrawHealthBar(spriteBatch, enemy);
        }
    }

    private void DrawHealthBar(SpriteBatch spriteBatch, Enemy enemy)
    {
        float hpPercent = enemy.Health / enemy.MaxHealth;

        int width = 30;
        int height = 5;

        Vector2 pos = enemy.Position + new Vector2(-width / 2, -80);

        spriteBatch.Draw(AssetsManager.pixel,
            new Rectangle((int)pos.X, (int)pos.Y, width, height),
            Color.DarkRed);

        spriteBatch.Draw(AssetsManager.pixel,
            new Rectangle((int)pos.X, (int)pos.Y,
                          (int)(width * hpPercent), height),
            Color.LimeGreen);
    }

    public void Reset()
    {
        enemies.Clear();
        timeSinceLastEnemy = 0;
        enemiesLeftInWave = 0;
        waveStarted = false;
    }
}
