using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace P02_FootballBetting.Data.Models{


    public class Position{

        public int PositionId {get; set;}

        [Required]  
        [MaxLength(50)]
        public string Name {get; set;}

        public virtual ICollection<Player> Players {get; set;}
    }


}