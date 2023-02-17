using FlightControlWebAPI.Models;
using FlightControlWebAPI.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Concurrent;

namespace FlightControlWebAPI.Services
{
    public class ControlBackgroundService : BackgroundService // IHostedService
    {
        // Save Terminals to memory
        private List<Terminal> terminals;
        // Flight object to get a new flight from AirportQueue in TerminalService every time Terminal 1 is empty.
        private Flight flight = new Flight();
        private ITerminalService terminalService;
        private ILoggerService loggerService;
        public ControlBackgroundService(IServiceProvider services)
        {
            Services = services;
        }
        public IServiceProvider Services { get; }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (var scope = Services.CreateScope())
            {
                // Use Scoped services inside ControlBackgroundService
                terminalService =
                        scope.ServiceProvider.GetRequiredService<ITerminalService>();
                loggerService =
                        scope.ServiceProvider.GetRequiredService<ILoggerService>();

                terminals = await terminalService.GetTerminals();

                foreach (var terminal in terminals)
                {
                    // Sets the NextTerminal object by the NextTerminalId decided in the database. //

                    terminal.NextTerminal = terminals.FirstOrDefault(t => t.Number == terminal.NextTerminalId);

                    terminal.IsFree = true;

                    ThreadStart threadStart = new ThreadStart(() => TerminalCheck(terminal));
                    Thread thread = new Thread(threadStart);
                    thread.Start();
                }

                ///
                /// Neccessary so program won't dispose of objects
                ///
                while (!stoppingToken.IsCancellationRequested)
                {

                    await Task.Delay(1000);
                    // loop to check queue of flights waiting to enter the airport
                    await CheckAirportQueue();
                }
            }
        }


        private void TerminalCheck(Terminal terminal)
        {
            while (true)
            {
                Thread.Sleep(1000);
                if (terminal.Flight != null)
                {
                    if (terminal.Number == 5)
                    {
                        terminal.Flight.IsDeparted = true;
                    }

                    if (terminal.Flight.IsDeparted == true && terminal.Number == 4)
                    {
                        lock (terminal)
                        {
                            Console.WriteLine($"$$$$$ Flight {terminal.Flight.FlightName} waiting in {terminal.Number} --{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}-- PREPARING TO TAKE OFF");

                            //Task.Run(async () => await terminalService.UpdateTerminal(terminal.Flight, terminal));
                            //Task.Run(async () => await loggerService.AddLogAsync(terminal.Flight, terminal));

                            Thread.Sleep((int)terminal.WaitSeconds * 1000);
                            ExitAirport(terminal);
                        }
                    }
                    else
                    {
                        lock (terminal)
                        {
                            Console.WriteLine($"$$$$$ Flight {terminal.Flight.FlightName} waiting in {terminal.Number} --{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}--");
                            // LOG ENTRANCE, BUT ONCE
                            Thread.Sleep((int)terminal.WaitSeconds * 1000);
                            MoveToNextTerminal(terminal);
                        }
                    }
                }
            }
        }

        private void ExitAirport(Terminal terminal)
        {
            if (terminal.Flight != null && terminal.IsFree == true)
            {
                terminal.IsFree = false;

                Console.WriteLine($"{terminal.Flight.FlightName} HAS LEFT THE AIRPORT --{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}--");

                Monitor.Enter(terminal);
                Task.Run(async () => await terminalService.SaveOutbound(terminal.Flight, terminal)).Wait();
                Monitor.Exit(terminal);
                terminal.Flight = null;
                terminal.IsFree = true;
            }
        }

        private void MoveToNextTerminal(Terminal terminal)
        {
            // Adds wait time to give priority to Terminal 8 (Located in terminals[7]) to avoid everything being stuck.
            // Meaning when terminal 8 attempts to go to terminal 4 but 3 will wait longer

            // If terminal 8 is busy, don't enter terminal 4 to prevent from a block in the airport
            //if (terminal.Number == 3 && terminals[7].Flight != null)
            //{
            //    return;
            //}

            // If flights are busy in terminals 5, 6 and 7, don't enter terminal 4 as it can block
            if (terminal.Number == 3 && (terminals[5].Flight != null || terminals[7].Flight != null) && terminals[4].Flight != null)
            {
                return;
            }

            if (terminal.NextTerminal.Flight == null && terminal.Flight != null)
            {

                //Flight flight = new Flight();
                //flight = terminal.Flight;
                terminal.NextTerminal.Flight = terminal.Flight;
                terminal.Flight = null;
                Monitor.Enter(terminal);
                try
                {
                    Task.Run(async () => await terminalService.SaveOutbound(terminal.NextTerminal.Flight, terminal)).Wait();

                    Task.Run(async () => await terminalService.SaveInbound(terminal.NextTerminal.Flight, terminal.NextTerminal)).Wait();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message.ToString());
                }
                finally
                {
                    Monitor.Exit(terminal);
                }

                Console.WriteLine($"{terminal.NextTerminal.Flight.FlightName} has left terminal {terminal.Number} to terminal {terminal.NextTerminal.Number} --{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}--");

            }
            // Go to Terminal 7 in case 6 is busy
            //else if(terminal.Number == 5 && terminals[6].Flight == null)
            //{
            //    terminals[6].Flight = terminal.Flight;
            //    terminal.Flight = null;
            //}
        }

        private async Task CheckAirportQueue()
        {
            await Task.Delay(2000);

            // Gets the latest flight in queue and enter terminal 1
            if (terminals[0].Flight == null)
            {
                flight = await terminalService.GetFlight();
                if (flight != null)
                {
                    Console.WriteLine($"Terminal 1 got flight {flight.FlightName} from queue --{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}--");
                    terminals[0].Flight = flight;
                    //Monitor.Enter(terminals[0]);
                    await terminalService.SaveInbound(terminals[0].Flight, terminals[0]);
                    //Monitor.Exit(terminals[0]);
                }
            }
        }
        private async void UpdateInbound(Flight flight, Terminal terminal)
        {
            await terminalService.UpdateTerminal(terminal.Flight, terminal);
            await loggerService.AddLogAsync(terminal.Flight, terminal);
        }

        private async void UpdateOutbound(Flight flight, Terminal terminal)
        {
            await terminalService.UpdateTerminal(null, terminal);
            await loggerService.UpdateLogAsync2(terminal.Flight, terminal);
        }




    }
}
