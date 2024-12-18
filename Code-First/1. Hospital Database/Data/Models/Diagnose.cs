using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace P01_HospitalDatabase.Data.Models{


    public class Diagnose{

        [Key]
        public int DiagnoseId {get; set;}

        [MaxLength(50)]
        public string Name {get; set;}

        [MaxLength(250)]
        public string Comments {get; set;}


        [ForeignKey(nameof(PatientId))]
        public int PatientId {get; set;}
        public Patient Patient {get; set;}

    }



}