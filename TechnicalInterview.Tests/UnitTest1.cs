using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using TechnicalInterview.Controllers;
using TechnicalInterview.Models;

namespace TechnicalInterview.Tests
{
    [TestFixture]
    public class CheckOTPControllerTests
    {
        private CheckOTPController _controller;

        [SetUp]
        public void Setup()
        {
            _controller = new CheckOTPController();
        }

        [Test]
        public void Verify_CorrectOTP_ReturnsCorrectMessage()
        { 
            var userModel = new UserModel
            {
                userId = "user1",
                dateAndTime = "12/07/2001 12:33:13",
                otp = "4f57ea"
            };

            var result = _controller.Verify(userModel) as ViewResult;
            var message = result?.ViewData["Message"] as string;

            Assert.IsNotNull(result);
            Assert.AreEqual("Correct OTP", message);
        }

        [Test]
        public void Verify_IncorrectOTP_ReturnsIncorrectMessage()
        {
            
            var userModel = new UserModel
            {
                userId = "testUser",
                dateAndTime = "01/01/2022 12:00:00",
                otp = "123456"
            };
            var result = _controller.Verify(userModel) as ViewResult;
            var message = result?.ViewData["Message"] as string;

            
            Assert.IsNotNull(result);
            Assert.AreEqual("Incorrect OTP", message);
        }

        [Test]
        public void GenerateOTP_ValidInputs_ReturnsGeneratedOTP()
        {
            
            var userId = "testUser";
            var dateAndTime = "01/01/2022 12:00:00";
            var result = _controller.GenerateOTP(userId, dateAndTime) as ContentResult;

            
            Assert.IsNotNull(result);
            Assert.IsFalse(string.IsNullOrEmpty(result.Content));
        }

        [Test]
        public void IsOTPValid_ValidOTPGenerationTime_ReturnsTrue()
        {
            // Arrange
            var otpGeneratedTime = System.DateTime.Now.AddSeconds(-25); // Generate OTP 25 seconds ago

            // Act
            var result = _controller.IsOTPValid(otpGeneratedTime);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void IsOTPValid_ExpiredOTPGenerationTime_ReturnsFalse()
        {
            // Arrange
            var otpGeneratedTime = System.DateTime.Now.AddSeconds(-35); // Generate OTP 35 seconds ago

            // Act
            var result = _controller.IsOTPValid(otpGeneratedTime);

            // Assert
            Assert.IsFalse(result);
        }   
    }
}
