using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TesteTecnico.Application.ViewModels
{
    public class AirplaneViewModel
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public int NumberOfPassenger { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
