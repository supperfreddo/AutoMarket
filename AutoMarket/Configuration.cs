using Dalamud.Configuration;
using System;
using System.Collections.Generic;

namespace AutoMarket;

[Serializable]
public class Configuration : IPluginConfiguration
{
    public int Version { get; set; } = 0;
    public Dictionary<string, bool> EnabledRetainers { get; set; } = new();
    public bool AdjustListings { get; set; } = false;
    public bool PullGil { get; set; } = false;
    public bool ReassignVentures { get; set; } = false;

    public void Save()
    {
        Plugin.PluginInterface.SavePluginConfig(this);
    }
}
