using Godot;
using MonoCustomResourceRegistry;
using PotionsGame.Player;

namespace PotionsGame.Items
{
    [RegisteredType(nameof(Pickupable), baseType = nameof(RigidBody2D))]
    public class Pickupable: RigidBody2D
    {
        [Export] private NodePath linePath;
        private Sprite line;

        public bool IsReachable
        {
            set => line.Visible = value;
        }

        public override void _Ready()
        {
            line = GetNode<Sprite>(linePath);
        }
        
        // void TryPickUp(PlayerPickUp player);
    }
}