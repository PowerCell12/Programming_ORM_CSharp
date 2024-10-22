using System.Xml.Serialization;

namespace  ProductShop.Dtos.Import;


[XmlType("Product")]
public class ProductDto{

    [XmlElement("name")]
    public string Name {get ;set;}

    [XmlElement("age")]
    public int Age {get; set;}

    [XmlElement("selledId")]
    public int SellerId {get; set;}


    [XmlElement("buyerId")]
    public int BuyerId {get; set;}
}

