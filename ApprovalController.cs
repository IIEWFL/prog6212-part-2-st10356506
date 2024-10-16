using Microsoft.AspNetCore.Mvc;
using CMCS_Application.Models;

namespace CMCS_Application.Controllers
{
    public class ApprovalController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View(ClaimMemory.ClaimList);
        }

        [HttpPost]
        public IActionResult Approve(int claimID)
        {
            var claim = ClaimMemory.ClaimList.FirstOrDefault(c => c.ClaimId == claimID);
            if (claim != null)
            {
                claim.Status = ClaimStatus.Approved;
                ClaimMemory.ClaimList.Remove(claim);
                ClaimMemory.ClaimHistory.Add(claim);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Reject(int claimID)
        {
            var claim = ClaimMemory.ClaimList.FirstOrDefault(c => c.ClaimId == claimID);
            if (claim != null)
            {
                claim.Status = ClaimStatus.Rejected;
                ClaimMemory.ClaimList.Remove(claim);
                ClaimMemory.ClaimHistory.Add(claim);
            }
            return RedirectToAction("Index");
        }

    }
}
