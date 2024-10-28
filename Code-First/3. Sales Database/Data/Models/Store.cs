using System.ComponentModel.DataAnnotations;
using System.Security;

namespace P03_SalesDatabase.Data.Models{

    public class Store{

        [Key]
        public int StoreId {get; set;}

        [MaxLength(80)]
        public string Name {get; set;}

        public virtual ICollection<Sale> Sales {get; set;}

    }



}