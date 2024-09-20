using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace P02_FootballBetting.Data.Models{


    public class Game{

        public int GameId {get; set;}


        [ForeignKey(nameof(HomeTeamId))]
        public int HomeTeamId {get; set;}
        public virtual Team HomeTeam {get; set;}


        [ForeignKey(nameof(AwayTeamId))]
        public int AwayTeamId {get; set;}
        public virtual Team AwayTeam {get; set;}


        public int HomeTeamGoals {get; set;}

        public int AwayTeamGoals {get; set;}

        public DateTime DateTime {get; set;}

        public double HomeTeamBetRate {get; set;}

        public double AwayTeamBetRate {get; set;}
        
        public double DrawBetRate {get; set;}

        public string Result {get; set;}
    
        public virtual ICollection<PlayerStatistic> PlayersStatistics {get; set;}

        public virtual ICollection<Bet> Bets {get; set;}

    }



}