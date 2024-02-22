namespace Metflix.Core.Models;

public class DBProfileModel
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string UserName { get; set; }
    public string Name { get; set; }
    public string FirstName { get; set; }
    public string SecondName { get; set; }
    public string ProfileImage { get; set; }
    public DateTime BirthDate { get; set; }
}