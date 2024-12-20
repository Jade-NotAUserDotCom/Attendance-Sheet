using System;
using System.Collections.Generic;
using System.Linq;

namespace AttendanceManagement
{
    // AttendanceRecord class represents an employee's attendance record
    public class AttendanceRecord
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; }
        public string Date { get; set; }
        public bool IsPresent { get; set; }

        public AttendanceRecord(int id, string employeeName, string date, bool isPresent)
        {
            Id = id;
            EmployeeName = employeeName;
            Date = date;
            IsPresent = isPresent;
        }
    }

    // AttendanceManager class handles operations on the attendance records
    public class AttendanceManager
    {
        private readonly List<AttendanceRecord> _records = new List<AttendanceRecord>();

        public IEnumerable<AttendanceRecord> GetAllRecords()
        {
            return _records;
        }

        public AttendanceRecord GetRecordById(int id)
        {
            return _records.FirstOrDefault(r => r.Id == id);
        }

        public void AddRecord(AttendanceRecord newRecord)
        {
            newRecord.Id = _records.Any() ? _records.Max(r => r.Id) + 1 : 1; // Auto-increment ID
            _records.Add(newRecord);
            Console.WriteLine($"Record for '{newRecord.EmployeeName}' on {newRecord.Date} added.");
        }

        public void UpdateRecord(int id, AttendanceRecord updatedRecord)
        {
            var existingRecord = _records.FirstOrDefault(r => r.Id == id);
            if (existingRecord != null)
            {
                existingRecord.EmployeeName = updatedRecord.EmployeeName;
                existingRecord.Date = updatedRecord.Date;
                existingRecord.IsPresent = updatedRecord.IsPresent;
                Console.WriteLine($"Record {id} updated.");
            }
            else
            {
                Console.WriteLine("Record not found.");
            }
        }

        public void MarkAsPresent(int id)
        {
            var record = _records.FirstOrDefault(r => r.Id == id);
            if (record != null)
            {
                record.IsPresent = true;
                Console.WriteLine($"Record for '{record.EmployeeName}' on {record.Date} marked as present.");
            }
            else
            {
                Console.WriteLine("Record not found.");
            }
        }

        public void DeleteRecord(int id)
        {
            var record = _records.FirstOrDefault(r => r.Id == id);
            if (record != null)
            {
                _records.Remove(record);
                Console.WriteLine($"Record for '{record.EmployeeName}' on {record.Date} deleted.");
            }
            else
            {
                Console.WriteLine("Record not found.");
            }
        }
    }

    // Program class to test the Attendance Manager functionality
    class Program
    {
        static void Main(string[] args)
        {
            AttendanceManager attendanceManager = new AttendanceManager();

            // Add attendance records
            attendanceManager.AddRecord(new AttendanceRecord(0, "John Doe", "2024-12-20", true));
            attendanceManager.AddRecord(new AttendanceRecord(0, "Jane Smith", "2024-12-20", false));
            attendanceManager.AddRecord(new AttendanceRecord(0, "Alice Brown", "2024-12-20", true));

            // Display all records
            Console.WriteLine("\nAll Attendance Records:");
            foreach (var record in attendanceManager.GetAllRecords())
            {
                Console.WriteLine($"ID: {record.Id}, Name: {record.EmployeeName}, Date: {record.Date}, Present: {record.IsPresent}");
            }

            // Update a record
            attendanceManager.UpdateRecord(2, new AttendanceRecord(2, "Jane Smith", "2024-12-20", true));

            // Mark a record as present
            attendanceManager.MarkAsPresent(3);

            // Display all records after updates
            Console.WriteLine("\nAttendance Records After Updates:");
            foreach (var record in attendanceManager.GetAllRecords())
            {
                Console.WriteLine($"ID: {record.Id}, Name: {record.EmployeeName}, Date: {record.Date}, Present: {record.IsPresent}");
            }

            // Delete a record
            attendanceManager.DeleteRecord(1);

            // Display all records after deletion
            Console.WriteLine("\nAttendance Records After Deletion:");
            foreach (var record in attendanceManager.GetAllRecords())
            {
                Console.WriteLine($"ID: {record.Id}, Name: {record.EmployeeName}, Date: {record.Date}, Present: {record.IsPresent}");
            }
        }
    }
}