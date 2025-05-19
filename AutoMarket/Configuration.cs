using Dalamud.Configuration;
using System;

namespace AutoMarket;

[Serializable]
public class Configuration : IPluginConfiguration
{
    public int Version { get; set; } = 0;
    public bool AdjustListings { get; set; } = false;
    public bool PullGil { get; set; } = false;
    public bool ReassignVentures { get; set; } = false;

    public void Save()
    {
        Plugin.PluginInterface.SavePluginConfig(this);
    }
}
