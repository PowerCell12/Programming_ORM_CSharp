
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using System.Reflection;

namespace MusicHub.Data.Models{

    public class Album{

        public Album() {

            Songs = new List<Song>();
            Price = Songs.Sum(x => x.Price);
        }
        

        [Key]
        public int Id {get; set;}

        [Required]
        [MaxLength(40)]
        public string Name {get; set;}


        [Required]
        public DateTime ReleaseDate {get; set;}

        public decimal Price { get; set; }

        

        [ForeignKey(nameof(ProducerId))]
        public int? ProducerId {get; set;}


        public Producer Producer {get; set;}


        public List<Song> Songs {get; set;}



    }


}