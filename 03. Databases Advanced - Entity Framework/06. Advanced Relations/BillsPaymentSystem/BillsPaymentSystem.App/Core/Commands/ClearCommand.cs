namespace BillsPaymentSystem.App.Core.Commands
{
    using Data;
    using System;

    public class ClearCommand : Command
    {
        public ClearCommand(BillsPaymentSystemContext context) : base(context)
        {
        }

        public override string Execute(string[] args)
        {
            Console.Clear();
            return "";
        }
    }
}
