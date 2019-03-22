using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoyageFramework
{
    public class Host : Person
    {
        public Host(string FirstName, string LastName,
           DateTime DateOfBirt) : base(FirstName, LastName)
        {
            if (Age < 18)
                throw new Exception("18 den büyük olmalı");
        }

    }
}
  