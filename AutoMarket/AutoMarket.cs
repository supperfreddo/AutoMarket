using System;
using Dalamud.Interface.Windowing;
using ImGuiNET;

namespace AutoMarket
{
    internal class AutoMarket : Window, IDisposable
    {
        private Plugin Plugin;

        public AutoMarket(Plugin plugin) : base("AutoMarket Overlay", ImGuiWindowFlags.NoDecoration | ImGuiWindowFlags.NoBackground)
        {
            Plugin = plugin;
        }

        public override void Draw() { }

        public void Dispose() { }
    }
}