namespace Domain.Dtos;
public class AddEnrollment
    {
        public int EnrollmentID { get; set; }
        public int CourseID { get; set; }
        public int StudentID { get; set; }
        public int  Grade { get; set; }

       
    }
    public class GetEnrollments
    {

     public int CourseID { get; set; }
        public int StudentID { get; set; }
                public int EnrollmentID { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        
        
        public string FirstMidName {get; set; }
            public int  Grade { get; set; }
       
    }