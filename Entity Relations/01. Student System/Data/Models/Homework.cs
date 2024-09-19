using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;
using Microsoft.EntityFrameworkCore;

namespace P01_StudentSystem.Data.Models{


    public class Homework{

        [Key]
        public int HomeworkId {get; set;}

        [Unicode(false)]
        public string Content {get; set;}

        public ContentType ContentType {get; set;}

        public DateTime SubmissionTime {get; set;}


        [ForeignKey(nameof(StudentId))]
        public int StudentId {get; set;}
        public Student Student {get; set;}


        [ForeignKey(nameof(CourseId))]
        public int CourseId {get; set;}
        public Course Course {get; set;}


    }

    public enum ContentType{
        Application,
        Pdf,
        Zip
    }



}