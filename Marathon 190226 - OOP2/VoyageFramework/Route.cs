using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoyageFramework
{
    class Route
    {
        private string _name;
        public string Name
        {
            get
            {
                if (_distance < 200)
                {
                    string.Format("{0} - {1} / {2}  KM'lik rota", DepartureLocation, ArrivalLocation, _distance);

                }
                else
                {
                    string.Format("{0} - {1} / {2}  KM'lik {3} molalı rota", DepartureLocation, ArrivalLocation, _distance, _breakCount);
                }
                return _name;
            }
        }
        public string DepartureLocation { get; }
        public string ArrivalLocation { get; }

        private int _duration;
        public int Duration
        {
            get
            {
                return ((_distance * 45) + (_breakCount * 1800) + 59) / 60;
            }
        }
        private decimal _basePrice;
        public decimal BasePrice
        {
            get
            {
                if (_distance < 300)
                {
                    _basePrice = (decimal)(60 + (((int)_distance - 300) / 25) * 4.25);
                }
                else
                {
                    _basePrice = _distance / 25;
                }
                return _basePrice * 5;
            }
        }
        private int _distance;
        public int Distance
        {
            get
            {
                return _distance;
            }
        }
        private int _breakCount;
        public int BreakCount
        {
            get
            {
                return _breakCount;
            }
            set
            {
                if (_distance < 200)
                {
                    _breakCount = 0;
                }
                _breakCount = _distance / 200;
            }
        }
        public Route(string departureLocation, string arrivalLocation, int distance)
        {
            this.DepartureLocation = departureLocation;
            this.ArrivalLocation = arrivalLocation;
            this._distance = distance;
        }
    }
}
