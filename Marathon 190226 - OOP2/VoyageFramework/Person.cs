using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoyageFramework
{
    public class Person
    {
        public string FirstName { get; }
        public string LastName { get; }
        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", this.FirstName, this.LastName);
            }

        }
        public string IdentityNumber { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }

        public int Age
        {
            get
            {
                return Convert.ToInt32(DateTime.Now.Date.Subtract(DateOfBirth).TotalDays) / 365;
            }
        }
        
        public Person(string firstName, string lastName)
        {
            firstName = FirstName;
            lastName = LastName;
        }

    }
}


