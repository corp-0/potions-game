using MonoCustomResourceRegistry;
using Godot;
using System;
using PotionsGame.Core.Extensions;
using PotionsGame.Core.Utils;

namespace PotionsGame.Player
{
    [RegisteredType(nameof(PlayerAiming), baseType = nameof(Node2D))]
    public class PlayerAiming: Node2D
    {
        private Vector2 aimingDirection;
        private Sprite crosshair;
        public Subject<bool> IsAiming = new Subject<bool>(false);
        private bool isAiming;
        
        public override void _PhysicsProcess(float delta)
        {
            aimingDirection = Input.GetVector(
                Direction.Left.ToAimControlString(),
                Direction.Right.ToAimControlString(),
                Direction.Up.ToAimControlString(),
                Direction.Down.ToAimControlString()
            );

            isAiming = aimingDirection != Vector2.Zero;
            IsAiming.Value = isAiming && isAiming != IsAiming.Value; // Notify subscribers only if value changed
            crosshair.Visible = isAiming;
            Rotation = aimingDirection.Angle();
        }

        public override void _Ready()
        {
            crosshair = this.GetNodeByTypeOrNull<Sprite>();
        }
        
    }
}