using ImGuiNET;
using Project6TD.System;
using Project6TD.Systems;
using Project6TD.Waves;
using System.Numerics;

namespace Project6TD.UI
{
    public class GameUI
    {
        public bool BuildRequested { get; private set; }
        public bool StartWaveRequested { get; private set; }
        public TowerType SelectedTowerType { get; private set; } = TowerType.Basic;
        // visa wave number
        private float waveMessageTimer = 0f;
        private const float waveMessageDuration = 2f;
        private bool showWaveMessage = false;
        public void Draw(EconomySystem economy, PlayerStats playerStats, WaveManager waveManager, bool waveRunning)
        {
            ImGui.Begin("Tower Defense");
            float hpPercent = (float)playerStats.CurrentHP / playerStats.MaxHP;

            ImGui.Text("Player HP");
            ImGui.ProgressBar(hpPercent, new Vector2(200, 20));
            ImGui.Text($"Money: {economy.Money}");
            ImGui.Text($"Wave: {waveManager.CurrentWaveNumber} / {waveManager.MaxWaves}");
            ImGui.Separator();

            // 🔨 BUILD TOWER
            if (ImGui.Button("Basic Tower (50)"))
            {
                BuildRequested = true;
                SelectedTowerType = TowerType.Basic;
            }

            if (BuildRequested)
            {
                ImGui.TextColored(
                    new Vector4(1, 1, 0, 1),
                    "Click on map to place tower"
                );
            }

            ImGui.Separator();

            if (ImGui.Button("Strong Tower (100)"))
            {
                BuildRequested = true;
                SelectedTowerType = TowerType.Strong;
            }
            if (BuildRequested)
            {
                ImGui.TextColored(new Vector4(1, 1, 0, 1),
                    "Click on map to place tower");
            }

            ImGui.Separator();
            if (ImGui.Button("Slow Tower (75)"))
            {
                BuildRequested = true;
                SelectedTowerType = TowerType.Slow;
            }
            if (BuildRequested)
            {
                ImGui.TextColored(new Vector4(1, 1, 0, 1), "Click on map to place tower");
            }



            if (waveRunning)
            {
                ImGui.Text("Wave in progress...");
            }
            else if (waveManager.HasMoreWaves)
            {
                ImGui.Text("Next wave starting...");
            }
            else
            {
                ImGui.Text("All waves completed!");
            }
            ImGui.End();

            if (showWaveMessage)
            {
                waveMessageTimer -= ImGui.GetIO().DeltaTime;

                if (waveMessageTimer <= 0f)
                    showWaveMessage = false;

                // Rita wave-text i mitten
                var screenSize = ImGui.GetIO().DisplaySize;

                ImGui.SetNextWindowPos(
                    new Vector2(screenSize.X / 2f, screenSize.Y / 2f),
                    ImGuiCond.Always,
                    new Vector2(0.5f, 0.5f));

                ImGui.Begin("WaveMessage",
                    ImGuiWindowFlags.NoDecoration |
                    ImGuiWindowFlags.NoBackground |
                    ImGuiWindowFlags.NoInputs);

                ImGui.SetWindowFontScale(3f);
                ImGui.TextColored(
                    new Vector4(1f, 0.8f, 0f, 1f),
                    $"WAVE {waveManager.CurrentWaveNumber}");


                ImGui.End();
            }
        }

        public void ResetBuildRequest()
        {
            BuildRequested = false;
            SelectedTowerType = TowerType.Basic;
        }

        public void ResetWaveRequest()
        {
            StartWaveRequested = false;
        }

        public void ShowWaveMessage()
        {
            showWaveMessage = true;
            waveMessageTimer = waveMessageDuration;
        }
    }
}
