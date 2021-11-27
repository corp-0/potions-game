using System;
using Godot;
using MonoCustomResourceRegistry;

namespace PotionsGame.Core.Utils
{
    [RegisteredType(nameof(Logger))]
    public class Logger: SingletonAutoload<Logger>
    {
        [Export] private Level currentLevel = Level.Info;
        
        private const string LOG_FORMAT = "{0} | {1} | {2}: {3}"; // {0} = DateTime, {1} = LogLevel, {2} = Context, {3} = Message

        public enum Level
        {
            Trace,
            Debug,
            Info,
            Warn,
            Error,
            Fatal
        }

        private static void Log(Level level, object context, string message)
        {
            if (level < Instance.currentLevel)
            {
                return;
            }
            
            var entry = string.Format(LOG_FORMAT, DateTime.Now, level.ToString().ToUpper(), context, message);
            
            if (level <= Level.Warn)
            {
                GD.Print(entry);
                return;
            }
            
            GD.PrintErr(entry);
        }
        
        public void Trace(object context, string message)
        {
            Log(Level.Trace, context, message);
        }
        
        public void Debug(object context, string message)
        {
            Log(Level.Debug, context, message);
        }
        
        public void Info(object context, string message)
        {
            Log(Level.Info, context, message);
        }
        
        public void Warn(object context, string message)
        {
            Log(Level.Warn, context, message);
        }
        
        public void Error(object context, string message)
        {
            Log(Level.Error, context, message);
        }
        
        public void Fatal(object context, string message)
        {
            Log(Level.Fatal, context, message);
        }
        
    }
}