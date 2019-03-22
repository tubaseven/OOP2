using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoyageFramework.Collections
{
    class DriverCollection
    {
        private Driver[] _drivers;
        public int Length
        {
            get
            {
                return _drivers.Length;
            }
        }

        public Driver this[int index]
        {
            get
            {
                return _drivers[index];
            }
        }

        public void AddDriver(Driver driver)
        {
            if (_drivers == null)
            {
                _drivers = new Driver[1];
                _drivers[0] = driver;
            }
            else
            {
                Array.Resize(ref _drivers, _drivers.Length + 1);
                _drivers[_drivers.Length - 1] = driver;
            }
        }

        private int IndexOfDriver(Driver driver)
        {
            for (int i = 0; i < _drivers.Length; i++)
            {
                if (_drivers[i].IdentityNumber == driver.IdentityNumber)
                {
                    return i;
                }
            }
            return -1;
        }

        private void RemoveDriverAt(int index)
        {
            for (int i = index; i < _drivers.Length - 1; i++)
            {
                _drivers[i] = _drivers[i + 1];
            }
            Array.Resize(ref _drivers, _drivers.Length - 1);
        }

        public void RemoveDriver(Driver driver)
        {
            RemoveDriverAt(IndexOfDriver(driver));
        }
    }
}


