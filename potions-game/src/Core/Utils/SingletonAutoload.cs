using Godot;

namespace PotionsGame.Core.Utils
{
    public class SingletonAutoload<T> : Node where T : Node
    {
        private static T instance;
        public static T Instance => instance;

        public override void _Ready()
        {
            if (instance != null)
            {
                Logger.Instance.Error(instance.Name, "was already instantiated!");
                return;
            }
            
            instance = this as T;
            Logger.Instance.Debug(instance?.Name, "is instantiated and ready to use!");
            AfterInit();
        }

        public virtual void AfterInit()
        {
        }
    }
}