namespace BillsPaymentSystem.App.Core
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Commands.Interfaces;
    using Data;
    using Interfaces;

    public class CommandInterpreter : ICommandInterpreter
    {
        private const string Suffix = "Command";

        public string Read(string[] args, BillsPaymentSystemContext context)
        {
            string command = args[0];
            string[] commandAgrs = args.Skip(1).ToArray();

            var type = Assembly.GetCallingAssembly()
                .GetTypes()
                .FirstOrDefault(e => e.Name == command + Suffix);

            if (type == null)
            {
                throw new ArgumentNullException($"{command}", "Not found!");
            }

            var instance = (ICommand)Activator.CreateInstance(type, context);

            string result = instance.Execute(commandAgrs);

            return result;
        }
    }
}
