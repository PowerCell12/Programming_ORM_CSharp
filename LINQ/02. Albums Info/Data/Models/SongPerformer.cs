

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace MusicHub.Data.Models{

    public class SongPerformer{


        [ForeignKey(nameof(SongId))]
        public int SongId {get; set;}

        [Required]
        public Song Song {get; set;}


        [ForeignKey(nameof(PerformerId))]
        public int PerformerId {get; set;}

        [Required]
        public Performer Performer {get; set;}



    }


}