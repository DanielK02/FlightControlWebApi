using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoModelsLibrary
{
    public class TerminalDto
    {
        public int Id { get; set; }

        public int Number { get; set; }

        public double WaitSeconds { get; set; }

        public FlightDto? Flight { get; set; }
    }
}
