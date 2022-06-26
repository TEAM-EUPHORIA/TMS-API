using System.Collections.Generic;
using TMS.BAL;
namespace TMS.TEST
{
    public static class DeparmentMock
    {
        public static List<Department> GetDepartments()
        {
            return new List<Department>(){
                new Department(){
                    Id= 1,
                    Name=".NET"
                },
                new Department(){
                    Id= 2,
                    Name="JAVA"
                },
                new Department(){
                    Id= 3,
                    Name="LAMP"
                },
            };
        }
        public static Department GetDepartment()
        {
            return new Department()
            {
                Id = 1,
                Name = "JAVA"
            };
        }
    }
}