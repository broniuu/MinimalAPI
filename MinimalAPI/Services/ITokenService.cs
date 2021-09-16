public interface ITokenService
{
    string BuildToken(string key, string issuer, UserDto user);
    string FindRole(string stream, string roleName);
}