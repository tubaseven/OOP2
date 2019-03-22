using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoyageFramework
{
    public abstract class Bus
    {
        private string _plate;
        public string Make { get; }
        public abstract int Capacity { get; }
        public bool HasToilet { get; }
        public string Plate
        {
            get { return _plate; }
            set { _plate = value; }
        }
        public abstract SeatInformation GetSeatInformation(int seatNumber);
    }
}
