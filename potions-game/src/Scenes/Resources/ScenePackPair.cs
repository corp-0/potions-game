using Godot;
using MonoCustomResourceRegistry;

namespace PotionsGame.Scenes.Resources
{
    [RegisteredType(nameof(ScenePackPair), baseType:nameof(Resource))]
    public class ScenePackPair: Resource
    {
        
        [Export] public ScenesDefinitions SceneDefinition;
        [Export] public PackedScene SceneFile;
        
    }
}