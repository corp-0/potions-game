using Godot;
using MonoCustomResourceRegistry;
using PotionsGame.Core.Mapping;

namespace PotionsGame.Scenes.Resources
{
    [RegisteredType(nameof(ScenePackPair), baseType:nameof(Resource))]
    public class ScenePackPair: Resource
    {
        
        [Export] public Maps SceneDefinition;
        [Export] public PackedScene SceneFile;
        
    }
}