using System;
using System.Linq;
using System.Numerics;
using Dalamud.Interface.Utility;
using Dalamud.Interface.Windowing;
using ECommons;
using ECommons.DalamudServices;
using FFXIVClientStructs.FFXIV.Component.GUI;
using ImGuiNET;

namespace AutoMarket
{
    internal class AutoMarket : Window, IDisposable
    {
        private Plugin Plugin;

        public AutoMarket(Plugin plugin) : base("AutoMarket Overlay", ImGuiWindowFlags.NoDecoration | ImGuiWindowFlags.NoBackground)
        {
            Plugin = plugin;
            IsOpen = true;
        }

        public void Dispose() { }

        public override void Draw()
        {
            try
            {
                unsafe
                {
                    if (GenericHelpers.TryGetAddonByName<AtkUnitBase>("RetainerList", out var addon) && GenericHelpers.IsAddonReady(addon))
                    {
                        var node = addon->UldManager.NodeList[0]; // First node of the retainer list
                        if (node == null) return;

                        var position = new Vector2(addon->X + 80, addon->Y + 10);
                        var scale = new Vector2(addon->Scale, addon->Scale);

                        ImGuiHelpers.ForceNextWindowMainViewport();
                        ImGuiHelpers.SetNextWindowPosRelativeMainViewport(position);

                        ImGui.PushStyleColor(ImGuiCol.WindowBg, 0);
                        ImGui.PushStyleVar(ImGuiStyleVar.FrameRounding, 0f);
                        ImGui.PushStyleVar(ImGuiStyleVar.FramePadding, new Vector2(3f, 3f));
                        ImGui.PushStyleVar(ImGuiStyleVar.WindowPadding, new Vector2(0f, 0f));
                        ImGui.PushStyleVar(ImGuiStyleVar.WindowBorderSize, 0f);
                        ImGui.PushStyleVar(ImGuiStyleVar.WindowMinSize, new Vector2(0, 0));

                        ImGui.Begin("###AutoMarketButton",
                            ImGuiWindowFlags.AlwaysAutoResize |
                            ImGuiWindowFlags.NoScrollbar |
                            ImGuiWindowFlags.NoNavFocus |
                            ImGuiWindowFlags.AlwaysUseWindowPadding |
                            ImGuiWindowFlags.NoTitleBar |
                            ImGuiWindowFlags.NoSavedSettings |
                            ImGuiWindowFlags.NoBackground);

                        if (ImGui.Button("Auto Market")) Plugin.ToggleConfigUI();

                        ImGui.SameLine();

                        // Check if any retainers are selected
                        bool hasSelectedRetainers = Plugin.Configuration.EnabledRetainers.Any(kvp => kvp.Value);

                        if (!hasSelectedRetainers) ImGui.BeginDisabled();

                        // Start button
                        if (ImGui.Button("Start"))
                        {
                            // Start the AutoMarket process
                        }

                        if (!hasSelectedRetainers) ImGui.EndDisabled();

                        ImGui.End();
                        ImGui.PopStyleVar(5);
                        ImGui.PopStyleColor();
                    }
                }
            }
            catch (Exception ex)
            {
                Svc.Log.Error(ex, "Error in AutoMarket");
            }
        }
    }
}