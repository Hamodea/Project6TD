using ImGuiNET;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.ImGuiNet;
using Project6TD.Projectiles;
using Project6TD.Towers;

using Project6TD.UI;
using Project6TD.Waves;
using System;
using Project6TD.Systems;
using Microsoft.Xna.Framework.Media;

namespace Project6TD.Core
{
    public class GameStateManager
    {
        public GameState CurrentState { get; private set; } = GameState.MainMenu;

        // UI
        private MainMenuUI mainMenuUI = new();
        private GameUI gameUI = new();
        private PauseMenuUI pauseMenuUI = new();
        private GameOverUI gameOverUI = new();
        private WinUI winUI = new();

        // Gameplay systems (referenser)
        private EnemyManager enemyManager;
        private TowerManager towerManager;
        private ProjectileManager projectileManager;
        private WaveManager waveManager;
        private BuildSystem buildSystem;
        private ParticleSystem particleSystem;

        private EconomySystem economySystem;

        public bool GameplayActive => CurrentState == GameState.Playing;

        public GameStateManager(
            EnemyManager enemyManager,
            TowerManager towerManager,
            ProjectileManager projectileManager,
            WaveManager waveManager,
            BuildSystem buildSystem,
            ParticleSystem particleSystem,
            EconomySystem economySystem)
        {
            this.enemyManager = enemyManager;
            this.towerManager = towerManager;
            this.projectileManager = projectileManager;
            this.waveManager = waveManager;
            this.buildSystem = buildSystem;
            this.particleSystem = particleSystem;
            this.economySystem = economySystem;
        }

        public void Update(GameTime gameTime)
        {
            switch (CurrentState)
            {
                case GameState.MainMenu:
                    UpdateMainMenu();
                    
                    break;

                case GameState.Playing:
                    UpdatePlaying(gameTime);
                    break;

                case GameState.Paused:
                    UpdatePaused();
                    break;

                case GameState.Win:
                    UpdateWinState();
                    break;

                case GameState.GameOver:
                    UpdateGameOver();
                    break;

            }
        }

        private void UpdateMainMenu()
        {
            
            if (mainMenuUI.StartGameRequested)
            {
                
                StartNewGame();
                CurrentState = GameState.Playing;
                mainMenuUI.Reset();
            }

            if (mainMenuUI.ExitRequested)
                Environment.Exit(0);
        }

        private void UpdatePlaying(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                CurrentState = GameState.Paused;
                return;
            }

            if (gameUI.BuildRequested)
            {
                buildSystem.StartBuilding(gameUI.SelectedTowerType);
                gameUI.ResetBuildRequest();
            }

            enemyManager.Update(gameTime);
            projectileManager.Update(gameTime);
            towerManager.Update(gameTime, enemyManager, projectileManager);
            waveManager.Update();
            particleSystem.Update(gameTime);

            if (gameUI.StartWaveRequested)
            {
                waveManager.StartNextWave();
                gameUI.ResetBuildRequest();
            }

            if (!ImGui.GetIO().WantCaptureMouse)
                buildSystem.Update(gameTime); 

            if (enemyManager.HasEnemyEscaped)
            {
                CurrentState = GameState.GameOver;
            }
                // Win state 
            if (!waveManager.HasMoreWaves && enemyManager.IsWaveFinished && !waveManager.IsWaveRunning)
            {
                CurrentState = GameState.Win;
            }
                
        }

        private void UpdatePaused()
        {
            if (pauseMenuUI.ResumeRequested)
            {
                CurrentState = GameState.Playing;
                pauseMenuUI.Reset();
            }

            if (pauseMenuUI.QuitToMenuRequested)
            {
                CurrentState = GameState.MainMenu;
                pauseMenuUI.Reset();
            }
        }

        private void UpdateGameOver()
        {
            if (gameOverUI.RestartRequested)
            {
                StartNewGame();
                CurrentState = GameState.Playing;
                gameOverUI.Reset();
            }

            if (gameOverUI.QuitRequested)
            {
                CurrentState = GameState.MainMenu;
                gameOverUI.Reset();
            }
        }
        private void UpdateWinState()
        {
            if (winUI.MenuRequested)
            {
                CurrentState = GameState.MainMenu;
                winUI.Reset();
            }
        }

        private void StartNewGame()
        {
            economySystem.Reset(200);

            enemyManager.Reset();
            towerManager.Clear();
            waveManager.Reset();
            buildSystem.CancelBuilding();
            waveManager.StartNextWave();
        }

        public void DrawUI(GameTime gameTime, ImGuiRenderer imGuiRenderer)
        {
            imGuiRenderer.BeginLayout(gameTime);

            switch (CurrentState)
            {
                case GameState.MainMenu:
                    mainMenuUI.Draw();
                    break;

                case GameState.Playing:
                   
                    gameUI.Draw( economySystem,
                        waveManager.CurrentWaveNumber,
                        waveManager.IsWaveRunning);
                    break;

                case GameState.Paused:
                    pauseMenuUI.Draw();
                    break;

                case GameState.GameOver:
                    gameOverUI.Draw();
                    break;
                case GameState.Win:
                    winUI.Draw();
                    break;
            }

            imGuiRenderer.EndLayout();
        }

        public void startIntro()
        {
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.4f;
            MediaPlayer.Play(AssetsManager.gameIntro);
        }
    }
}
