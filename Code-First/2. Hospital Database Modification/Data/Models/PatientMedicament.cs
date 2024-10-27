using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace P01_HospitalDatabase.Data.Models{


    public class PatientMedicament{

        [ForeignKey(nameof(PatientId))]
        public int PatientId {get; set;}
        public  virtual Patient Patient {get; set;}


        [ForeignKey(nameof(MedicamentId))]
        public int MedicamentId {get; set;}
        public  virtual Medicament Medicament {get; set;}


    }



}