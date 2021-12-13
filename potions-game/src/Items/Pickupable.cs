using Godot;
using MonoCustomResourceRegistry;
using PotionsGame.Core.Managers;
using PotionsGame.Core.Utils;

namespace PotionsGame.Items
{
    [RegisteredType(nameof(Pickupable), baseType = nameof(RigidBody2D))]
    public class Pickupable: RigidBody2D, IDisableable
    {
        [Export] private NodePath spritePath;
        [Export] private NodePath linePath;
        private Sprite sprite;
        private Sprite line;

        public Texture SpriteTexture => sprite.Texture;

        public bool IsReachable
        {
            set => line.Visible = value;
        }

        public override void _Ready()
        {
            sprite = GetNode<Sprite>(spritePath);
            line = GetNode<Sprite>(linePath);
        }

        public void Disable()
        {
            GetParent().RemoveChild(this);
        }

        public void Enable()
        {
            SceneManager.Instance.CurrentMapNode.AddObject(this);
        }

        public bool IsEnabled => GetParent() != null;
    }
}