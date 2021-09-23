using MinimalAPI;

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

    public Task UpsertUsers()
    {
        var userDtos = _users;
        using (var db = new DishContext())
        {
            foreach (var userDto in userDtos)
            {
                var user = db.Users
                    .FirstOrDefault(u => u.Name == userDto.UserName);
                if (user == null)
                {
                    user = db.Add(entity: new User
                    {
                        Name = userDto.UserName,
                        Password = userDto.Password
                    }
                        ).Entity;
                }
                else
                {
                    user.Name = userDto.UserName;
                    user.Password = userDto.Password;
                }
            }
            db.SaveChanges();
        }
        return Task.CompletedTask;
    }
}