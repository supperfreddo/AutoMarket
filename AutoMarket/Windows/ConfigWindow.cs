using System;
using Dalamud.Interface.Windowing;
using ImGuiNET;

namespace AutoMarket.Windows;

public class ConfigWindow : Window, IDisposable
{
    private Plugin Plugin;

    public ConfigWindow(Plugin plugin)
        : base("AutoMarket", ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse)
    {
        Plugin = plugin;
    }

    public void Dispose() { }

    public override void Draw()
    {
        if (ImGui.BeginTabBar("##ConfigTabs"))
        {
            if (ImGui.BeginTabItem("General"))
            {
                bool adjustListings = Plugin.Configuration.AdjustListings;
                if (ImGui.Checkbox("Adjust Listings", ref adjustListings))
                {
                    Plugin.Configuration.AdjustListings = adjustListings;
                    Plugin.Configuration.Save();
                }
                if (ImGui.IsItemHovered())
                {
                    ImGui.BeginTooltip();
                    ImGui.SetTooltip("If enabled, will automatically adjust market board listings to undercut the lowest price.");
                    ImGui.EndTooltip();
                }

                bool pullGil = Plugin.Configuration.PullGil;
                if (ImGui.Checkbox("Pull Gil", ref pullGil))
                {
                    Plugin.Configuration.PullGil = pullGil;
                    Plugin.Configuration.Save();
                }
                if (ImGui.IsItemHovered())
                {
                    ImGui.BeginTooltip();
                    ImGui.SetTooltip("If enabled, will automatically pull gil from retainers.");
                    ImGui.EndTooltip();
                }

                bool reassignVentures = Plugin.Configuration.ReassignVentures;
                if (ImGui.Checkbox("Reassign Ventures", ref reassignVentures))
                {
                    Plugin.Configuration.ReassignVentures = reassignVentures;
                    Plugin.Configuration.Save();
                }
                if (ImGui.IsItemHovered())
                {
                    ImGui.BeginTooltip();
                    ImGui.SetTooltip("If enabled, will automatically reassign ventures.");
                    ImGui.EndTooltip();
                }

                ImGui.EndTabItem();
            }
        }
    }
}
