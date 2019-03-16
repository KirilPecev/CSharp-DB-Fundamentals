namespace BillsPaymentSystem.App.Core
{
    using Data;
    using Interfaces;
    using IO.Interfaces;
    using System;
    using System.Linq;

    public class Engine : IEngine
    {
        private readonly ICommandInterpreter _commandInterpreter;
        private readonly IReader _reader;
        private readonly IWriter _writer;

        public Engine(ICommandInterpreter commandInterpreter, IReader reader, IWriter writer)
        {
            this._commandInterpreter = commandInterpreter;
            this._reader = reader;
            this._writer = writer;
        }

        public void Run()
        {
            while (true)
            {
                string[] inputParams = this._reader.ReadLine()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                try
                {
                    var result = "";
                    using (BillsPaymentSystemContext context = new BillsPaymentSystemContext())
                    {
                        result = this._commandInterpreter.Read(inputParams, context);
                    }

                    if (!(string.IsNullOrEmpty(result) || string.IsNullOrWhiteSpace(result)))
                    {
                        this._writer.WriteLine(result);
                    }
                }
                catch (ArgumentNullException ane)
                {
                    this._writer.WriteLine(ane.Message);
                }
                catch (ArgumentException argumentException)
                {
                    this._writer.WriteLine(argumentException.Message);
                }
                catch (Exception e)
                {

                }
            }
        }
    }
}
