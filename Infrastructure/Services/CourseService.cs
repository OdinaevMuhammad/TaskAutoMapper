namespace Infrastructure.Services;
using Domain.Entities;
using Domain.Dtos;

using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

public class Courseservice
{
    private DataContext _context;

    private readonly IWebHostEnvironment _hostEnvironment;

    public Courseservice(DataContext context, IWebHostEnvironment env)
    {
        _context = context;
        _hostEnvironment = env;
    }

    public async Task<Response<List<GetCourses>>> GetCourses()
    {


        var list = await _context.Courses.Select(s => new GetCourses()
        {
            CourseID = s.CourseID,
            Title = s.Title,
            Credits = s.Credits

        }).ToListAsync();

        return new Response<List<GetCourses>>(list);


    }





    public async Task<Response<AddCourse>> InsertCourse(AddCourse course)
    {
     
        var newCourse = new Course()
        {
          CourseID = course.CourseID,
            Title = course.Title,
            Credits = course.Credits
        };
        _context.Courses.Add(newCourse);

        await _context.SaveChangesAsync();
        newCourse.CourseID = course.CourseID;
        return new Response<AddCourse>(course);
    }
    public async Task<Response<GetCourses>> UpdateCourse(AddCourse course)
    {

        var find = await _context.Courses.FindAsync(course.CourseID);
              find.CourseID = course.CourseID;
            find.Title = course.Title;
            find.Credits = course.Credits;

 
        var response = new GetCourses()
        {
           CourseID = course.CourseID,
            Title = course.Title,
            Credits = course.Credits
        };

        await _context.SaveChangesAsync();
        response.CourseID = course.CourseID;
        return new Response<GetCourses>(response);
    }

    public async Task<Response<string>> DeleteCourse(int id)
    {
        var find = await _context.Courses.FindAsync(id);
        _context.Courses.Remove(find);
        await _context.SaveChangesAsync();
        if (find.CourseID > 0) return new Response<string>("Course deleted successfully");


        return new Response<string>(HttpStatusCode.BadRequest, "Course not found");
    }
}


