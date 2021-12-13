using System.Linq;
using Godot;
using GodotXUnitApi;
using PotionsGame.Core.Extensions;
using PotionsGame.Core.Mapping;
using PotionsGame.Core.Utils;
using PotionsGame.Scenes.Resources;
using Xunit;
using Manager = PotionsGame.Core.Managers.SceneManager;

namespace Tests.Integration.SceneManager
{
    public class TestSceneManager
    {
        [GodotFact(Scene = "res://src/Core/Game.tscn")]
        public void SceneManager_Should_Change_Current_Scene()
        {
            Manager.Instance.TravelToMap(Maps.World2);
            Assert.Equal(Maps.World2, Manager.Instance.CurrentMap);
        }
        
        [GodotFact(Scene = "res://src/Core/Game.tscn")]
        public void SceneManager_Should_Have_Current_Scene_Definition()
        {
            Manager.Instance.TravelToMap(Maps.World);
            Assert.Equal(Maps.World, Manager.Instance.CurrentMap);
        }

        [GodotFact(Scene = "res://src/Core/Game.tscn")]
        public void Amount_Of_Instanced_Maps_Should_Be_Equal_To_All_Maps_Requested()
        {
            Manager.Instance.TravelToMap(Maps.World);
            int mapsCount = Manager.Instance.InstancedMaps.Count;
            Assert.Equal(1, mapsCount);
            
            Manager.Instance.TravelToMap(Maps.World2);
            mapsCount = Manager.Instance.InstancedMaps.Count;
            Assert.Equal(2, mapsCount);
        }
        
        [GodotFact(Scene = "res://src/Core/Game.tscn")]
        public void SceneManager_Should_Only_Have_One_Map_Active_At_Once()
        {
            Manager.Instance.TravelToMap(Maps.World);
            Manager.Instance.TravelToMap(Maps.World2);
            int activeCount = Manager.Instance.InstancedMaps.Values.Count(map => map.IsEnabled);
            Assert.Equal(1, activeCount);
        }
    }
}