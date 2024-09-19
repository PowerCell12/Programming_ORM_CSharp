using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;
using Microsoft.EntityFrameworkCore;

namespace P01_StudentSystem.Data.Models{


    public class StudentCourse{

        [ForeignKey(nameof(StudentId))]
        public int StudentId {get; set;}
        public Student Student {get; set;}


        [ForeignKey(nameof(CourseId))]
        public int CourseId {get; set;}
        public Course Course {get; set;}

    }



}