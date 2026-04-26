namespace mvc.Service.interfaces
{
    public interface ITokenService
    {
        string CreateToken(int userId, string email);
    }
}
