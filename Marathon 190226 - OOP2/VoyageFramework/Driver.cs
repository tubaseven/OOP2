using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoyageFramework
{

    public class Driver : Person
    {
        public LicenseType LicenseType { get; set; }

        private DateTime _dateofbirth;
        private int _age;

        public Driver(string firstname, string lastname, LicenseType licenceType, DateTime dateofbirth, int age) : base(firstname, lastname)
        {
            if (age < 25)
            {
                throw new Exception("Sürücüler minumum 25 yaşında olmalı.");
            }
            LicenseType = licenceType;
        }
    }
}

