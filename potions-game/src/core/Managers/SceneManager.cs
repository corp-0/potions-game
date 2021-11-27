using System;
using System.Collections.Generic;
using Godot;
using MonoCustomResourceRegistry;
using PotionsGame.Core.Utils;
using PotionsGame.Scenes.Resources;

namespace PotionsGame.Core.Managers
{
    [RegisteredType(nameof(SceneManager))]
    public class SceneManager : SingletonAutoload<SceneManager>
    {
        [Export] private List<ScenePackPair> scenes = default;
        private readonly Dictionary<ScenesDefinitions, PackedScene> scenesDict = new();
        private Node game;
        private Node2D currentScene;


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

        public void ChangeScene(ScenesDefinitions sceneDefinitions)
        {
            try
            {

                var sceneFile = scenesDict[sceneDefinitions];
                if (sceneFile.Instance<Node2D>() is not { } sceneInstance)
                {
                    throw new Exception(
                        $"Scene {sceneFile.ResourceName} is not a supported scene type! Must be a {nameof(Node2D)}");
                }

                game.AddChild(sceneInstance);
                currentScene = sceneInstance;
                // sceneInstance.Connect("finished", this, nameof(OnSceneFinished));
            }
            catch (KeyNotFoundException)
            {
                GD.PrintErr("Given scene is not registered! Did you forget to add it to the scenes list?");
            }
        }
    }
}