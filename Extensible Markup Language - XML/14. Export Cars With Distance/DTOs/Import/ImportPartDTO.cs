using System.Numerics;
using System.Xml.Serialization;

namespace CarDealer.DTOs.Import;

[XmlType("partID ")]
public class ImportPartIdDTO{

    [XmlAttribute("id")]
    public int Id {get; set;}


}