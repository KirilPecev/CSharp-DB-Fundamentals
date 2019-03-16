namespace BillsPaymentSystem.App
{
    using Core;
    using Core.Interfaces;
    using Core.IO;
    using Core.IO.Interfaces;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            ICommandInterpreter commandInterpreter = new CommandInterpreter();
            IReader reader = new ConsoleReader();
            IWriter writer = new ConsoleWriter();

            Engine engine = new Engine(commandInterpreter, reader, writer);
            engine.Run();
        }
    }
}
