namespace CustomAutomapper.App.Commands
{
    using System;
    using Interfaces;

    public class ExitCommand : ICommand
    {
        public string ExecuteCommand(string[] args)
        {
            Environment.Exit(1);

            return string.Empty;
        }
    }
}
