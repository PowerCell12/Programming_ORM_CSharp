using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace P01_HospitalDatabase.Data.Models{


    public class Visitation{

        [Key]
        public int VisitationId {get; set;}


        public DateTime Date {get; set;} // if error here

        [MaxLength(250)]
        public string Comments {get; set;}


        [ForeignKey(nameof(DoctorId))]
        public int DoctorId {get; set;}
        public virtual  Doctor Doctor {get; set;}

        
        [ForeignKey(nameof(PatientId))]
        public int PatientId {get; set;}
        public virtual Patient Patient {get; set;}


    }



}