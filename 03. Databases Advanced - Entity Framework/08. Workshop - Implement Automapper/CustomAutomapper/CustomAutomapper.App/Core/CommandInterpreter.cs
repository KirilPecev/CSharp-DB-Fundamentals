namespace CustomAutomapper.App.Core
{
    using Commands.Interfaces;
    using Interfaces;
    using System;
    using System.Linq;
    using System.Reflection;

    public class CommandInterpreter : ICommandInterpreter
    {
        private readonly IServiceProvider _provider;
        private readonly string Suffix = "Command";

        public CommandInterpreter(IServiceProvider provider)
        {
            this._provider = provider;
        }

        public string Read(string[] args)
        {
            string command = args[0] + Suffix;
            string[] commandArgs = args.Skip(1).ToArray();

            Type type = Assembly.GetCallingAssembly()
                .GetTypes()
                .FirstOrDefault(t => t.Name == command);

            if (type == null)
            {
                throw new ArgumentException(ExceptionMessages.NullCommandException);
            }

            ConstructorInfo constructor = type.GetConstructors()
                .FirstOrDefault();

            if (constructor == null)
            {
                throw new ArgumentException(ExceptionMessages.NullConstructorException);
            }

            Type[] constructorParams = constructor
                .GetParameters()
                .Select(p => p.ParameterType)
                .ToArray();

            object[] services = constructorParams
                .Select(this._provider.GetService)
                .ToArray();

            ICommand commandInstance = (ICommand)Activator.CreateInstance(type, services);

            string result = commandInstance.ExecuteCommand(commandArgs);

            return result;
        }
    }
}
