namespace WebApi.Controllers;
using Domain.Entities;
using Domain.Dtos;
using  Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;

using Infrastructure.Services;
[ApiController]
[Route("[controller]")]
public class StudentServiceController
{


    private readonly Studentservice _studentservice;

    public StudentServiceController(Studentservice studentservice)
    {
        _studentservice = studentservice;
    }
    
    [HttpGet("GetStudents")]
    public async Task<Response<List<GetStudents>>> GetAllAsync()
    {
        return await _studentservice.GetStudents();
    }

    [HttpPost("Insert")]
    public async Task<Response<AddStudent>> AddStudent([FromForm]AddStudent Student)
    {
        return await _studentservice.InsertStudent(Student);
    }
    [HttpPut("Update")]
    public async Task<Response<GetStudents>> Update([FromForm]AddStudent Student)
    {
        return await _studentservice.UpdateStudent(Student);
    }
    [HttpDelete("Delete Student")]
    public async Task<Response<string>> Delete(int id)
    {
        return await _studentservice.DeleteStudent(id);
    }
    
}