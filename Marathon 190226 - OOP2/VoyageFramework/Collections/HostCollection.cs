using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoyageFramework.Collections
{
    class HostCollection
    {
        public int Length { get { return _host.Length; } }

        private Host[] _host;
        public Host this[int index] { get { return _host[index]; } }

        public void AddHost(Host host)
        {
            if (_host == null)
            {
                _host = new Host[1];
                _host[0] = host;
            }
            else
            {
                Array.Resize(ref _host, _host.Length + 1);
                _host[_host.Length - 1] = host;
            }
        }

        public void RemoveHost(Host host)
        {
            RemoveHostAt(IndexOfHost(host));
        }

        private int IndexOfHost(Host host)
        {
            for (int i = 0; i < _host.Length; i++)
            {
                if (_host[i].IdentityNumber == host.IdentityNumber) return i;
            }
            return -1;
        }

        private void RemoveHostAt(int ındexOfHost)
        {
            for (int i = ındexOfHost; i < _host.Length - 1; i++)
            {
                _host[i] = _host[i + 1];
            }
            Array.Resize(ref _host, _host.Length - 1);
        }
    }
}