using Godot;
using PotionsGame.Core.Managers;
using PotionsGame.Core.Mapping;
using PotionsGame.Core.Utils;

namespace PotionsGame.Core
{
    public class Game : Node
    {
        [Export] private bool debug = false;
        [Export] private Maps mainScene = default;

        public override void _Ready()
        {
            SceneManager.Instance.TravelToMap(mainScene);
            if (debug)
            {
                Logger.Instance.Info(nameof(Game), "Running in debug mode, all debug info will be shown");
            }
        }
        
        public override void _Process(float delta)
        {
            if (!debug) return;

            foreach (var c in GetTree().Root.GetChildren())
            {
                RecursiveShowDebugInfo(c);
            }
        }

        private void RecursiveShowDebugInfo(object node)
        {
            if (node is IDisplayDebuggeable { DisplayDebug: true } debuggeable)
            {
                debuggeable.DisplayDebugInfo();
            }

            if (!(node is Node n) || n.GetChildCount() <= 0) return;
            foreach (var child in n.GetChildren())
            {
                RecursiveShowDebugInfo(child);
            }
        }
    }
}
