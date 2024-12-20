using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AttendanceManagementSystem
{
    // Models
    public class AttendanceRecord
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; }
        public string Date { get; set; }
        public bool IsPresent { get; set; } // True if the employee is present
    }

    // Interfaces
    public interface IAttendanceService
    {
        void AddAttendanceRecord(AttendanceRecord record);
        IEnumerable<AttendanceRecord> GetAllRecords();
        void MarkAsPresent(int recordId);
    }

    // Services (Implementing the interface)
    public class AttendanceManager : IAttendanceService
    {
        private readonly List<AttendanceRecord> _records = new List<AttendanceRecord>();

        public void AddAttendanceRecord(AttendanceRecord record)
        {
            record.Id = _records.Count + 1; // Auto-increment Id
            record.IsPresent = false;       // Default as not present
            _records.Add(record);
        }

        public IEnumerable<AttendanceRecord> GetAllRecords()
        {
            return _records;
        }

        public void MarkAsPresent(int recordId)
        {
            var record = _records.Find(r => r.Id == recordId);
            if (record != null)
                record.IsPresent = true;
        }
    }

    // Controllers
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _attendanceService;

        public AttendanceController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<AttendanceRecord>> Get()
        {
            return Ok(_attendanceService.GetAllRecords());
        }

        [HttpPost]
        public ActionResult AddRecord([FromBody] AttendanceRecord record)
        {
            _attendanceService.AddAttendanceRecord(record);
            return CreatedAtAction(nameof(Get), new { id = record.Id }, record);
        }

        [HttpPut("{recordId}")]
        public ActionResult MarkAsPresent(int recordId)
        {
            _attendanceService.MarkAsPresent(recordId);
            return NoContent();
        }
    }

    // Main Program
    internal class Program
    {
        private static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container
            builder.Services.AddControllers();

            // Register services for dependency injection
            builder.Services.AddTransient<IAttendanceService, AttendanceManager>();

            // Add Swagger for API documentation
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();

            // Map controllers
            app.MapControllers();

            // Run the application
            app.Run();
        }
    }
}