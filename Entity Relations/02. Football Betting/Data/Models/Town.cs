using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;
using System.Drawing;

namespace P02_FootballBetting.Data.Models{

    public class Town{
        public int TownId {get; set;}

        public string Name {get; set;}


        [ForeignKey(nameof(CountryId))]
        public int CountryId {get; set;}

        public virtual Country Country {get; set;}


        public virtual ICollection<Player> Players {get; set;}

        public virtual ICollection<Team> Teams {get; set;} // wtf

    }



}
