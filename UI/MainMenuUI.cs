using ImGuiNET;
using System;
using System.Numerics;

namespace Project6TD.UI
{
    public class MainMenuUI
    {
        public bool StartGameRequested { get; private set; }
        public bool ExitRequested { get; private set; }
       

        public void Draw()
        {
           
            var viewport = ImGui.GetMainViewport();

            // fullscreen 
            ImGui.SetNextWindowPos(viewport.Pos);
            ImGui.SetNextWindowSize(viewport.Size);
            ImGui.GetStyle().FrameRounding = 8f;

            ImGui.SetNextWindowBgAlpha(0.8f);
            ImGui.Begin(
                "MainMenuFullscreen",
                ImGuiWindowFlags.NoDecoration |
                ImGuiWindowFlags.NoMove |
                ImGuiWindowFlags.NoResize |
                ImGuiWindowFlags.NoBringToFrontOnFocus
            );

            // center 
            const float buttonWidth = 220f;
            const float buttonHeight = 45f;
            const float spacing = 15f;

            float totalHeight =
                buttonHeight * 2 +
                spacing +
                30; // titel + lite luft

            Vector2 windowSize = ImGui.GetWindowSize();

            float startY = (windowSize.Y - totalHeight) * 0.5f;
            float centerX = (windowSize.X - buttonWidth) * 0.5f;

            // Titel
            ImGui.SetCursorPos(new Vector2(centerX, startY));
            ImGui.PushStyleColor(ImGuiCol.Text, new Vector4(1f, 0.8f, 0.2f, 1f));
            ImGui.SetWindowFontScale(2.0f);
            ImGui.Text("* TOWER DEFENSE *");
            ImGui.SetWindowFontScale(1.0f);
            ImGui.PopStyleColor();

            ImGui.Dummy(new Vector2(0, 20));

            // New Game
            ImGui.PushStyleColor(ImGuiCol.Button, new Vector4(0.2f, 0.6f, 0.2f, 1f));
            ImGui.PushStyleColor(ImGuiCol.ButtonHovered, new Vector4(0.3f, 0.7f, 0.3f, 1f));
            ImGui.PushStyleColor(ImGuiCol.ButtonActive, new Vector4(0.1f, 0.5f, 0.1f, 1f));

            ImGui.SetCursorPosX(centerX);
            if (ImGui.Button("New Game", new Vector2(buttonWidth, buttonHeight)))
                StartGameRequested = true;

            ImGui.PopStyleColor(3);

            ImGui.Dummy(new Vector2(0, spacing));

            // Exit
            ImGui.PushStyleColor(ImGuiCol.Button, new Vector4(0.6f, 0.2f, 0.2f, 1f));
            ImGui.PushStyleColor(ImGuiCol.ButtonHovered, new Vector4(0.8f, 0.3f, 0.3f, 1f));
            ImGui.PushStyleColor(ImGuiCol.ButtonActive, new Vector4(0.5f, 0.1f, 0.1f, 1f));

            ImGui.SetCursorPosX(centerX);
            if (ImGui.Button("Exit", new Vector2(buttonWidth, buttonHeight)))
                ExitRequested = true;

            ImGui.PopStyleColor(3);


            ImGui.End();
        }

        public void Reset()
        {
            StartGameRequested = false;
            ExitRequested = false;
        }
    }
}
