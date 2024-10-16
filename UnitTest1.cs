using Microsoft.VisualStudio.TestTools.UnitTesting;
using CMCS_Application.Controllers;  
using CMCS_Application.Models;      
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace CMCSTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestSubmitClaim()
        {
            var controller = new ClaimController();
            var claim = new Claim
            {
                LecturerName = "Yadav Priaram",
                LecturerEmail = "yadavp@gmail.com",
                HoursWorked = 15,
                HourlyRate = 50,
                AdditionalNotes = "Test notes",
            };
            var files = new List<IFormFile>();

            var result = controller.ClaimForm(claim, files);

            //assert
            Assert.IsNotNull(result );
            Assert.AreEqual(1, ClaimMemory.ClaimList.Count );
        }

        [TestMethod]
        public void TestFileValidation()
        {
            var controller = new ClaimController();
            var files = new List<IFormFile>
            {
                new FormFile(null, 0,0, "TestFile", "test.sql") //incorrect file type to test restrictions
            };
            var claim = new Claim
            {
                LecturerName = "Yadav Priaram",
                LecturerEmail = "yadavpri@gmail.com"
            };

            var result = controller.ClaimForm(claim, files);

            //assert
            Assert.IsNotNull(result );
            Assert.AreEqual("Please note that only .pdf, .docx, and .xlsx. files are allowed.", controller.ViewBag.Error);
        }

        [TestMethod]
        public void TestVerifyClaim()
        {
            var claim = new Claim
            {
                ClaimId = 1,
                LecturerName = "Yadav Priaram",
                LecturerEmail = "yadavp@gmail.com",
                HoursWorked = 15,
                HourlyRate = 50,
                Status = ClaimStatus.Approved
            };

            ClaimMemory.ClaimList.Add(claim);
            claim.Status = ClaimStatus.Approved; //simulate approved claim
            //assert
            Assert.AreEqual(ClaimStatus.Approved, ClaimMemory.ClaimList[0].Status);
            
        }

        [TestMethod]
        public void TestClaimHistory()
        {
            var claim = new Claim
            {
                ClaimId= 2,
                LecturerName = "Yadav Priaram",
                LecturerEmail = "yadavp@gmail.com",
                HoursWorked = 15,
                HourlyRate = 50,
                Status= ClaimStatus.Approved
            };

            ClaimMemory.ClaimList.Add(claim);
            ClaimMemory.ClaimHistory.Add(claim);

            var controller = new ClaimController();
            var results = controller.ClaimHistory();

            Assert.IsNotNull(results);
            Assert.AreEqual(1, ClaimMemory.ClaimHistory.Count);
        }
    }
}