using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoyageFramework.Collections;

namespace VoyageFramework
{
    class BusExpedition
    {
        DriverCollection driverCollection = new DriverCollection();
        HostCollection hostCollection = new HostCollection();
        TicketCollection ticketCollection = new TicketCollection();

        Random rnd = new Random();

        private Bus _bus;
        public Bus Bus
        {
            get
            {
                return _bus;
            }
            set
            {
                for (int i = 0; i < driverCollection.Length; i++)
                {
                    if (value is LuxuryBus && driverCollection[i].LicenseType != LicenseType.HighLicense)
                    {
                        throw new Exception("Bu otobüsü kullanmaya yetkiniz yoktur");
                    }
                    else if (value is StandardBus && driverCollection[i].LicenseType != LicenseType.None)
                    {
                        throw new Exception("Bu otobüsü kullanmaya yetkiniz yoktur");
                    }
                    else
                    {
                        value = _bus;
                    }
                }
            }
        }
        public Route Route { get; }
        public DateTime DepartureTime { get; }

        public string Code
        {
            get
            {
                rnd.Next(1000, 9999);
                if (Bus.HasToilet == true)
                {
                    return string.Format("{0}{1}-LX-{2}", Route.DepartureLocation.First(), DepartureTime.ToString("yyMMdd"), rnd.ToString());
                }
                else
                {
                    return string.Format("{0}{1}-ST-{2}", Route.DepartureLocation.First(), DepartureTime.ToString("yyMMdd"), rnd.ToString());
                }
            }
        }
        private DateTime _estimatedDepartureTime;
        public DateTime EstimatedDepartureTime
        {
            get
            {
                if (DepartureTime != null)
                {
                    return DepartureTime;
                }
                else
                {
                    return _estimatedDepartureTime;
                }
            }
            set
            {
                _estimatedDepartureTime = value;
            }
        }

