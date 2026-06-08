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
        private PlayerStats playerStats;
        private WaveManager waveManager;
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

            // WAVES
            if (!waveRunning)
            {
                if (ImGui.Button("Start Next Wave"))
                {
                    StartWaveRequested = true;
                }
            }
            else
            {
                ImGui.Text("Wave in progress...");
            }

            ImGui.End();
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
    }
}
