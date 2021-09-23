public interface IUserRepositoryService
{
    UserDto GetUser(UserModel userModel);

    Task UpsertUsers();
}