using System;
using Godot;
using MonoCustomResourceRegistry;
using PotionsGame.Core.Utils;

namespace PotionsGame.Core.Mapping
{
    /// <summary>
    /// Represents a map/level in the game. It has some validations to make sure the Node hierarchy is correct.
    /// </summary>
    [RegisteredType(nameof(Map), iconPath  = "res://assets/Textures/Types Icons/map.png",baseType = nameof(Node2D))]
    public class Map: Node2D, IDisableable
    {
        public TileMap Ground { get; private set; }

        public TileMap Objects { get; private set; }

        public TileMap Walls { get; private set; }

        private void ValidateMap()
        {
            if (Ground is null)
            {
                throw new Exception("Map must have a ground layer.");
            }
            
            if (Objects is null)
            {
                throw new Exception("Map must have an objects layer.");
            }
            
            if (Walls is null)
            {
                throw new Exception("Map must have a walls layer.");
            }
        }
        
        public override void _Ready()
        {
            Ground = GetNodeOrNull<TileMap>("Ground");
            Objects = GetNodeOrNull<TileMap>("Objects");
            Walls = GetNodeOrNull<TileMap>("Walls");
            
            ValidateMap();
        }

        public void AddObject(Node node)
        {
            Objects.AddChild(node);
        }

        public void AddGround(Node node)
        {
            Ground.AddChild(node);
        }

        public void Disable()
        {
            SetProcess(false);
            SetProcessInput(false);
            SetPhysicsProcess(false);

            foreach (var child in GetChildren())
            {
                if (child is IDisableable disableable)
                {
                    disableable.Disable();
                }
            }
            
            Visible = false;
        }

        public void Enable()
        {
            SetProcess(true);
            SetProcessInput(true);
            SetPhysicsProcess(true);

            foreach (var child in GetChildren())
            {
                if (child is IDisableable disableable)
                {
                    disableable.Enable();
                }
            }
            
            Visible = true;
        }

        public bool IsEnabled => IsProcessing() && IsProcessingInput() && IsPhysicsProcessing();
    }
}