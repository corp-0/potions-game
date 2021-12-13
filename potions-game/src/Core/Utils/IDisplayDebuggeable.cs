namespace PotionsGame.Core.Utils
{
    public interface IDisplayDebuggeable
    {
        public bool DisplayDebug { get; set; }
        void DisplayDebugInfo();
    }
}