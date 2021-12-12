using System;

namespace AdventOfCode2021.solutions
{
    public abstract class ProjectDay
    {
        public Action<string, Main.LogLevel> Logfunction;
        
        public void SetLogFunction(Action<string, Main.LogLevel> logFunction)
        {
            Logfunction = logFunction;
        }

        public void Log(string message, Main.LogLevel logLevel = Main.LogLevel.Normal)
        {
            Logfunction(message, logLevel);
        }

        public abstract void Run();
    }
}