# OTP Generator and Verifier App

This is a web application that provides a simple OTP (One-Time Password) generator and verifier feature. Users can enter their user ID and a specific date and time to generate an OTP, and then verify the generated OTP.

## Features

- OTP Generation: Users can enter their user ID and a specific date and time to generate a unique OTP, initially the Date and Time field is going to be automatically filled with the current time, but it's an editable field.
- OTP Verification: Users can enter the generated OTP to verify its correctness.
- Time Limit: The generated OTP is valid for a duration of 30 seconds, after the 30seconds the OTP is going to be invalid.

## Technologies Used

- Backend Framework: ASP.NET Core
- Frontend Framework: Bootstrap
- Language: C#
- Hashing Algorithm: SHA256

