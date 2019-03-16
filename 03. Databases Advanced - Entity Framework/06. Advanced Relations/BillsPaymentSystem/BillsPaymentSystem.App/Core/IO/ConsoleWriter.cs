namespace BillsPaymentSystem.App.Core.IO
{
    using System;
    using Interfaces;

    public class ConsoleWriter : IWriter
    {
        public void WriteLine(string contents)
        {
            Console.WriteLine(contents);
        }

        public void Write(string contents)
        {
            Console.Write(contents);
        }
    }
}
