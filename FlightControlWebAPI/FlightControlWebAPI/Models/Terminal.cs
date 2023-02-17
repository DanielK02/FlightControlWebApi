using FlightControlWebAPI.Services.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Timers;

namespace FlightControlWebAPI.Models
{
    public class Terminal
    {
        public int Id { get; set; }
        [Required]
        public int Number { get; set; }
        [Required]
        public double WaitSeconds { get; set; }
        public virtual Flight? Flight { get; set; }
        public int NextTerminalId { get; set; }
        [NotMapped]
        public Terminal NextTerminal { get; set; }
        [NotMapped]
        public SemaphoreSlim Semaphore = new SemaphoreSlim(1, 1);
        [NotMapped]
        public bool IsFree { get; set; }
        //[NotMapped]
        //public Task TerminalTask { get; set; }
        //[NotMapped]
        //public Action TerminalAction { get; set; }

        //public async Task CheckTerminal()
        //{
        //    while (true)
        //    {
        //        await Task.Delay(500);
        //        if (Flight != null)
        //        {
        //            await Semaphore.WaitAsync();
        //            Console.WriteLine($"{Flight.FlightName} is waiting in {this.Number}");
        //            await Task.Delay((int)WaitSeconds * 1000);
        //            NextTerminal.Flight = this.Flight;
        //            Console.WriteLine($"{Flight.FlightName} left {this.Number}");
        //            this.Flight = null;
        //            Semaphore.Release();
        //        }
        //    }

        //}

        //public void Run()
        //{
        //    Console.WriteLine($"Hello from terminal {Number}");
        //    Task.Delay(5000).Wait();
        //}

    }
}