        public DateTime EstimatedArrivalTime 
        {
            get
            {
                TimeSpan duration = TimeSpan.Parse(Route.Duration.ToString());
                if (HasDelay == true)
                {
                    return _estimatedDepartureTime + duration;
                }
                return DepartureTime + duration;
            }
        }
        public bool HasDelay
        {
            get
            {
                if (DepartureTime != EstimatedDepartureTime)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public bool HasSnackService { get; set; }

        public void AddDriver(Driver driver)
        {
            if (driverCollection.Length < (this.Route.Distance + 399) / 400)
            {
                if (Bus != null)
                {
                    if (Bus is StandardBus && driver.LicenseType != LicenseType.None)
                    {
                        driverCollection.AddDriver(driver);
                    }
                    else if (Bus is LuxuryBus && driver.LicenseType == LicenseType.HighLicense)
                    {
                        driverCollection.AddDriver(driver);
                    }
                    else
                    {
                        throw new Exception("Bu koşullarda sürücü eklenemez");
                    }
                }
            }
            else
            {
                driverCollection.AddDriver(driver);
            }
        }
        public void AddHost(Host host)
        {
            if (hostCollection.Length < (this.Route.Distance + 599) / 600)
            {
                hostCollection.AddHost(host);
            }
        }

        public void RemoveDriver(Driver driver)
        {
            driverCollection.RemoveDriver(driver);
        }

        public void RemoveHost(Host host)
        {
            hostCollection.RemoveHost(host);
        }
        public decimal GetPriceOf(int seatNumber)
        {
            SeatInformation selection = new SeatInformation();
            if (Bus is StandardBus)
            {
                if (selection.Section == SeatSection.LeftSide)
                {
                    return Route.BasePrice * 125 / 100;
                }
                else
                {
                    return Route.BasePrice * 120 / 100;
                }
            }
            else
            {
                return Route.BasePrice * 135 / 100;
            }
        }
        public void AddToTickets(Ticket ticket)
        {
            ticketCollection.AddTicket(ticket);
        }
        private Ticket SellTicket(Person person, int seatNumber, decimal fee)
        {
            decimal kar = 105 / 100;
            if (IsSeatAvailableFor(person.Gender, seatNumber) && IsSeatEmpty(seatNumber))
            {
                if (person is Driver || person is Host)
                {
                    fee = Route.BasePrice;
                }
                fee = Route.BasePrice * Route.BasePrice * kar;

                Ticket ticket = new Ticket(this, Bus.GetSeatInformation(seatNumber), person, fee);
                AddToTickets(ticket);
                return ticket;
            }
            else
            {
                throw new Exception("HATA!!! Öncelikle ödeme yapmanız gerekmektedir.");
            }
        }
        public Ticket[] SellDoubleTickets(Person person01, Person person02, int seatNumber, decimal fee)
        {
            decimal kar = 105 / 100;
            Ticket[] tickets = new Ticket[2];

            if (Bus is StandardBus && Bus.GetSeatInformation(seatNumber).Category != SeatCategory.Singular && IsSeatEmpty(seatNumber))
            {
                int nearSeat = seatNumber % 3 == 2 ? seatNumber + 1 : seatNumber % 3 == 0 ? seatNumber - 1 :
                throw new Exception("Çift kişilik seçim için satın almalısınız.");
                if (IsSeatEmpty(nearSeat))
                {
                    if (person01 is Host || person01 is Driver && person02 is Host || person02 is Driver)
                    {
                        fee = Route.BasePrice;
                        tickets[0] = new Ticket(this, Bus.GetSeatInformation(seatNumber), person01, fee / 2);
                        tickets[1] = new Ticket(this, Bus.GetSeatInformation(nearSeat), person02, fee / 2);
                        AddToTickets(tickets[0]);
                        AddToTickets(tickets[1]);
                    }
                    else if (fee < Route.BasePrice * kar)
                    {
                        throw new Exception("HATA!! Öncelikle ödemenizi gerçekleştirmeniz gerekiyor.");
                    }
                    else
                    {
                        tickets[0] = new Ticket(this, Bus.GetSeatInformation(seatNumber), person01, fee / 2);
                        tickets[1] = new Ticket(this, Bus.GetSeatInformation(nearSeat), person02, fee / 2);
                        AddToTickets(tickets[0]);
                        AddToTickets(tickets[1]);
                    }
                }
                else throw new Exception("Seçtiğiniz koltuklar doludur.");
            }
            else throw new Exception("Lüks otobüs seçiminiz nedeniyle çift koltuk satın alamazsınız..");
            return tickets;
        }

        public void CancelTicket(Ticket ticket)
        {
            ticketCollection.RemoveTicket(ticket);
        }

        public bool IsSeatEmpty(int seatNumber)
        {
            for (int i = 0; i < ticketCollection.Length; i++)
            {
                if (ticketCollection[i].SeatInformation.Number == seatNumber)
                {
                    return false;
                }
            }
            return true;
        }

        private bool IsSeatAvailableFor(Gender gender, int seatNumber)
        {
            bool result = false;
            if (IsSeatEmpty(seatNumber))
            {
                if (Bus is LuxuryBus)
                    result = true;
                else
                {
                    if (seatNumber % 3 == 1)
                        result = true;
                    else
                    {
                        if (seatNumber % 3 == 2)
                        {
                            if (!IsSeatEmpty(seatNumber + 1))
                            {
                                if (IsSameGender(seatNumber + 1, gender))
                                    result = true;
                                else
                                    result = false;
                            }
                            else
                                result = true;
                        }
                        else if (seatNumber % 3 == 0)
                        {
                            if (!IsSeatEmpty(seatNumber - 1))
                            {
                                if (IsSameGender(seatNumber - 1, gender))
                                    result = true;
                                else
                                    result = false;
                            }
                            else
                                result = true;
                        }
                    }
                }
            }
            return result;
        }

        private bool IsSameGender(int seatNumber, Gender gender)
        {
            bool result = false;
            for (int i = 0; i < ticketCollection.Length; i++)
            {
                if (ticketCollection[i].SeatInformation.Number == seatNumber)
                {
                    if (ticketCollection[i].Passenger.Gender == gender)
                    {
                        result = true;
                    }
                }
            }
            return result;
        }
    }
}
