using System;
using Dalamud.Interface.Windowing;
using ImGuiNET;
using ECommons;
using ECommons.UIHelpers.AtkReaderImplementations;
using FFXIVClientStructs.FFXIV.Component.GUI;

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

            if (ImGui.BeginTabItem("Retainers"))
            {
                unsafe
                {
                    if (GenericHelpers.TryGetAddonByName<AtkUnitBase>("RetainerList", out var addon) && GenericHelpers.IsAddonReady(addon))
                    {
                        var reader = new ReaderRetainerList(addon);
                        var retainers = reader.Retainers;

                        if (ImGui.BeginTable("##RetainerTable", 5, ImGuiTableFlags.Borders | ImGuiTableFlags.RowBg | ImGuiTableFlags.SizingFixedFit))
                        {
                            ImGui.TableSetupColumn("");
                            ImGui.TableSetupColumn("Name");
                            ImGui.TableSetupColumn("Level");
                            ImGui.TableSetupColumn("Inventory");
                            ImGui.TableSetupColumn("Gil");
                            ImGui.TableHeadersRow();

                            foreach (var retainer in retainers)
                            {
                                ImGui.TableNextRow();

                                if (!retainer.IsActive)
                                {
                                    ImGui.PushStyleColor(ImGuiCol.Text, new System.Numerics.Vector4(0.5f, 0.5f, 0.5f, 1.0f));
                                }

                                ImGui.TableNextColumn();
                                bool enabled = false;
                                Plugin.Configuration.EnabledRetainers.TryGetValue(retainer.Name, out enabled);
                                if (!retainer.IsActive)
                                {
                                    ImGui.BeginDisabled();
                                    enabled = false; // Force disabled state for inactive retainers
                                }
                                if (ImGui.Checkbox($"##{retainer.Name}", ref enabled))
                                {
                                    if (retainer.IsActive) // Only allow changes for active retainers
                                    {
                                        Plugin.Configuration.EnabledRetainers[retainer.Name] = enabled;
                                        Plugin.Configuration.Save();
                                    }
                                }
                                if (!retainer.IsActive)
                                {
                                    ImGui.EndDisabled();
                                }

                                ImGui.TableNextColumn();
                                ImGui.Text(retainer.Name);

                                ImGui.TableNextColumn();
                                ImGui.Text($"{retainer.Level}");

                                ImGui.TableNextColumn();
                                ImGui.Text($"{retainer.Inventory}/175");

                                ImGui.TableNextColumn();
                                ImGui.Text($"{retainer.Gil:N0}");

                                if (!retainer.IsActive)
                                {
                                    ImGui.PopStyleColor();
                                }
                            }

                            ImGui.EndTable();
                        }
                    }
                    else
                    {
                        ImGui.Text("Retainer list not available, talk to a summoning bell.");
                    }
                }
                ImGui.EndTabItem();
            }
        }
    }
}
