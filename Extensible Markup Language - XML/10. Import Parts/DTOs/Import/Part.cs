using System.Xml.Serialization;

namespace CarDealer.DTOs.Import;


[XmlType("Part")]
public class PartDto{

    [XmlElement("name")]
    public string Name {get; set;}

    [XmlElement("price")]
    public decimal Price {get; set;} // not sure

    [XmlElement("quantity")]
    public int Quantity {get; set;}

    [XmlElement("supplierId")]
    public int SupplierId {get; set;}

}