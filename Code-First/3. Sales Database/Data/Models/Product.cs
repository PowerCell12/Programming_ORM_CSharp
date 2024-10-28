using System.ComponentModel.DataAnnotations;

namespace P03_SalesDatabase.Data.Models{

    public class Product{

        [Key]
        public int ProductId {get; set;}

        [MaxLength(50)]
        public string Name {get; set;}


        public float Quantity {get; set;}

        public decimal Price {get; set;}

        public string Description {get; set;}

        public virtual ICollection<Sale> Sales {get; set;}

    }



}