using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoyageFramework
{
    class LuxuryBus : Bus
    {
        public override int Capacity
        {
            get { return 20; }
        }
        public bool HasToilet
        {
            get { return true; }
        }
        public override SeatInformation GetSeatInformation(int seatNumber)
        {
            SeatSection section = new SeatSection();
            SeatCategory category = new SeatCategory();

            if (seatNumber % 2 == 1)
            {
                section = SeatSection.LeftSide;
            }
            else
            {
                section = SeatSection.RightSide;
            }
            category = SeatCategory.Singular;
            SeatInformation selection = new SeatInformation(seatNumber, section, category);
            return selection;
        }
    }
}
    