using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace P01_HospitalDatabase.Data.Models{

    public class Medicament{
        [Key]
        public int MedicamentId{get; set;}

        [MaxLength(50)]
        public string Name {get; set;}

        public ICollection<PatientMedicament> Prescriptions {get; set;}

    }




}