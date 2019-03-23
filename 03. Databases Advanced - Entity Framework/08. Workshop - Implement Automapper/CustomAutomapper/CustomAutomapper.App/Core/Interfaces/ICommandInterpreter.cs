namespace CustomAutomapper.App.Core.Interfaces
{
    using Microsoft.EntityFrameworkCore;

    public interface ICommandInterpreter
    {
        string Read(string[] args);
    }
}
