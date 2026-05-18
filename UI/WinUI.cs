using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Project6TD.UI
{
    public class WinUI
    {
        public bool NextLevelRequested { get; private set; }
        public bool MenuRequested { get; private set; }

        public void Draw()
        {
            var viewport = ImGui.GetMainViewport();

            ImGui.SetNextWindowPos(viewport.Pos);
            ImGui.SetNextWindowSize(viewport.Size);

            // transparent window
            ImGui.SetNextWindowBgAlpha(0f);

            ImGui.Begin(
                "WinScreen",
                ImGuiWindowFlags.NoDecoration |
                ImGuiWindowFlags.NoMove |
                ImGuiWindowFlags.NoResize
            );

            // mörk overlay över spelet
            var drawList = ImGui.GetBackgroundDrawList();
            drawList.AddRectFilled(
                viewport.Pos,
                viewport.Pos + viewport.Size,
                ImGui.ColorConvertFloat4ToU32(new Vector4(0, 0, 0, 0.55f))
            );

            const float buttonWidth = 240f;
            const float buttonHeight = 50f;
            const float spacing = 18f;

            Vector2 winSize = ImGui.GetWindowSize();

            float centerX = (winSize.X - buttonWidth) * 0.5f;
            float startY = winSize.Y * 0.5f - 100;

            // ===== TITLE =====
            ImGui.SetCursorPos(new Vector2(centerX, startY));

            ImGui.PushStyleColor(ImGuiCol.Text, new Vector4(1f, 0.85f, 0.2f, 1f));
            ImGui.SetWindowFontScale(2.6f);
            ImGui.Text("VICTORY!");
            ImGui.SetWindowFontScale(1f);
            ImGui.PopStyleColor();

            ImGui.Dummy(new Vector2(0, 30));

            // ===== NEXT LEVEL BUTTON =====
            ImGui.PushStyleColor(ImGuiCol.Button, new Vector4(0.2f, 0.6f, 0.2f, 1f));
            ImGui.PushStyleColor(ImGuiCol.ButtonHovered, new Vector4(0.3f, 0.75f, 0.3f, 1f));
            ImGui.PushStyleColor(ImGuiCol.ButtonActive, new Vector4(0.1f, 0.5f, 0.1f, 1f));

            ImGui.SetCursorPosX(centerX);
            if (ImGui.Button("Next Level", new Vector2(buttonWidth, buttonHeight)))
                NextLevelRequested = true;

            ImGui.PopStyleColor(3);

            ImGui.Dummy(new Vector2(0, spacing));

            // ===== MENU BUTTON =====
            ImGui.PushStyleColor(ImGuiCol.Button, new Vector4(0.2f, 0.4f, 0.7f, 1f));
            ImGui.PushStyleColor(ImGuiCol.ButtonHovered, new Vector4(0.3f, 0.5f, 0.85f, 1f));
            ImGui.PushStyleColor(ImGuiCol.ButtonActive, new Vector4(0.15f, 0.3f, 0.6f, 1f));

            ImGui.SetCursorPosX(centerX);
            if (ImGui.Button("Back to Menu", new Vector2(buttonWidth, buttonHeight)))
                MenuRequested = true;

            ImGui.PopStyleColor(3);

            ImGui.End();
        }

        public void Reset()
        {
            NextLevelRequested = false;
            MenuRequested = false;
        }
    }
}
