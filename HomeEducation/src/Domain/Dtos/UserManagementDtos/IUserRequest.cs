namespace HomeEducation.Domain.Dtos.UserManagementDtos;

public interface IUserRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
    public string FirstName { set; get; }
    public string LastName { set; get; }
    public string? Image {  get; set; }
}