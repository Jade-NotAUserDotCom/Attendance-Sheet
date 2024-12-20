using System;
using System.Collections.Generic;

namespace AttendanceManagement
{
    public class AttendanceRecord
    {
        public int Id { get; set; }
        public string StudentName { get; set; }
        public DateTime Date { get; set; }
        public bool IsPresent { get; set; }

        public AttendanceRecord(int id, string studentName, DateTime date)
        {
            Id = id;
            StudentName = studentName;
            Date = date;
            IsPresent = false;
        }
    }

    public class AttendanceManager
    {
        private static int _nextRecordId = 1;
        private readonly List<AttendanceRecord> _records = new List<AttendanceRecord>();

        public void AddRecord(string studentName, DateTime date)
        {
            var newRecord = new AttendanceRecord(_nextRecordId++, studentName, date);
            _records.Add(newRecord);
            Console.WriteLine($"Attendance record for '{newRecord.StudentName}' on {newRecord.Date.ToShortDateString()} has been added.");
        }

        public IEnumerable<AttendanceRecord> GetRecords()
        {
            return _records;
        }

        public AttendanceRecord GetRecordById(int id)
        {
            return _records.Find(record => record.Id == id);
        }

        public void UpdateRecord(int id, string studentName, DateTime date)
        {
            var record = GetRecordById(id);
            if (record != null)
            {
                record.StudentName = studentName;
                record.Date = date;
                Console.WriteLine($"Attendance record ID {id} has been updated.");
            }
            else
            {
                Console.WriteLine("Record not found.");
            }
        }

        public void MarkAsPresent(int id)
        {
            var record = GetRecordById(id);
            if (record != null)
            {
                record.IsPresent = true;
                Console.WriteLine($"Attendance record ID {id} has been marked as present.");
            }
            else
            {
                Console.WriteLine("Record not found.");
            }
        }

        public void RemoveRecord(int id)
        {
            int removedCount = _records.RemoveAll(record => record.Id == id);
            if (removedCount > 0)
            {
                Console.WriteLine($"Attendance record ID {id} has been removed.");
            }
            else
            {
                Console.WriteLine("Record not found.");
            }
        }
    }

    // Program class with the single Main method
    class Program
    {
        private static void main(string[] args)
        {
            var attendanceManager = new AttendanceManager();

            // Add attendance records
            attendanceManager.AddRecord("John Doe", new DateTime(2024, 12, 20)); // ID 1
            attendanceManager.AddRecord("Jane Smith", new DateTime(2024, 12, 20)); // ID 2
            attendanceManager.AddRecord("Michael Brown", new DateTime(2024, 12, 20)); // ID 3

            Console.WriteLine("\nAttendance Records:");
            foreach (var record in attendanceManager.GetRecords())
            {
                Console.WriteLine($"ID: {record.Id}, Name: {record.StudentName}, Date: {record.Date.ToShortDateString()}, Present: {record.IsPresent}");
            }

            // Mark a record as present
            attendanceManager.MarkAsPresent(1);

            // Update a record
            attendanceManager.UpdateRecord(2, "Jane Johnson", new DateTime(2024, 12, 21));

            // Remove a record
            attendanceManager.RemoveRecord(3); // Remove Michael Brown

            Console.WriteLine("\nUpdated Attendance Records:");
            foreach (var record in attendanceManager.GetRecords())
            {
                Console.WriteLine($"ID: {record.Id}, Name: {record.StudentName}, Date: {record.Date.ToShortDateString()}, Present: {record.IsPresent}");
            }

            Console.ReadKey();
        }
    }
}