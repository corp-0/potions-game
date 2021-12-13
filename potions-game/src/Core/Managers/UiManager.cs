using System;
using Godot;
using MonoCustomResourceRegistry;
using PotionsGame.Core.Utils;
using PotionsGame.Player.UI;

namespace PotionsGame.Core.Managers
{
    [RegisteredType(nameof(UiManager))]
    public class UiManager: SingletonAutoload<UiManager>
    {
        public InventoryHud InventoryHud { get; set; } 
    }
}