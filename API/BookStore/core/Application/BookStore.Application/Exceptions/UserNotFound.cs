namespace BookStore.Application.Exceptions;

public class UserNotFound : NotFoundException
{
    public UserNotFound(string s, string email) : base($"User  with  {email} not found.")
    {
    }
}