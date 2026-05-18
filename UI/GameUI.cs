using ImGuiNET;
using Project6TD.Systems;
using System.Numerics;

namespace Project6TD.UI
{
    public class GameUI
    {
        public bool BuildRequested { get; private set; }
        public bool StartWaveRequested { get; private set; }
        public TowerType SelectedTowerType { get; private set; } = TowerType.Basic;

        public void Draw(EconomySystem economy, int currentWave, bool waveRunning)
        {
            ImGui.Begin("Tower Defense");

            ImGui.Text($"Money: {economy.Money}");
            ImGui.Text($"Wave: {currentWave}");
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
