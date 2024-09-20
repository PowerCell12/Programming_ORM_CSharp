using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace P02_FootballBetting.Data.Models{

    public class Team{

        public int TeamId {get; set;}

        [Required]
        [MaxLength(256)]   
        public string Name {get; set;}

        public string LogoUrl {get; set;}

        [MaxLength(3)]
        public string Initials {get; set;}

        public decimal Budget {get; set;}



        [ForeignKey(nameof(PrimaryKitColorId))]
        public int PrimaryKitColorId {get; set;}

        public virtual Color PrimaryKitColor {get; set;}



        [ForeignKey(nameof(SecondaryKitColorId))]
        public int SecondaryKitColorId {get; set;}

        public virtual Color SecondaryKitColor {get; set;}



        [ForeignKey(nameof(TownId))]
        public int TownId {get; set;}

        public virtual Town Town {get; set;}


        public virtual ICollection<Player> Players {get; set;}


        [InverseProperty(nameof(Game.HomeTeam))]
        public virtual ICollection<Game>  HomeGames {get; set;}

 
        [InverseProperty(nameof(Game.AwayTeam))]
        public virtual  ICollection<Game> AwayGames {get; set;}

    }




    // public enum Initial{
    //     JUV,
    //     LIV,
    //     ARS
    // }


}