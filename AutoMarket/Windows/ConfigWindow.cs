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
                ImGui.EndTabItem();
            }
        }
    }
}
