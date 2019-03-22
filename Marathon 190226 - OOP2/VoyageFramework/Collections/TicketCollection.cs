using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoyageFramework.Collections
{
    class TicketCollection
    {
        public int Length
        {
            get
            {
                return _ticket.Length;
            }
        }

        public Ticket this[int index]
        {
            get
            {
                return _ticket[index];
            }
        }

        private Ticket[] _ticket;

        public void AddTicket(Ticket ticket)
        {
            if (_ticket == null)
            {
                _ticket = new Ticket[1];
                _ticket[0] = ticket;
            }
            else
            {
                Array.Resize(ref _ticket, _ticket.Length + 1);
                _ticket[_ticket.Length - 1] = ticket;
            }
        }

        public void AddDoubleTicket(Ticket ticket01, Ticket ticket02)
        {
            if (_ticket == null)
            {
                _ticket = new Ticket[2];
                _ticket[0] = ticket01;
                _ticket[1] = ticket02;
            }
            else
            {
                Array.Resize(ref _ticket, _ticket.Length + 2);
                _ticket[_ticket.Length - 2] = ticket01;
                _ticket[_ticket.Length - 1] = ticket02;
            }
        }

        private int IndexOfTicket(Ticket ticket)
        {
            for (int i = 0; i < _ticket.Length; i++)
            {
                if (_ticket[i].SeatInformation.Number == ticket.SeatInformation.Number)
                {
                    return i;
                }
            }
            return -1;
        }

        public void RemoveTicket(Ticket ticket)
        {
            RemoveTicketAt(IndexOfTicket(ticket));
        }

        private void RemoveTicketAt(int index)
        {
            for (int i = index; i < _ticket.Length - 1; i++)
            {
                _ticket[i] = _ticket[i + 1];
            }
            Array.Resize(ref _ticket, _ticket.Length - 1);
        }
    }
}