using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace P01_HospitalDatabase.Data.Models{
    public class Patient{

        [Key]
        public int PatientId {get; set;}

        [MaxLength(50)]
        public string FirstName {get; set;}

        [MaxLength(50)]
        public string LastName {get; set;}

        [MaxLength(250)]
        public string Address {get; set;}

        [MaxLength(80)]
        [Unicode(false)]
        public string Email {get; set;}

        public bool HasInsurance {get; set;}

        public virtual ICollection<Visitation> Visitations {get; set;}

        public virtual ICollection<Diagnose> Diagnoses {get; set;}

        public virtual ICollection<PatientMedicament> Prescriptions {get; set;}

    }


}