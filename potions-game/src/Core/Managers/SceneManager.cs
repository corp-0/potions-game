using System;
using System.Collections.Generic;
using Godot;
using MonoCustomResourceRegistry;
using PotionsGame.Core.Mapping;
using PotionsGame.Core.Utils;
using PotionsGame.Scenes.Resources;

namespace PotionsGame.Core.Managers
{
    [RegisteredType(nameof(SceneManager))]
    public class SceneManager : SingletonAutoload<SceneManager>
    {
        [Export] private List<ScenePackPair> scenes = default;
        private Node game;
        public Map CurrentMapNode { get; private set; }

        public Maps CurrentMap { get; private set; }

        public Dictionary<Maps, PackedScene> DefinedScenes { get; } = new Dictionary<Maps, PackedScene>();

        public Dictionary<Maps, Map> InstancedMaps { get; } = new Dictionary<Maps, Map>();
        
        private void InitializeScenesDictionary()
        {
            foreach (var scenePackPair in scenes)
            {
                DefinedScenes.Add(scenePackPair.SceneDefinition, scenePackPair.SceneFile);
            }
        }
        
        public override void AfterInit()
        {
            game = GetTree().CurrentScene;
            InitializeScenesDictionary();
        }

        public void TravelToMap(Maps definition)
        {
            Map targetScene;
            
            if (!InstancedMaps.ContainsKey(definition))
            {
                targetScene = InstanceScene(definition);
                game.AddChild(targetScene);

                CurrentMapNode?.Disable();
            }
            else
            {
                targetScene = InstancedMaps[definition];
                targetScene.Enable();
            }
            
            CurrentMapNode?.Disable();
            CurrentMap = definition;
            CurrentMapNode = targetScene;
        }
        
        private Map InstanceScene(Maps definition)
        {
            var sceneFile = DefinedScenes[definition];
            var instance = sceneFile.InstanceOrNull<Map>();
            if (instance == null)
            {
                throw new Exception(
                    $"Scene {sceneFile.ResourceName} is not a supported scene type! Must be a {nameof(Map)}");
            }
            
            InstancedMaps.Add(definition, instance);

            return instance;
        }
    }
}