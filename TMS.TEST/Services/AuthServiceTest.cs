using System.Collections.Generic;
using TMS.API.Services;
using Moq;
using TMS.API.Controllers;
using Xunit;
using TMS.BAL;
using TMS.API.Repositories;
using Moq;
using Xunit;
using TMS.BAL;
using TMS.API.ViewModels;

namespace TMS.TEST.Services
{
    public class AuthServiceTest
    {
       private readonly Mock<IUnitOfWork> _unitOfWork = new();

       private readonly IAuthService _authService;

        private readonly Dictionary<string,string> result = new();

       private readonly object _unityService;
       
       private readonly LoginModel _user = AuthMock.GetUseCredentials();
       private readonly User resultUser = new User(){
        Email="david@gmail.com",
        FullName="David",
        Role = new Role(){
          Name="Trainer"
        },
        RoleId = 4,
        Id = 1
       };
        
        
        
       private void token()
        {
            result.Add("token", "");
        }
        private void AddIsValid()
        {
            result.Add("IsValid", "true");
        }
       
        public AuthServiceTest()
       {
         _authService = new AuthService(_unitOfWork.Object);
          
       } 
       
    [Fact]
    public void Login()
    {
      AddIsValid();
     _unitOfWork.Setup(obj => obj.Validation.ValidateLoginDetails(_user)).Returns(result);
      _unitOfWork.Setup(obj => obj.Users.GetUserByEmailAndPassword(_user)).Returns(resultUser);
     _unitOfWork.Setup(obj => obj.Complete());
     var Result = _authService.Login(_user);
     result.Clear();
     token();
     Assert.Equal(result.ContainsKey("token"),Result.ContainsKey("token"));
    }
    
    }
}
