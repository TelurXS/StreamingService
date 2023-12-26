namespace Domain.Entities;

/// <summary>
/// Representation of the account entity in the database
/// </summary>
public class Account
{
    /// <summary>
    /// Account identifier
    /// </summary>
    public required Guid Id { get; set; }

    /// <summary>
    /// Account name
    /// </summary>
    public required string Name { get; set; }
    
    /// <summary>
    /// Account login
    /// </summary>
    public required string Login { get; set; }
    
    /// <summary>
    /// Account email
    /// </summary>
    public required string Email { get; set; }
    
    /// <summary>
    /// Hashed account password
    /// </summary>
    public required string Password { get; set; }
    
    public required ICollection<Rate> Rates { get; set; }
}