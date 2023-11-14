namespace HomeEducation.Application.Queries.UserManagementQuesries.Dtos;
public class UserProfile<T> where T : class
{
    public T User { get; set; }
}
