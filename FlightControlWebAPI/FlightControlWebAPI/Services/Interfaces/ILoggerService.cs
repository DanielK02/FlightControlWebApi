using FlightControlWebAPI.Models;

namespace FlightControlWebAPI.Services.Interfaces
{
    public interface ILoggerService
    {
        Task<Logger> AddLogAsync(Flight flight, Terminal? terminal);
        //Task<Logger> LogEntry(int id, int terminalNumber, DateTime now);
        //Task LogExit(int id, int terminalNumber, DateTime now);
        Task UpdateLogAsync(Logger log);

        Task UpdateLogAsync2(Flight flight, Terminal? terminal);

        //public Task<Logger> GetLogAsync(Flight flight);

    }
}