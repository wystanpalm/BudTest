using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace BudTest.Data.Entity
{
    [XmlRoot("countries", Namespace = "http://www.worldbank.org")]
    public class WbCountries : List<WbCountry>
    {
    }
}
