namespace Infrastructure.Services;
using Domain.Entities;
using Domain.Dtos;
using Infrastructure.Mappers;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using AutoMapper;
public class Studentservice
{
    private DataContext _context;
    private IMapper _mapper;

    private readonly IWebHostEnvironment _hostEnvironment;

    public Studentservice(DataContext context, IWebHostEnvironment env, IMapper mapper)
    {
        _context = context;
        _hostEnvironment = env;
        _mapper = mapper;
    }

    public async Task<Response<List<GetStudents>>> GetStudents()
    {


        var list = await _context.Students.Select(s => new GetStudents()
        {
          ID = s.ID,
          LastName = s.LastName,
          FirstMidName = s.FirstMidName, 
          EnrollmentDate = s.EnrollmentDate,
          profileImage = s.profileImage

        }).ToListAsync();

        return new Response<List<GetStudents>>(list);


    }





    public async Task<Response<AddStudent>> InsertStudent(AddStudent student)
    {
        if (student.profileImage == null) return null;

        var path = Path.Combine(_hostEnvironment.WebRootPath, "images");
        if (Directory.Exists(path) == false) Directory.CreateDirectory(path);

        var filepath = Path.Combine(_hostEnvironment.WebRootPath, "images",student.profileImage.FileName);

        using (var stream = File.Create(filepath))
        {
            await student.profileImage.CopyToAsync(stream);
        }
        var newStudent = _mapper.Map<Student>(student);
        

        _context.Students.Add(newStudent);

        await _context.SaveChangesAsync();
        return new Response<AddStudent>(student);
    }
    public async Task<Response<GetStudents>> UpdateStudent(AddStudent student)
    {

        var find = await _context.Students.FindAsync(student.ID);
             find.FirstMidName = student.FirstMidName;
            find.LastName = student.LastName;
            find.EnrollmentDate = student.EnrollmentDate;
            find.profileImage = student.profileImage.FileName;

        if (student.profileImage != null)
        {
            find.profileImage = await UpdateFile(student.profileImage, find.profileImage);
        }
            await _context.SaveChangesAsync();

        var response = new GetStudents()
        {
             FirstMidName = student.FirstMidName,
            LastName = student.LastName,
            EnrollmentDate = student.EnrollmentDate,
            profileImage = student.profileImage.FileName
        };
        response.ID = student.ID;
        return new Response<GetStudents>(response);
    }
    public async Task<string> UpdateFile(IFormFile file, string oldFileName)
    {
        var filepath = Path.Combine(_hostEnvironment.WebRootPath, "images", oldFileName);
        if (File.Exists(filepath) == true) File.Delete(filepath);

        var newFilepath = Path.Combine(_hostEnvironment.WebRootPath, "images", file.FileName);
        using (var stream = new FileStream(newFilepath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return file.FileName;

    }
    public async Task<Response<string>> DeleteStudent(int id)
    {
        var find = await _context.Students.FindAsync(id);
        _context.Students.Remove(find);
        await _context.SaveChangesAsync();
        if (find.ID > 0) return new Response<string>("Student deleted successfully");


        return new Response<string>(HttpStatusCode.BadRequest, "Student not found");
    }
}


