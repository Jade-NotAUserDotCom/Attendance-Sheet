namespace AttendanceManagementModels // Ensure this matches your project structure
{
    public class AttendanceRecord
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public string Date { get; set; } = string.Empty;
        public bool IsPresent { get; set; }
    }
}