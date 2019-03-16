namespace BillsPaymentSystem.App.Core.Commands
{
    using System;
    using Data;

    public class ExitCommand : Command
    {
        public ExitCommand(BillsPaymentSystemContext context) : base(context)
        {
        }

        public override string Execute(string[] args)
        {
            Environment.Exit(1);

            return "";
        }
    }
}
