using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoyageFramework
{
    public class StandardBus : Bus
    {
        public override int Capacity
        {
            get { return 30; }
        }
        public bool HasToilet
        {
            get { return false; }
        }
        public override SeatInformation GetSeatInformation(int seatNumber)
        {
            SeatSection section = new SeatSection();
            SeatCategory category = new SeatCategory();

            if (seatNumber % 3 == 1)
            {
                section = SeatSection.LeftSide;
                category = SeatCategory.Singular;
            }
            else if (seatNumber % 3 == 2)
            {
                section = SeatSection.RightSide;
                category = SeatCategory.Corridor;
            }
            else
            {
                section = SeatSection.RightSide;
                category = SeatCategory.Window;
            }
            SeatInformation selection = new SeatInformation(seatNumber, section, category);
            return selection;
        }
    }
}
    
