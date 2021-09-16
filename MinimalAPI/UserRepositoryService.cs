public class UserRepositoryService : IUserRepositoryService
{
    private List<UserDto> _users => new()
    {
        new("Anu Viswan", "anu"),
        new("Jia Anu", "jia"),
        new("Naina Anu", "naina"),
        new("Sreena Anu", "sreena"),
        new("Filip", "1234"),
    };
    public UserDto GetUser(UserModel userModel)
    {
        return _users.FirstOrDefault(x => string.Equals(x.UserName, userModel.UserName) && string.Equals(x.Password, userModel.Password));
    }
}