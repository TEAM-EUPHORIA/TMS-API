using System.Collections.Generic;
using TMS.BAL;
namespace TMS.TEST
{
    public static class RoleMock
    {
        public static List<Role> GetRoles()
        {
            return new List<Role>(){
                new Role(){
                    Id= 1,
                    Name="Trainee"
                },
                new Role(){
                    Id= 2,
                    Name="Trainer"
                },
                new Role(){
                    Id= 3,
                    Name="Reviewer"
                },
                new Role(){
                    Id= 3,
                    Name="Co-Ordinator"
                },
            };
        }
        public static Role GetRole()
        {
            return new Role()
            {
                Id = 1,
                Name = "JAVA"
            };

        }
    }
}