using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace P03_SalesDatabase.Data.Models{

    public class Customer{

        [Key]
        public int CustomerId {get; set;}

        [MaxLength(100)]
        public string Name {get; set;}

        
        [Unicode(false)] // wtf
        [MaxLength(80)]      
        public string Email {get; set;}

        public string CreditCardNumber {get; set;}

        public virtual ICollection<Sale> Sales {get; set;}
        

    }



}