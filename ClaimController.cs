using Microsoft.AspNetCore.Mvc;
using CMCS_Application.Models;
using Microsoft.AspNetCore.Identity;
using System.Runtime.CompilerServices;

namespace CMCS_Application.Controllers
{
    public class ClaimController : Controller
    {
        //generate unique claim id from 1
        private static int _claimUniqueID = 1;
        //allowed file type definition
        private readonly string[] allowedFiles = { ".pdf", ".docx", ".xlsx" };
        //max file size - 5mb
        private const long fileSizeLimit = 5* 1024 * 1024;


        [HttpGet]
        public IActionResult ClaimForm()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ClaimForm(Claim claim, List<IFormFile> files)
        {
            claim.ClaimId = _claimUniqueID++;

            foreach (var file in files)
            {
                // Check file extension
                if (!allowedFiles.Contains(Path.GetExtension(file.FileName).ToLower()))
                {
                    ViewBag.Error = "Please note that only .pdf, .docx, and .xlsx. files are allowed.";
                    return View(claim);
                }

                // Check file size
                if (file.Length > fileSizeLimit)
                {
                    ViewBag.Error = "File size must be less than 5MB.";
                    return View(claim);
                }
            }
            claim.Documents = SaveFile(files);

            ClaimMemory.ClaimList.Add(claim);
            ClaimMemory.SubmittedClaim.Add(claim);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(ClaimMemory.SubmittedClaim);
        }

        public List<string> SaveFile(List<IFormFile> files)
        {
            var filePaths = new List<string>();
            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var filePath = Path.Combine(uploadPath, file.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(stream); 
                    }
                    filePaths.Add(fileName);
                }
            }
            return filePaths;
        }
        
        public IActionResult ClaimHistory()
        {
            var history = ClaimMemory.ClaimHistory;
            return View(history);
        }
    }

}
   