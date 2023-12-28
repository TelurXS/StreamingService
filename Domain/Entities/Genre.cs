﻿namespace Domain.Entities;

public class Genre
{
    public Guid Id { get; set; }
    
    public required string Name { get; set; }

    public ICollection<Title> Titles { get; set; } = default!;
}