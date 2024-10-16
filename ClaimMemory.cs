using System.Security.Claims;

namespace CMCS_Application.Models
{
    public class ClaimMemory
    {
        public static List<Claim> ClaimList = new List<Claim>();
        public static List<Claim> SubmittedClaim = new List<Claim>();
        public static List<Claim> ClaimHistory = new List<Claim>();
        }
    }

