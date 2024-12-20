using System;
using System.Collections.Generic;
using System.Linq;

namespace SampleManager.Services
{
    // IAttendanceService Interface
    public interface IAttendanceService
    {
        IEnumerable<SampleManager.Models.Attendance> GetAllAttendances();
        SampleManager.Models.Attendance? GetAttendanceById(int id);
        void AddAttendance(SampleManager.Models.Attendance newAttendance);
        void UpdateAttendance(int id, SampleManager.Models.Attendance updatedAttendance);
        void DeleteAttendance(int id);
    }

    // AttendanceService class that implements IAttendanceService
    public class AttendanceService : IAttendanceService
    {
        private readonly List<SampleManager.Models.Attendance> _attendances = new List<SampleManager.Models.Attendance>();

        public IEnumerable<SampleManager.Models.Attendance> GetAllAttendances()
        {
            return _attendances;
        }

        public SampleManager.Models.Attendance? GetAttendanceById(int id)
        {
            return _attendances.FirstOrDefault(a => a.Id == id);
        }

        public void AddAttendance(SampleManager.Models.Attendance newAttendance)
        {
            newAttendance.Id = _attendances.Any() ? _attendances.Max(a => a.Id) + 1 : 1;
            _attendances.Add(newAttendance);
            Console.WriteLine($"Attendance for Employee ID {newAttendance.EmployeeId} on {newAttendance.Date:yyyy-MM-dd} added.");
        }

        public void UpdateAttendance(int id, SampleManager.Models.Attendance updatedAttendance)
        {
            var existingAttendance = _attendances.FirstOrDefault(a => a.Id == id);
            if (existingAttendance != null)
            {
                existingAttendance.Date = updatedAttendance.Date;
                existingAttendance.EmployeeId = updatedAttendance.EmployeeId;
                existingAttendance.IsPresent = updatedAttendance.IsPresent;
                Console.WriteLine($"Attendance ID {id} updated.");
            }
            else
            {
                Console.WriteLine($"Attendance ID {id} not found.");
            }
        }

        public void DeleteAttendance(int id)
        {
            var attendance = _attendances.FirstOrDefault(a => a.Id == id);
            if (attendance != null)
            {
                _attendances.Remove(attendance);
                Console.WriteLine($"Attendance ID {id} deleted.");
            }
            else
            {
                Console.WriteLine($"Attendance ID {id} not found.");
            }
        }
    }
}

// In the Models namespace:
namespace SampleManager.Models
{
    public class Attendance
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int EmployeeId { get; set; }
        public bool IsPresent { get; set; }
    }
}
