using Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Context;

public class DataContext:DbContext
{
 public DataContext(DbContextOptions<DataContext> options) : base(options)
 {

 }
 protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Enrollment>().HasKey(sc => new { sc.StudentID, sc.CourseID});

        modelBuilder.Entity<Enrollment>()
        .HasOne<Student>(sc => sc.Student)
        .WithMany(s => s.Enrollments)
        .HasForeignKey(sc => sc.StudentID);


        modelBuilder.Entity<Enrollment>()
        .HasOne<Course>(sc => sc.Course)
        .WithMany(s => s.Enrollments)
        .HasForeignKey(sc => sc.CourseID);
    }
    public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Student> Students { get; set; }
  

}