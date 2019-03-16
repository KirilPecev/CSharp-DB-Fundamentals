namespace BillsPaymentSystem.App.Core.IO.Interfaces
{
    public interface IWriter
    {
        void WriteLine(string contents);

        void Write(string contents);
    }
}
