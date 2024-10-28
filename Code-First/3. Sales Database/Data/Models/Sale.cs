using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security;

namespace P03_SalesDatabase.Data.Models{

    public class Sale{

        [Key]
        public int SaleId {get; set;}

        public DateTime Date {get; set;} // maybe


        [ForeignKey(nameof(ProductId))]
        public int ProductId {get; set;}
        public Product Product {get; set;}


        [ForeignKey(nameof(CustomerId))]
        public int CustomerId {get; set;}
        public Customer Customer {get; set;}


        [ForeignKey(nameof(StoreId))]
        public int StoreId {get; set;}
        public Store Store {get; set;}
    }



}
