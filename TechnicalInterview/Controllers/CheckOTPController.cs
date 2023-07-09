using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using TechnicalInterview.Models;
using NUnit.Framework;

namespace TechnicalInterview.Controllers
{
    public class CheckOTPController : Controller
    {
        private const int OTPValidityDurationSeconds = 30;
        private static DateTime? otpGeneratedTime = null;

        public IActionResult CheckOTP()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Verify(UserModel usr)
        {
            if (otpGeneratedTime.HasValue && usr.otp == GenerateOneTimePassword(usr.userId, usr.dateAndTime) && IsOTPValid(otpGeneratedTime.Value))
            {
                ViewBag.Message = "Correct OTP";
            }
            else
            {
                ViewBag.Message = "Incorrect OTP";
            }

            return View("CheckOTP");
        }


        // Generates an OTP based on the user ID and date and time
        public IActionResult GenerateOTP(string userId, string dateAndTime)
        {
            string otp = GenerateOneTimePassword(userId, dateAndTime);
            otpGeneratedTime = DateTime.Now;
            return Content(otp);
        }

        private string GenerateOneTimePassword(string userId, string dateAndTime)
        {
            //concatenates the userId and dateAndTime strings
            string combinedString = userId + dateAndTime;


            // Generate a SHA256 hash of the combined string
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(combinedString));
                string otp = ByteArrayToHexString(hashBytes).Substring(0, 6); // Take the first 6 characters as OTP

                return otp;
            }
        }


        // Converts a byte array to a hexadecimal string representation, used to convert the otp from the hash byte form to a hexadecimal form
        private string ByteArrayToHexString(byte[] bytes)
        {
            StringBuilder hex = new StringBuilder(bytes.Length * 2);
            foreach (byte b in bytes)
            {
                hex.AppendFormat("{0:x2}", b);
            }
            return hex.ToString();
        }


        // Checks if the OTP is valid based on the OTP generation time(30 seconds validity), it's public for testing resons and doesn't really impacts the scurtiy since I pass only the time of the otp
        public bool IsOTPValid(DateTime otpGeneratedTime)
        {
            DateTime currentDateTime = DateTime.Now;
            TimeSpan timeDifference = currentDateTime - otpGeneratedTime;
            return timeDifference.TotalSeconds <= OTPValidityDurationSeconds;
        }
        
        
    }
}
