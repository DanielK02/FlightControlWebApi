using FlightControlWebAPI.Models;
using System.Collections.Concurrent;

namespace FlightControlWebAPI.Services.Interfaces
{
    public interface ITerminalService
    {
        //public ConcurrentQueue<Flight> airportQueue { get; set; }
        public Task<List<Terminal>> GetTerminals();
        public Task<Terminal> GetTerminalById(int? id);
        public Task UpdateTerminal(Flight? flight, Terminal terminal);
        public Task UpdateTerminalAsync(int terminalId, Flight flight);

        public Task ApproachAirport(Flight flight);
        public Task<Flight> GetFlight();
        public Task SaveInbound(Flight? flight, Terminal terminal);

        public Task SaveOutbound(Flight? flight, Terminal terminal);
    }
}
