using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project6TD.Enemies;
using Project6TD.Projectiles;
using System.Collections.Generic;

namespace Project6TD.Towers
{
    public class TowerManager
    {
        private List<Tower> towers = new();
        public void AddTower(Tower tower)
        {
            towers.Add(tower);
        }

        public void Update(GameTime gameTime, EnemyManager enemyManager, ProjectileManager projectileManager)
        {
            foreach (var tower in towers)
            {
                tower.Update(gameTime);

                Enemy target = enemyManager.GetClosestEnemy(tower.Position);

                if (target != null)
                {
                    float distance = Vector2.Distance(tower.Position, target.Position);

                    if (distance <= tower.Range) // <-- RANGE CHECK
                    {
                        tower.TryAttack(target, projectileManager, gameTime);
                    }
                    
                    
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var tower in towers)
                tower.Draw(spriteBatch);
        }
        public void Clear()
        {
            towers.Clear();
        }
    }
}
