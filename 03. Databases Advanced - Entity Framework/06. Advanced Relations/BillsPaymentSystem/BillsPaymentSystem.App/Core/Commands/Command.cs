namespace BillsPaymentSystem.App.Core.Commands
{
    using Data;
    using Interfaces;

    public abstract class Command : ICommand
    {
        protected readonly BillsPaymentSystemContext Context;

        protected Command(BillsPaymentSystemContext context)
        {
            this.Context = context;
        }

        public abstract string Execute(string[] args);
    }
}
