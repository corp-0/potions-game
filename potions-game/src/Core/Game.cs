using System;
using Godot;
using PotionsGame.Core.Managers;
using PotionsGame.Scenes.Resources;

namespace PotionsGame.Core
{
    public class Game : Node
    {
        [Export] private ScenesDefinitions mainScene = default;

        public override void _Ready()
        {
            SceneManager.Instance.ChangeScene(mainScene);
        }
    }
}
