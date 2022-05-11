// using Microsoft.EntityFrameworkCore;
// using TMS.API.UtilityFunctions;
// using TMS.BAL;

// namespace TMS.API.Services
// {
//     public class AssignmentService
//     {
//         private readonly AppDbContext _context;
//         private readonly ILogger<AssignmentService> _logger;
//         public AssignmentService(AppDbContext context, ILogger<AssignmentService> logger)
//         {
//             _context = context ?? throw new ArgumentNullException(nameof(context));
//             _logger = logger ?? throw new ArgumentNullException(nameof(logger));
//         }

//         //     public User GetUserById(int id)
//         //     {
//         //         if (id == 0) ServiceExceptions.throwArgumentExceptionForId(nameof(GetUserById));
//         //         try
//         //         {
//         //             var dbUser = _context.Users.Find(id);
//         //             if (dbUser != null)
//         //             {
//         //                 string base64String = Convert.ToBase64String(dbUser.Image, 0, dbUser.Image.Length);
//         //                 User result = new User();
//         //                 result.base64Header = result.base64Header + base64String;
//         //                 if (dbUser.DepartmentId != null)
//         //                 {
//         //                     result = _context.Users.Where(u => u.Id == id).Include("Role").Include("Department").FirstOrDefault();
//         //                     return result;
//         //                 }
//         //                 else
//         //                 {
//         //                     result = _context.Users.Where(u => u.Id == id).Include("Role").FirstOrDefault();
//         //                     return result;
//         //                 }
//         //             }
//         //             return null;
//         //         }
//         //         catch (InvalidOperationException ex)
//         //         {
//         //             TMSLogger.DbContextInjectionFailed(ex, _logger, nameof(AssignmentService), nameof(GetUserById));
//         //             throw ex;
//         //         }
//         //         catch (Exception ex)
//         //         {
//         //             TMSLogger.EfCoreExceptions(ex, _logger, nameof(AssignmentService), nameof(GetUserById));
//         //             throw ex;
//         //         }
//         //     }
//         //     public bool CreateUser(User user)
//         //     {
//         //         if (user == null) ServiceExceptions.throwArgumentExceptionForObject(nameof(CreateUser), nameof(user));
//         //         try
//         //         {
//         //             string Imagedate = "";
//         //             var newUserEmployeeId = _context.Users.ToList().Count() + 1;

//         //             user.EmployeeId = ($"ACE{user.RoleId}{newUserEmployeeId}");

//         //             user.Password = HashPassword.Sha256(user.Password);

//         //             Imagedate = ImageService.Getbase64String(user.base64Header);
//         //             user.base64Header = ImageService.Getbase64Header(user.base64Header);
//         //             user.Image = System.Convert.FromBase64String(Imagedate);

//         //             user.isDisabled = false;
//         //             user.CreatedOn = DateTime.Now;

//         //             _context.Users.Add(user);
//         //             _context.SaveChanges();
//         //             return true;
//         //         }
//         //         catch (InvalidOperationException ex)
//         //         {
//         //             TMSLogger.DbContextInjectionFailed(ex, _logger, nameof(AssignmentService), nameof(CreateUser));
//         //             throw ex;
//         //         }
//         //         catch (Exception ex)
//         //         {
//         //             TMSLogger.EfCoreExceptions(ex, _logger, nameof(AssignmentService), nameof(CreateUser));
//         //             throw ex;
//         //         }

//         //     }

//         //     public bool UpdateUser(User user)
//         //     {
//         //         if (user == null) ServiceExceptions.throwArgumentExceptionForObject(nameof(UpdateUser), nameof(user));
//         //         try
//         //         {
//         //             var dbUser = _context.Users.Find(user.Id);
//         //             if (dbUser != null)
//         //             {
//         //                 string Imagedate = "";
//         //                 dbUser.EmployeeId = user.EmployeeId;

//         //                 dbUser.Password = HashPassword.Sha256(user.Password);

//         //                 Imagedate = ImageService.Getbase64String(user.base64Header);
//         //                 dbUser.base64Header = ImageService.Getbase64Header(user.base64Header);
//         //                 dbUser.Image = System.Convert.FromBase64String(Imagedate);

//         //                 dbUser.isDisabled = false;
//         //                 dbUser.UpdatedOn = DateTime.Now;

//         //                 _context.Update(dbUser);
//         //                 _context.SaveChanges();
//         //                 return true;
//         //             }
//         //             return false;
//         //         }
//         //         catch (InvalidOperationException ex)
//         //         {
//         //             TMSLogger.DbContextInjectionFailed(ex, _logger, nameof(AssignmentService), nameof(UpdateUser));
//         //             throw ex;
//         //         }
//         //         catch (Exception ex)
//         //         {
//         //             TMSLogger.EfCoreExceptions(ex, _logger, nameof(AssignmentService), nameof(UpdateUser));
//         //             throw ex;
//         //         }
//         //     }



//         //     public bool DisableUser(int userId)
//         //     {
//         //         if (userId == 0) ServiceExceptions.throwArgumentExceptionForId(nameof(DisableUser));
//         //         try
//         //         {
//         //             var dbUser = _context.Users.Find(userId);
//         //             if (dbUser != null)
//         //             {
//         //                 dbUser.isDisabled = true;
//         //                 dbUser.UpdatedOn = DateTime.Now;
//         //                 _context.Update(dbUser);
//         //                 _context.SaveChanges();
//         //                 return true;
//         //             }
//         //             return false;
//         //         }
//         //         catch (InvalidOperationException ex)
//         //         {
//         //             TMSLogger.DbContextInjectionFailed(ex, _logger, nameof(AssignmentService), nameof(DisableUser));
//         //             throw ex;
//         //         }
//         //         catch (Exception ex)
//         //         {
//         //             TMSLogger.EfCoreExceptions(ex, _logger, nameof(AssignmentService), nameof(DisableUser));
//         //             throw ex;
//         //         }
//         //     }
//     }
// }

