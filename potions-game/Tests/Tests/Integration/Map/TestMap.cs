using System;
using Godot;
using GodotXUnitApi;
using Xunit;
using MapNode = PotionsGame.Core.Mapping.Map;

namespace Tests.Integration.Map
{
    public class TestMap
    {
        [GodotFact]
        public void Map_With_Proper_Structure_Should_Load_Fine()
        {
            var scene = GDU.CurrentScene;
            var map = new MapNode();
            var ground = new TileMap();
            ground.Name = "Ground";
            var objects = new TileMap();
            objects.Name = "Objects";
            map.AddChild(ground);
            map.AddChild(objects);
            scene.AddChild(map);

            var exception = Record.Exception(() => scene.AddChild(map));
            Assert.Null(exception);
        }
    }
}