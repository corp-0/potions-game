using Godot;
using MonoCustomResourceRegistry;
using PotionsGame.Core.Extensions;

namespace PotionsGame.Player.UI
{
    [RegisteredType(nameof(InventoryEntry), baseType = nameof(TextureRect))]
    public class InventoryEntry: TextureRect
    {
        [Export] private AtlasTexture inventoryTexture;
        [Export] private AtlasTexture selectedTexture;
        public TextureRect ItemIcon {get; private set;}
        
        public bool IsSelected { get; private set; }
        
        public override void _Ready()
        {
            ItemIcon = this.GetNodeByTypeOrNull<TextureRect>();
        }
        
        public void Select()
        {
            IsSelected = true;
            Texture = selectedTexture;
        }
        
        public void Deselect()
        {
            IsSelected = false;
            Texture = inventoryTexture;
        }
    }
}