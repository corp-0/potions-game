using System;
using Godot;
using MonoCustomResourceRegistry;

namespace PotionsGame.Player
{
    [RegisteredType(nameof(PlayerMovement))]
    public class PlayerMovement : Node
    {
        [Export] private float speedCoef = 500;
        private Vector2 velocity;
        private KinematicBody2D body;
        public override void _PhysicsProcess(float delta)
        {
            velocity = Input.GetVector(
                Direction.Left.ToMoveControlString(),
                Direction.Right.ToMoveControlString(),
                Direction.Up.ToMoveControlString(),
                Direction.Down.ToMoveControlString()
            );
            
            if (velocity != Vector2.Zero)
            {
                body.Position += body.MoveAndSlide(velocity * delta * speedCoef);
            }
        }

        public override void _Ready()
        {
            body = GetParent<KinematicBody2D>();
            if (body is null)
            {
                throw new InvalidOperationException($"{nameof(PlayerMovement)} must be child of a {nameof(KinematicBody2D)}");
            }
        }
    }
    
    
}
