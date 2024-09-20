using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace P02_FootballBetting.Data.Models{


    public class Player{

        public int PlayerId {get; set;}

        [Required]
        public string Name {get; set;}

        public int SquadNumber {get; set;}

        public int Assists {get; set;}


        [ForeignKey(nameof(TownId))]
        public int TownId {get; set;}

        public virtual Town Town {get; set;}


        
        [ForeignKey(nameof(PositionId))]

        public int PositionId {get; set;}
        
        public virtual Position Position {get; set;}
        



        public bool IsInjured {get; set;}


        [ForeignKey(nameof(TeamId))]
        public int TeamId {get; set;}

        public virtual Team Team {get; set;}


        public virtual ICollection<PlayerStatistic> PlayersStatistics {get; set;}


    }



}