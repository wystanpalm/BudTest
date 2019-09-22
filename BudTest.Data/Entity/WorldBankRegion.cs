using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace BudTest.Data.Entity
{
    [DataContract]
    public class WorldBankRegion
    {
        [DataMember(Name = "value")]
        public string Value { get; set; }
    }
}
