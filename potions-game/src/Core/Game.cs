using System;
using Godot;
using PotionsGame.Core.Extensions;
using PotionsGame.Core.Managers;
using PotionsGame.Scenes.Resources;

namespace PotionsGame.Core
{
    public class Game : Node
    {
        [Export] private ScenesDefinitions mainScene = default;
        private Label sceneLabel;

        public override void _Ready()
        {
            sceneLabel = this.GetNodeByTypeOrNull<Label>();
            SceneManager.Instance.ChangeScene(mainScene);
        }

        public override void _Input(InputEvent @event)
        {
            var changeTo = SceneManager.Instance.CurrentSceneName == "World"
                ? ScenesDefinitions.World2
                : ScenesDefinitions.World;
            if (Input.IsActionJustPressed("ui_accept"))
            {
                SceneManager.Instance.ChangeScene(changeTo);
            }
        }

        public override void _Process(float delta)
        {
            sceneLabel.Text = $"Current scene: {SceneManager.Instance.CurrentSceneName}";
        }
    }
}
