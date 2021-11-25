using System;

namespace PotionsGame.Player
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    public static class DirectionExtensions
    {
        public static string ToMoveControlString(this Direction direction)
        {
            return direction switch
            {
                Direction.Up => "move_up",
                Direction.Down => "move_down",
                Direction.Left => "move_left",
                Direction.Right => "move_right",
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction,
                    $"Received not supported direction for movement {direction}")
            };
        }

        public static string ToAimControlString(this Direction direction)
        {
            return direction switch
            {
                Direction.Up => "aim_up",
                Direction.Down => "aim_down",
                Direction.Left => "aim_left",
                Direction.Right => "aim_right",
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction,
                    $"Received not supported direction for aiming {direction}")
            };
        }
    }
}