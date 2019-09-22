using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace BudTest.Data.Entity
{
    public class WorldBankCountry
    {
        public string name { get; set; }

        public WorldBankRegion region { get; set; }

        public string capitalCity { get; set; }

        public decimal longitude { get; set; }

        public decimal latitude { get; set; }
    }
}
