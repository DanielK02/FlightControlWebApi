using FlightControlWebAPI.DAL;
using FlightControlWebAPI.Models;
using FlightControlWebAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FlightControlWebAPI.Services
{
    public class LoggerService : ILoggerService
    {
        private readonly DataContext _dataContext;
        public LoggerService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Logger> GetLogAsync(Flight flight, Terminal terminal)
        {
            return await _dataContext.Logs.FirstOrDefaultAsync(t => t.Flight.FlightName == flight.FlightName && t.Terminal.Number == terminal.Number);
        }
        public async Task<Logger> AddLogAsync(Flight flight, Terminal? terminal)
        {
            var log = new Logger
            {
                Flight = flight,
                Terminal = terminal,
                Inbound = DateTime.Now
            };
            //Monitor.Enter(log);
            await _dataContext.AddAsync(log);
            
            await _dataContext.SaveChangesAsync();
            //Monitor.Exit(log);
            return log;
        }
        public async Task UpdateLogAsync(Logger log)
        {

            log.Outbound = DateTime.Now;
            await _dataContext.SaveChangesAsync();
        }       
        public async Task UpdateLogAsync2(Flight flight, Terminal? terminal)
        {
            var log = await GetLogAsync(flight, terminal);
            if (log != null)
            {
                log.Outbound = DateTime.Now;
                //Monitor.Enter(log);
                await _dataContext.SaveChangesAsync();
                //Monitor.Exit(log);
            }
            
        }
    }
}
