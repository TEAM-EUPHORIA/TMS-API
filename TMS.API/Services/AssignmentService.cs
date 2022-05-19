using Microsoft.EntityFrameworkCore;
using TMS.API.UtilityFunctions;
using TMS.BAL;

namespace TMS.API.Services
{
    public class AssignmentService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<AssignmentService> _logger;
        public AssignmentService(AppDbContext context, ILogger<AssignmentService> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        private static void prepareAssignment(Assignment assignment)
        {
            File PDF = FileService.GetBase64HeaderAndByteArray(assignment.Base64);
            assignment.Base64 = PDF.header;
            assignment.Document = PDF.bytes;
        }
        public IEnumerable<Assignment> GetAssignmentsByTopicId(int id)
        {
            if (id == 0) ServiceExceptions.throwArgumentExceptionForId(nameof(GetAssignmentsByTopicId));
            try
            {
                var result = _context.Topics.Where(t => t.Id == id)
                    .Include(t => t.Assignments).ThenInclude(a => a.Owner).ThenInclude(u => u.Role)
                    .Include(t => t.Assignments).ThenInclude(a => a.Status).FirstOrDefault();

                return result.Assignments;
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.DbContextInjectionFailed(ex, _logger, nameof(AssignmentService), nameof(GetAssignmentsByTopicId));
                throw;
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(AssignmentService), nameof(GetAssignmentsByTopicId));
                throw;
            }
        }
        public Assignment GetAssignmentByTopicIdAndOwnerId(int tid, int oid)
        {
            if (tid == 0 || oid == 0) ServiceExceptions.throwArgumentExceptionForId(nameof(GetAssignmentsByTopicId));
            try
            {
                var result = _context.Assignments.Where(a => a.TopicId == tid && a.OwnerId == oid).Include(a => a.Status).FirstOrDefault();
                return result;
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.DbContextInjectionFailed(ex, _logger, nameof(AssignmentService), nameof(GetAssignmentsByTopicId));
                throw;
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(AssignmentService), nameof(GetAssignmentsByTopicId));
                throw;
            }
        }
        public bool CreateAssignment(Assignment assignment)
        {
            if (assignment == null) ServiceExceptions.throwArgumentExceptionForObject(nameof(CreateAssignment), nameof(assignment));
            if (assignment.TopicId == 0) ServiceExceptions.throwArgumentExceptionForObject(nameof(CreateAssignment), nameof(assignment));
            try
            {
                var result = _context.Assignments.Any(a => a.TopicId == assignment.TopicId && a.OwnerId == assignment.OwnerId);
                if (!result)
                {
                    prepareAssignment(assignment);
                    _context.Add(assignment);
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.DbContextInjectionFailed(ex, _logger, nameof(UserService), nameof(CreateAssignment));
                throw;
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(UserService), nameof(CreateAssignment));
                throw;
            }
        }
        public bool UpdateAssignment(Assignment assignment)
        {
            if (assignment == null) ServiceExceptions.throwArgumentExceptionForObject(nameof(UpdateAssignment), nameof(assignment));
            if (assignment.Id == 0 || assignment.TopicId == 0) ServiceExceptions.throwArgumentExceptionForObject(nameof(UpdateAssignment), nameof(assignment));
            try
            {
                prepareAssignment(assignment);
                _context.Update(assignment);
                _context.SaveChanges();
                return true;
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.DbContextInjectionFailed(ex, _logger, nameof(UserService), nameof(UpdateAssignment));
                throw;
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(UserService), nameof(UpdateAssignment));
                throw;
            }
        }
    }
}

