using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoModelsLibrary
{
    public class FlightDto
    {
        public int Id { get; set; }
        public string FlightName { get; set; }
        public string SerialNumber { get; set; }
        public BrandTypeDto Brand { get; set; }
    }
}
