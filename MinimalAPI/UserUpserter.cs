
namespace MinimalAPI;
public class UserUpserter
{
    public Task UpsertUsers(IEnumerable<UserDto> userDtos)
    {
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
