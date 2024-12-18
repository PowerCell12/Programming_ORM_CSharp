using System.Numerics;
using System.Xml.Serialization;

namespace CarDealer.DTOs.Import;


[XmlType("Customer")]
public class CustomerDto{

    [XmlElement("name")]
    public string Name {get; set;}

    [XmlElement("birthDate")]
    public DateTime BirthDate {get ;set;}

    [XmlElement("isYoungDriver")]
    public bool IsYoungDriver {get; set;}
}


