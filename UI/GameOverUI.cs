using ImGuiNET;
using System.Numerics;

namespace Project6TD.UI
{
    public class GameOverUI
    {
        public bool RestartRequested { get; private set; }
        public bool QuitRequested { get; private set; }
        public bool QuitToMenuRequested { get; private set; }
        public void Draw()
        {
            var viewport = ImGui.GetMainViewport();

            // fullscreen window
            ImGui.SetNextWindowPos(viewport.Pos);
            ImGui.SetNextWindowSize(viewport.Size);

            // transparent background
            ImGui.SetNextWindowBgAlpha(0.5f);

            ImGui.Begin(
                "GameOver",
                ImGuiWindowFlags.NoDecoration |
                ImGuiWindowFlags.NoMove |
                ImGuiWindowFlags.NoResize
            );

            // mörk overlay över spelet
            var drawList = ImGui.GetBackgroundDrawList();
            drawList.AddRectFilled(
                viewport.Pos,
                viewport.Pos + viewport.Size,
                ImGui.ColorConvertFloat4ToU32(new Vector4(0, 0, 0, 0.6f))
            );

            const float buttonWidth = 240f;
            const float buttonHeight = 50f;
            const float spacing = 18f;

            Vector2 winSize = ImGui.GetWindowSize();

            float centerX = (winSize.X - buttonWidth) * 0.5f;
            float startY = winSize.Y * 0.5f - 100;

            // ===== TITLE =====
            ImGui.SetCursorPos(new Vector2(centerX, startY));

            ImGui.PushStyleColor(ImGuiCol.Text, new Vector4(1f, 0.2f, 0.2f, 1f));
            ImGui.SetWindowFontScale(2.5f);
            ImGui.Text("GAME OVER");
            ImGui.SetWindowFontScale(1f);
            ImGui.PopStyleColor();

            ImGui.Dummy(new Vector2(0, 30));

            // ===== RESTART BUTTON =====
            ImGui.PushStyleColor(ImGuiCol.Button, new Vector4(0.2f, 0.6f, 0.2f, 1f));
            ImGui.PushStyleColor(ImGuiCol.ButtonHovered, new Vector4(0.3f, 0.7f, 0.3f, 1f));
            ImGui.PushStyleColor(ImGuiCol.ButtonActive, new Vector4(0.1f, 0.5f, 0.1f, 1f));

            ImGui.SetCursorPosX(centerX);
            if (ImGui.Button("Restart", new Vector2(buttonWidth, buttonHeight)))
                RestartRequested = true;

            ImGui.PopStyleColor(3);

            ImGui.Dummy(new Vector2(0, spacing));

            // ===== QUIT BUTTON =====
            ImGui.PushStyleColor(ImGuiCol.Button, new Vector4(0.6f, 0.2f, 0.2f, 1f));
            ImGui.PushStyleColor(ImGuiCol.ButtonHovered, new Vector4(0.8f, 0.3f, 0.3f, 1f));
            ImGui.PushStyleColor(ImGuiCol.ButtonActive, new Vector4(0.5f, 0.1f, 0.1f, 1f));

            ImGui.SetCursorPosX(centerX);
            if (ImGui.Button("Quit to Menu", new Vector2(buttonWidth, buttonHeight)))
                QuitRequested = true;

            ImGui.PopStyleColor(3);

            ImGui.End();
        }

        public void Reset()
        {
            RestartRequested = false;
            QuitRequested = false;
        }
    }
}
