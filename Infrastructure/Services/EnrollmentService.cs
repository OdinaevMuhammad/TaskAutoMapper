namespace Infrastructure.Services;
using Domain.Entities;
using Domain.Dtos;
using AutoMapper;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

public class Enrollmentservice
{
    private DataContext _context;
    private IMapper _mapper;

    private readonly IWebHostEnvironment _hostEnvironment;

    public Enrollmentservice(DataContext context, IWebHostEnvironment env, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
        _hostEnvironment = env;
    }

    public async Task<Response<List<GetEnrollments>>> GetEnrollments()
    {


        var list = await _context.Enrollments.Select(s => new GetEnrollments()
        {
            EnrollmentID = s.EnrollmentID,
            CourseID = s.CourseID,
            StudentID = s.StudentID,
            FirstMidName = s.Student.FirstMidName,
            LastName = s.Student.LastName,
            Title = s.Course.Title

        }).ToListAsync();

        return new Response<List<GetEnrollments>>(list);


    }





    public async Task<Response<AddEnrollment>> InsertEnrollment(AddEnrollment enrollment)
    {
     
        var newEnrollment = _mapper.Map<Enrollment>(enrollment);
     
        _context.Enrollments.Add(newEnrollment);

        await _context.SaveChangesAsync();
        newEnrollment.EnrollmentID  = enrollment.EnrollmentID ;

        return new Response<AddEnrollment>(enrollment);
    }
    public async Task<Response<GetEnrollments>> UpdateEnrollment(AddEnrollment enrollment)
    {

        var find = await _context.Enrollments.FindAsync(enrollment.EnrollmentID);
            find.StudentID = enrollment.StudentID;
            find.CourseID = enrollment.CourseID;
            find.Grade = enrollment.Grade;

 
        var response = new GetEnrollments()
        {
           EnrollmentID = enrollment.EnrollmentID,
            StudentID = enrollment.StudentID,
            CourseID = enrollment.CourseID
        };
                await _context.SaveChangesAsync();
            find.EnrollmentID = enrollment.EnrollmentID;
        return new Response<GetEnrollments>(response);
    }

    public async Task<Response<string>> DeleteEnrollment(int id)
    {
        var find = await _context.Enrollments.FindAsync(id);
        _context.Enrollments.Remove(find);
        await _context.SaveChangesAsync();
        if (find.EnrollmentID > 0) return new Response<string>("Enrollment deleted successfully");

        
        return new Response<string>(HttpStatusCode.BadRequest, "Enrollment not found");
    }
}


