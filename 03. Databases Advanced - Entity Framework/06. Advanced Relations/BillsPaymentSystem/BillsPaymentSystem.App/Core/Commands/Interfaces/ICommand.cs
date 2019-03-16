namespace BillsPaymentSystem.App.Core.Commands.Interfaces
{
    public interface ICommand
    {
        string Execute(string[] args);
    }
}
