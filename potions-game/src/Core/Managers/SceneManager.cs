using System;
using System.Collections.Generic;
using Godot;
using MonoCustomResourceRegistry;
using PotionsGame.Core.Extensions;
using PotionsGame.Core.Utils;
using PotionsGame.Scenes.Resources;

namespace PotionsGame.Core.Managers
{
    [RegisteredType(nameof(SceneManager))]
    public class SceneManager : SingletonAutoload<SceneManager>
    {
        [Export] private List<ScenePackPair> scenes = default;
        private readonly Dictionary<ScenesDefinitions, PackedScene> scenesDict = new Dictionary<ScenesDefinitions, PackedScene>();
        private readonly Dictionary<ScenesDefinitions, Node2D> scenesInstances = new Dictionary<ScenesDefinitions, Node2D>();
        private Node game;
        private Node2D currentScene;
        
        public string CurrentSceneName => currentScene?.Name;


        private void InitializeScenesDictionary()
        {
            foreach (var scenePackPair in scenes)
            {
                scenesDict.Add(scenePackPair.SceneDefinition, scenePackPair.SceneFile);
            }
        }


        public override void AfterInit()
        {
            game = GetTree().CurrentScene;
            InitializeScenesDictionary();
        }

        public void ChangeScene(ScenesDefinitions defintion)
        {
            Node2D targetScene;
            
            if (!scenesInstances.ContainsKey(defintion))
            {
                targetScene = InstanceScene(defintion);
                game.AddChild(targetScene);

                currentScene?.Disable();
            }
            else
            {
                targetScene = scenesInstances[defintion];
                targetScene.Enable();
            }
            
            currentScene?.Disable();
            currentScene = targetScene;
        }
        
        private Node2D InstanceScene(ScenesDefinitions definition)
        {
            var sceneFile = scenesDict[definition];
            var instance = sceneFile.InstanceOrNull<Node2D>();
            if (instance == null)
            {
                throw new Exception(
                    $"Scene {sceneFile.ResourceName} is not a supported scene type! Must be a {nameof(Node2D)}");
            }
            
            scenesInstances.Add(definition, instance);

            return instance;
        }
    }
}