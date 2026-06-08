using CatmullRom;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project6TD;
using Project6TD.Enemies;
using Project6TD.System;
using Project6TD.Systems;
using System;
using System.Collections.Generic;

public class EnemyManager
{
    List<Enemy> enemies = new();

    int timeSinceLastEnemy = 0;
    int milliSecBetweenCreation = 800;
    private List<EnemyType> currentWaveEnemies;
    private int spawnIndex = 0;
    CatmullRomPath path;
    Animation enemyAnimation;
    

    // Track whether a wave was actually started
    private bool waveStarted = false;

    private bool enemyEscaped = false;
    private readonly EconomySystem economySystem;
    private readonly PlayerStats playerStats;

    public EnemyManager(CatmullRomPath path, Animation enemyAnimation, EconomySystem economySystem, PlayerStats playerStats)
    {
        this.path = path;
        this.enemyAnimation = enemyAnimation;
        this.economySystem = economySystem;
        this.playerStats = playerStats;
    }

    public void StartWave(List<EnemyType> enemyTypes, int spawnIntervalMs)
    {
        currentWaveEnemies = enemyTypes;
        spawnIndex = 0;

        milliSecBetweenCreation = spawnIntervalMs;
        timeSinceLastEnemy = 0;

        waveStarted = true;
        enemyEscaped = false;

        System.Diagnostics.Debug.WriteLine($"EnemyManager: StartWave count={enemyTypes.Count}");
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
                    int damageToPlayer = enemies[i] is StrongEnemy ? 3 : 1;
                    playerStats.TakeDamage(damageToPlayer);
                }
                AssetsManager.enemyDamage.Play();
                enemies.RemoveAt(i);
            }
        }
    }

    private void SpawnEnemies(GameTime gameTime)
    {
        if (currentWaveEnemies == null)
            return;

        if (spawnIndex >= currentWaveEnemies.Count)
            return;

        timeSinceLastEnemy += gameTime.ElapsedGameTime.Milliseconds;

        if (timeSinceLastEnemy >= milliSecBetweenCreation)
        {
            timeSinceLastEnemy -= milliSecBetweenCreation;

            EnemyType type = currentWaveEnemies[spawnIndex];
            spawnIndex++;

            Enemy enemy;

            if (type == EnemyType.Strong)
            {
                enemy = new StrongEnemy(
                    path,
                    AssetsManager.Enemy2Walk.Clone()
                );
            }
            else
            {
                enemy = new BasicEnemy(
                    path,
                    enemyAnimation.Clone()
                );
            }

            enemies.Add(enemy);

            System.Diagnostics.Debug.WriteLine($"Spawned {type}, index={spawnIndex}");
        }
    }

    public bool IsWaveFinished =>
        waveStarted &&
        currentWaveEnemies != null &&
        spawnIndex >= currentWaveEnemies.Count &&
        enemies.Count == 0;

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

        Vector2 pos = enemy.Position + new Vector2(-width / 2, -90);

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
        waveStarted = false;
    }
}
