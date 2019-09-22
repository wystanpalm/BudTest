using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace BudTest.Data.Entity
{
    [DataContract]
    public class WorldBankCountry
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "region")]
        public WorldBankRegion Region { get; set; }

        [DataMember(Name = "capitalCity")]
        public string CapitalCity { get; set; }

        [DataMember(Name = "longitude")]
        public decimal Longitude { get; set; }

        [DataMember(Name = "latitude")]
        public decimal Latitude { get; set; }
    }
}
