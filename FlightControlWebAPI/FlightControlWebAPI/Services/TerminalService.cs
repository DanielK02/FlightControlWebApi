using FlightControlWebAPI.DAL;
using FlightControlWebAPI.Models;
using FlightControlWebAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Concurrent;
using System.Data;
using System.Threading;

namespace FlightControlWebAPI.Services
{
    public class TerminalService : ITerminalService
    {
        private readonly DataContext _data;
        private readonly ILoggerService _logger;
        private List<Flight>? flights;
        private static ConcurrentQueue<Flight>? airportQueue = new ConcurrentQueue<Flight>();
        private object _locker = new object();

        public TerminalService(DataContext dataContext, ILoggerService logger)
        {
            _data = dataContext;
            _logger = logger;
            SetFlights();
        }
        // Set flights to memory, neccessary for json to show the Flight object in Terminal
        void SetFlights() => flights = _data.Flights.ToList();

        public async Task<List<Terminal>> GetTerminals()
        {         
            return await _data.Terminals.ToListAsync();            
        }

        public async Task<Terminal> GetTerminalById(int? id)
        {
            
            return await _data.Terminals.FindAsync(id.Value);
            
        }

        public async Task UpdateTerminalAsync(int terminalId, Flight? flight)
        {
            var terminal = await _data.Terminals.FindAsync(terminalId);
            terminal.Flight = flight;
            _data.Terminals.Update(terminal);
            await _data.SaveChangesAsync();
        }

        public async Task UpdateTerminal(Flight? flight, Terminal terminal)
        {
            terminal = await _data.Terminals.FindAsync(terminal.Id);

            if (flight != null)
            {
                terminal.Flight = _data.Flights.FirstOrDefault(f => f.Id == flight.Id);

            }
            else
            {
                terminal.Flight = null;
            }
            _data.Terminals.Update(terminal);
            //Monitor.Enter(_locker);
            await _data.SaveChangesAsync();
            //Monitor.Exit(_locker);
        }

        public async Task ApproachAirport(Flight flight)
        {
            airportQueue.Enqueue(flight);
            //Monitor.Enter(_locker);
            await _data.SaveChangesAsync();
            //Monitor.Exit(_locker);
        }

        public async Task<ConcurrentQueue<Flight>> GetQueue()
        {
            return airportQueue;
        }

        public async Task<Flight> GetFlight()
        {
            Flight flight;
            airportQueue.TryDequeue(out flight);
            return flight;
        }
        //private async Task EnterAirport(Flight flight)
        //{
        //    this.terminals[0].Flight = flight; 
        //    await _data.SaveChangesAsync();
        //}




        //public async Task ApproachAirport(Flight flight)
        //{
        //    if (!airportQueue.IsEmpty)
        //    {
        //        airportQueue.Enqueue(flight);
        //        airportQueue.TryDequeue(out flight);
        //    }
        //    await terminals[0].AcceptFlight(flight);
        //}

        public async Task SaveInbound(Flight? flight, Terminal terminal)
        {
            await UpdateTerminal(terminal.Flight, terminal);
            await _logger.AddLogAsync(terminal.Flight, terminal);
        }


        public async Task SaveOutbound(Flight? flight, Terminal terminal)
        {
            await UpdateTerminal(null, terminal);
            await _logger.UpdateLogAsync2(flight, terminal);
        }





        //_data.Terminals.Attach(terminal);
        //_data.Entry(terminal).State = EntityState.Modified;

        //Use Mutex or Lock on each instance you'd like to keep safe

        //mutex.WaitOne();
        // ALL THE CODE THAT IS WRITTEN HERE IS UNDER MUTEX//
        //mutex.ReleaseMutex();

        //_lockSem.Wait();
        // ALL THE CODE HERE, SAME AS MUTEX
        //_lockSem.Release();

    }

}

