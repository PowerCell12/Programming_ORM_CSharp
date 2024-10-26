using System.Numerics;
using System.Xml.Serialization;

namespace CarDealer.DTOs.Import;

[XmlType("car")]
public class ExportCarMake{

    [XmlAttribute("id")]
    public string Id {get; set;}


    [XmlAttribute("model")]
    public string Model {get; set;}

    [XmlAttribute("traveled-distance")]
    public long TraveledDistance {get; set;}

}