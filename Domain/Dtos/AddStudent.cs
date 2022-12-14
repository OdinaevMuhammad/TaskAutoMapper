using System.Reflection;
namespace Domain.Dtos;
using Microsoft.AspNetCore.Http;
    public class AddStudent
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public IFormFile profileImage { get; set; }
        
        
    }
      public class GetStudents
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public DateTime EnrollmentDate { get; set; }     
           public string profileImage { get; set; }

    }

