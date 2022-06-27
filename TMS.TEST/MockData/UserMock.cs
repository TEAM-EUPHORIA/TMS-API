using System.Collections.Generic;
using TMS.BAL;
namespace TMS.TEST
{
    public static class UserMock
    {
        public static List<User> GetAllUserByRole()
        {
            return new List<User>(){
                new User(){
                    Id= 1,
                    FullName="Tom",
                },
                new User(){
                    Id= 2,
                    FullName="James"
                },
                new User(){
                    Id= 3,
                    FullName="White"
                },
            };
        }
        public static User GetUserById()
        {
            return new User ()
            {
                Id = 1,
                FullName= "Thomas"
            };
        }
    }
}