using CMCS_Application.Models;

namespace CMCS_Application.Models
{
    public class Claim
    {
    public int ClaimId { get; set; }
    public string LecturerName { get; set;}
    public string LecturerEmail { get; set;}
    public int HoursWorked { get; set; }
    public int HourlyRate { get; set; }
    public string AdditionalNotes { get; set; }
    public ClaimStatus Status { get; set; } = ClaimStatus.Pending;
        public List<string> Documents { get; set; } = new List<string>();
    }
    public enum ClaimStatus
    {
        Pending,
        Approved,
        Rejected
    }
}
