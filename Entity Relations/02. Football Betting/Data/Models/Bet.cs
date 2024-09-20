using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace P02_FootballBetting.Data.Models{


    public class Bet{

        public int BetId {get; set;}

        public decimal Amount {get; set;}

        [Required]
        public string Prediction {get; set;}

        public DateTime DateTime {get; set;}

    
    
        [ForeignKey(nameof(UserId))]
        public int UserId {get; set;}
        public User User {get; set;}

    

        
        [ForeignKey(nameof(GameId))]
        public int  GameId {get; set;}
        public Game Game {get; set;}

    }



}