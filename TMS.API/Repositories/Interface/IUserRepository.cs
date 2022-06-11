using TMS.BAL;

namespace TMS.API.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsersByDeptandrole(int departmentId, int roleId, AppDbContext dbContext);
        User GetUserById(int id, AppDbContext dbContext);
        void CreateUser(User user, AppDbContext dbContext);
        void UpdateUser(User user, AppDbContext dbContext);
        void DisableUser(int userId, AppDbContext dbContext);
    }
}