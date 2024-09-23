using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Formats.Asn1;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MusicHub.Data.Models{

    public class Song{

        public Song(){

            SongPerformers = new List<SongPerformer>();

        }


        [Key]
        public int Id {get; set;}


        [Required]
        [MaxLength(20)]
        public string Name {get; set;}

        [Required]
        public TimeSpan Duration {get; set;}

        [Required]
        public DateTime CreatedOn {get; set;}

        [Required]
        public Genre Genre {get; set;}



        [ForeignKey(nameof(AlbumId))]
        public int? AlbumId {get; set;}

        public Album Album {get; set;}


        [ForeignKey(nameof(WriterId))]
        [Required]
        public int WriterId {get; set;}

        public Writer Writer {get; set;}

        [Required]
        public decimal Price {get; set;}


        public List<SongPerformer> SongPerformers {get; set;}



    }


}


public enum Genre{
             Blues,
            Rap, 
            PopMusic,
            Rock, 
            Jazz
}
