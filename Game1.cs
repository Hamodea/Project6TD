using ImGuiNET;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.ImGuiNet;
using Project6TD.Projectiles;
using Project6TD.Waves;
using Project6TD.Levels;
using Project6TD.UI;
using Project6TD.Towers;
using Project6TD.Systems;
using System.Diagnostics;
using Project6TD.Core;

namespace Project6TD
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        // Core
        private Level level;

        // Managers
        private EnemyManager enemyManager;
        private TowerManager towerManager;
        private WaveManager waveManager;
        private ProjectileManager projectileManager;
        private BuildSystem buildSystem;
        private ParticleSystem particleSystem;
        private EconomySystem economySystem;

        // UI
        private ImGuiRenderer imGuiRenderer;
        private GameUI gameUI;
        private MainMenuUI mainMenuUI;
        GameState currentState = GameState.MainMenu;
        PauseMenuUI pauseMenuUI;
        GameOverUI gameOverUI;
        
        GameStateManager gameStateManager;
        private bool drawEnemy2Test = true;

        // Economy
        private int money = 200;
        

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            graphics.ApplyChanges();

           

            Window.Title = "Tower Defense";
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // --- Assets (textures + animations) ---
            AssetsManager.LoadTexture(Content, GraphicsDevice);
            AssetsManager.LoadSounds(Content);
          

            // --- Level (background, path, placementMask) ---
            level = new Level(GraphicsDevice, Content);

            // --- Economy ---
            economySystem = new EconomySystem(money);

            // --- Managers ---
            enemyManager = new EnemyManager(level.RoadPath, AssetsManager.EnemyWalk, economySystem);
            towerManager = new TowerManager();
            projectileManager = new ProjectileManager();
            waveManager = new WaveManager(enemyManager);

            particleSystem = new ParticleSystem();

            // --- Build / placement system ---
            buildSystem = new BuildSystem(level, towerManager, particleSystem, economySystem);

            // --- ImGui ---
            imGuiRenderer = new ImGuiRenderer(this);
            imGuiRenderer.RebuildFontAtlas();
            gameStateManager = new GameStateManager(enemyManager, towerManager, projectileManager, waveManager,
                buildSystem, particleSystem, economySystem);
            
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
             {
                AssetsManager.towerShoot.Play();
                Debug.WriteLine("test");
            }

            gameStateManager.Update(gameTime);
            

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // MENU BACKGROUND 
            if (gameStateManager.CurrentState == GameState.MainMenu)
            {
                spriteBatch.Begin();

                spriteBatch.Draw(
                    AssetsManager.bakgroundTex,
                    new Rectangle(
                        0,
                        0,
                        GraphicsDevice.Viewport.Width,
                        GraphicsDevice.Viewport.Height),
                    Color.White 
                );

                spriteBatch.End();
            }

            // ===== WORLD / GAMEPLAY DRAW =====
            if (gameStateManager.CurrentState != GameState.MainMenu)
            {
                spriteBatch.Begin();
                level.Draw(spriteBatch);
                spriteBatch.End();

                level.DrawRoad(spriteBatch);

                spriteBatch.Begin();

               
                enemyManager.Draw(spriteBatch);
                towerManager.Draw(spriteBatch);
                projectileManager.Draw(spriteBatch);
                particleSystem.Draw(spriteBatch);

                spriteBatch.End();
            }

            // ===== UI =====
            gameStateManager.DrawUI(gameTime, imGuiRenderer);

            base.Draw(gameTime);
        }
        
        

    }
}
