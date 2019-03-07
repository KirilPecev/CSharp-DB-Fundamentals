namespace P01_HospitalDatabase.Core
{
    using AppStructure.Models;
    using System;
    using System.Linq;

    public class Engine
    {
        public void Run()
        {
            MyConsole console = new MyConsole(ConsoleColor.Blue, ConsoleColor.White, "Hospital");
            Menu menu = new Menu();

            Produce(menu);
        }

        public void Produce(Menu menu)
        {
            menu.PrintMenu();

            int command = int.Parse(Console.ReadLine());

            Type type = menu.GetType();
            object classInstance = Activator.CreateInstance(type);

            var method = type.GetMethods().Where(x => x.Name.Contains(command.ToString())).FirstOrDefault();
            if (method != null)
            {
                method.Invoke(classInstance, null);
            }

            Type commandInterpreterType = typeof(CommandInterpreter);
            object commandInterpreterInstance = Activator.CreateInstance(commandInterpreterType);

            var commandMethod = commandInterpreterType.GetMethods().Where(x => x.Name.Contains(command.ToString())).FirstOrDefault();
            if (method != null)
            {
               string result = commandMethod.Invoke(commandInterpreterInstance, null).ToString();
               Console.WriteLine(result);
            }
        }
    }
}
