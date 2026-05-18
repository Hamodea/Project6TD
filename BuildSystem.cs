using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Project6TD.Systems;
using Project6TD.Towers;


namespace Project6TD
{
    public enum TowerType
    {
        Basic,
        Strong
    }

    public class BuildSystem
    {
        private Level level;
        private TowerManager towerManager;
        private ButtonState previousMouseState = ButtonState.Released;
        public bool IsBuilding { get; private set; }
        private ParticleSystem particleSystem;
        private EconomySystem economySystem;
        // default cost for basic, strong cost is handled per-type
        private int towerCost = 50;

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

            // Klick-detekt (pressed THIS frame)
            if (mouse.LeftButton == ButtonState.Pressed &&
                previousMouseState == ButtonState.Released)
            {
                if (!level.IsPlacementAllowed(mouse.X, mouse.Y))
                    goto End;

                int cost = pendingTowerType == TowerType.Basic ? 50 : 100;

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



    }
}
