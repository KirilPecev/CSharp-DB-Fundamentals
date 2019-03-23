namespace CustomAutomapper.App.Core
{
    using Interfaces;
    using Microsoft.Extensions.DependencyInjection;
    using System;

    public class Engine : IEngine
    {
        private readonly IServiceProvider _provider;

        public Engine(IServiceProvider provider)
        {
            this._provider = provider;
        }

        public void Run()
        {
            while (true)
            {
                string[] commandArgs = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);

                ICommandInterpreter commandInterpreter = this._provider.GetService<ICommandInterpreter>();

                string result = commandInterpreter.Read(commandArgs);

                if (!(string.IsNullOrEmpty(result) || string.IsNullOrWhiteSpace(result)))
                {
                    Console.WriteLine(result);
                }
            }
        }
    }
}
