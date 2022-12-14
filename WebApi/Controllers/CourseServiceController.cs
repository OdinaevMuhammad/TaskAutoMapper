namespace WebApi.Controllers;
using Domain.Entities;
using Domain.Dtos;
using  Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;

using Infrastructure.Services;
[ApiController]
[Route("[controller]")]
public class CourseServiceController
{


    private readonly Courseservice _courseservice;

    public CourseServiceController(Courseservice courseservice)
    {
        _courseservice = courseservice;
    }
    
    [HttpGet("GetCourses")]
    public async Task<Response<List<GetCourses>>> GetAllAsync()
    {
        return await _courseservice.GetCourses();
    }

    [HttpPost("Insert")]
    public async Task<Response<AddCourse>> AddCourse(AddCourse course)
    {
        return await _courseservice.InsertCourse(course);
    }
    [HttpPut("Update")]
    public async Task<Response<GetCourses>> Update(AddCourse course)
    {
        return await _courseservice.UpdateCourse(course);
    }
    [HttpDelete("Delete Course")]
    public async Task<Response<string>> Delete(int id)
    {
        return await _courseservice.DeleteCourse(id);
    }
}