﻿namespace Domain.Entities;

public class Account
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
    
    public string Login { get; set; } = string.Empty;
    
    public string Email { get; set; } = string.Empty;
    
    public string Password { get; set; } = string.Empty;
}