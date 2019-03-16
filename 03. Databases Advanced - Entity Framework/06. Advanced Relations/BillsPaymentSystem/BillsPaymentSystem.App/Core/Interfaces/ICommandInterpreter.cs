namespace BillsPaymentSystem.App.Core.Interfaces
{
    using Data;

    public interface ICommandInterpreter
    {
        string Read(string[] args, BillsPaymentSystemContext context);
    }
}
