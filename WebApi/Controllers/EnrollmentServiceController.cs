namespace WebApi.Controllers;
using Domain.Entities;
using Domain.Dtos;
using  Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;

using Infrastructure.Services;
[ApiController]
[Route("[controller]")]
public class EnrollmentServiceController
{


    private readonly Enrollmentservice _enrollmentservice;

    public EnrollmentServiceController(Enrollmentservice enrollmentservice)
    {
        _enrollmentservice = enrollmentservice;
    }
    
    [HttpGet("GetEnrollments")]
    public async Task<Response<List<GetEnrollments>>> GetAllAsync()
    {
        return await _enrollmentservice.GetEnrollments();
    }

    [HttpPost("Insert")]
    public async Task<Response<AddEnrollment>> AddEnrollment([FromForm]AddEnrollment Enrollment)
    {
        return await _enrollmentservice.InsertEnrollment(Enrollment);
    }
    [HttpPut("Update")]
    public async Task<Response<GetEnrollments>> Update([FromForm]AddEnrollment Enrollment)
    {
        return await _enrollmentservice.UpdateEnrollment(Enrollment);
    }
    [HttpDelete("Delete Enrollment")]
    public async Task<Response<string>> Delete(int id)
    {
        return await _enrollmentservice.DeleteEnrollment(id);
    }
}