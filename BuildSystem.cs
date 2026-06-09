using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project6TD.Systems;
using Project6TD.Towers;


namespace Project6TD
{
    public enum TowerType
    {
        Basic,
        Strong,
        Slow
    }

    public class BuildSystem
    {
        private Level level;
        private TowerManager towerManager;
        private ButtonState previousMouseState = ButtonState.Released;
        public bool IsBuilding { get; private set; }
        private ParticleSystem particleSystem;
        private EconomySystem economySystem;
       

        private TowerType pendingTowerType = TowerType.Basic;

        public BuildSystem(Level level, TowerManager towerManager, ParticleSystem particleSystem, EconomySystem economy)
        {
            this.level = level;
            this.towerManager = towerManager;
            this.particleSystem = particleSystem;
            this.economySystem = economy;
        }

        public void StartBuilding()
        {
            IsBuilding = true;
            pendingTowerType = TowerType.Basic;
        }

        // overload to start building a specific tower type
        public void StartBuilding(TowerType type)
        {
            IsBuilding = true;
            pendingTowerType = type;
        }

        public void CancelBuilding()
        {
            IsBuilding = false;
        }

        // Now uses the internal EconomySystem instead of a ref int
        public void Update(GameTime gameTime)
        {
            if (!IsBuilding)
                return;

            var mouse = Mouse.GetState();
            int towerRadius = 20;
            // Klick-detekt
            if (mouse.LeftButton == ButtonState.Pressed &&
                previousMouseState == ButtonState.Released)
            {
                if (!level.IsPlacementAllowed(mouse.X, mouse.Y, towerRadius))
                    goto End;

                int cost = pendingTowerType switch
                {
                    TowerType.Basic => 50,
                    TowerType.Slow => 75,
                    TowerType.Strong => 100,
                    _ => 50
                };

                if (!economySystem.CanAfford(cost))
                    goto End;

                if (pendingTowerType == TowerType.Basic)
                {
                    towerManager.AddTower(
                        new BasicTower(
                            new Vector2(mouse.X, mouse.Y),
                            AssetsManager.towerTex,
                            particleSystem
                        )
                    );
                }
                else if (pendingTowerType == TowerType.Slow)
                {
                    towerManager.AddTower(
                        new SlowTower(
                            new Vector2(mouse.X, mouse.Y),
                            AssetsManager.slowTowerTex, 
                            particleSystem
                        )
                     );
                }
                else // Strong
                {
                    towerManager.AddTower(
                        new StrongTower(
                            new Vector2(mouse.X, mouse.Y),
                            AssetsManager.strongtowerTex,
                            particleSystem
                        )
                    );
                }

                economySystem.Spend(cost);
                IsBuilding = false;
            }

        End:
            previousMouseState = mouse.LeftButton;
        }

        public void DrawPreview(SpriteBatch spriteBatch)
        {
            if (!IsBuilding)
                return;

            var mouse = Mouse.GetState();
            Vector2 pos = new Vector2(mouse.X, mouse.Y);
            int towerRadius = 20;
            bool allowed = level.IsPlacementAllowed(mouse.X, mouse.Y, towerRadius);
            int cost = pendingTowerType switch
            {
                TowerType.Basic => 50,
                TowerType.Slow => 75,
                TowerType.Strong => 100,
                _ => 50
            };
            bool canAfford = economySystem.CanAfford(cost);

            Color previewColor = (allowed && canAfford)
                ? Color.LimeGreen * 0.8f
                : Color.Red * 0.8f;

            Texture2D tex = pendingTowerType switch
            {
                TowerType.Basic => AssetsManager.towerTex,
                TowerType.Slow => AssetsManager.slowTowerTex,
                TowerType.Strong => AssetsManager.strongtowerTex,
                _ => AssetsManager.towerTex
            };

            float range = pendingTowerType switch
            {
                TowerType.Basic => 160f,
                TowerType.Slow => 170f,
                TowerType.Strong => 190f,
                _ => 160f
            };

            float scale = (range * 2f) / AssetsManager.cirkelTex.Width;

            spriteBatch.Draw(
                AssetsManager.cirkelTex,
                pos,
                null,
                previewColor * 0.4f,
                0f,
                new Vector2(
                    AssetsManager.cirkelTex.Width / 2f,
                    AssetsManager.cirkelTex.Height / 2f),
                scale,
                SpriteEffects.None,
                0f
            );

            //  Rita tornet ovanpå
            Vector2 origin = new Vector2(tex.Width / 2f, tex.Height / 2f);

            spriteBatch.Draw(
                tex,
                pos,
                null,
                previewColor * 0.8f,
                0f,
                origin,
                1f,
                SpriteEffects.None,
                0f
            );
        }
    }
}