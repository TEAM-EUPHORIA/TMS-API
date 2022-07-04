using System.Collections.Generic;
using TMS.API.ViewModels;
using TMS.BAL;
namespace TMS.TEST
{
     public static class AuthMock
     {
    //     // public static List<User> GetUsers()
    //     // {
    //     //     return new List<User>(){

    //     //         new User(){

    //     //             UserName="David",
    //     //             Password="asd123"

    //     //         },

    //     //         new User(){
    //     //             UserName="Dyson",
    //     //             Password="asd123"

    //     //         },

    //     //         new User(){

    //     //             UserName="Tylor",
    //     //             Password="asd123"
    //     //         },
    //     //     };
    // }
       public static LoginModel GetUseCredentials()
        {
            return new LoginModel()
                {  
                    Email="david@mail.com",
                    Password="abcd1234"
                };

        }
    }
}
