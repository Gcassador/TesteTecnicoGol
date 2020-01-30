using System;
using System.Collections.Generic;
using System.Text;

namespace TesteTecnico.Domain.Entities
{
    public class Airplane : BaseEntity
    {
        public Airplane()
        {

        }

        public Airplane(string model, int numberOfPassangers, DateTime createdDate, int? id)
        {
            if (id != null)
                Id = id??0;
            Model = model;
            NumberOfPassengers = numberOfPassangers;
            CreatedDate = createdDate;
        }
        public string Model { get; set; }
        public int NumberOfPassengers { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
