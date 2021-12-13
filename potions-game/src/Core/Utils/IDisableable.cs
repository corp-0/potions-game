using Godot;

namespace PotionsGame.Core.Utils
{
    public interface IDisableable
    {
        void Disable();
        void Enable();
        bool IsEnabled { get; }
    }
}