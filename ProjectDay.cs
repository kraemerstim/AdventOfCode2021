using System;

namespace AdventOfCode2021
{
    public abstract class ProjectDay
    {
        private Action<string, Main.LogLevel> _logfunction;
        
        public void SetLogFunction(Action<string, Main.LogLevel> logFunction)
        {
            _logfunction = logFunction;
        }

        public void Log(string message, Main.LogLevel logLevel = Main.LogLevel.Normal)
        {
            _logfunction(message, logLevel);
        }

        public abstract void Run();
    }
}