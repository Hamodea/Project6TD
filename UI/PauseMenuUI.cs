using ImGuiNET;
using System.Numerics;

namespace Project6TD.UI
{
    public class PauseMenuUI
    {
        public bool ResumeRequested { get; private set; }
        public bool QuitToMenuRequested { get; private set; }

        public void Draw()
        {
            var viewport = ImGui.GetMainViewport();
            ImGui.SetNextWindowPos(viewport.Pos);
            ImGui.SetNextWindowSize(viewport.Size);

            ImGui.Begin(
                "PauseMenu",
                ImGuiWindowFlags.NoDecoration |
                ImGuiWindowFlags.NoMove |
                ImGuiWindowFlags.NoResize
            );

            const float buttonWidth = 220f;
            const float buttonHeight = 45f;

            Vector2 winSize = ImGui.GetWindowSize();
            float centerX = (winSize.X - buttonWidth) * 0.5f;
            float centerY = winSize.Y * 0.5f - 60;

            ImGui.SetCursorPos(new Vector2(centerX, centerY));
            ImGui.Text("Paused");

            ImGui.Dummy(new Vector2(0, 20));

            ImGui.SetCursorPosX(centerX);
            if (ImGui.Button("Resume", new Vector2(buttonWidth, buttonHeight)))
                ResumeRequested = true;

            ImGui.Dummy(new Vector2(0, 10));

            ImGui.SetCursorPosX(centerX);
            if (ImGui.Button("Quit to Menu", new Vector2(buttonWidth, buttonHeight)))
                QuitToMenuRequested = true;

            ImGui.End();
        }

        public void Reset()
        {
            ResumeRequested = false;
            QuitToMenuRequested = false;
        }
    }
}
