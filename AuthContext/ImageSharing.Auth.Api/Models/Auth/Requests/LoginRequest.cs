﻿namespace ImageSharing.Auth.Api.Models.Auth.Requests;

public class LoginRequest
{
    public string? Email { get; set; }
    public string? Password { get; set; }
}