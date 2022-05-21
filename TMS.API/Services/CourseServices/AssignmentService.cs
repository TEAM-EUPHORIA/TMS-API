using Microsoft.EntityFrameworkCore;
using TMS.API.UtilityFunctions;
using TMS.BAL;

namespace TMS.API.Services
{
    public partial class CourseService
    {
        private static void prepareAssignment(Assignment assignment)
        {
            File PDF = FileService.GetBase64HeaderAndByteArray(assignment.Base64);
            assignment.Base64 = PDF.header;
            assignment.Document = PDF.bytes;
        }
        private static void prepareAssignment(Assignment assignment, Assignment dbAssignment)
        {
            File PDF = FileService.GetBase64HeaderAndByteArray(assignment.Base64);
            dbAssignment.Base64 = PDF.header;
            dbAssignment.Document = PDF.bytes;
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
                TMSLogger.DbContextInjectionFailed(ex, _logger, nameof(CourseService), nameof(GetAssignmentsByTopicId));
                throw;
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(CourseService), nameof(GetAssignmentsByTopicId));
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
                TMSLogger.DbContextInjectionFailed(ex, _logger, nameof(CourseService), nameof(GetAssignmentsByTopicId));
                throw;
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(CourseService), nameof(GetAssignmentsByTopicId));
                throw;
            }
        }
        public Dictionary<string, string> CreateAssignment(Assignment assignment)
        {
            if (assignment == null) ServiceExceptions.throwArgumentExceptionForObject(nameof(CreateAssignment), nameof(assignment));
            var validation = Validation.ValidateAssignment(assignment,_context);
            if (validation.ContainsKey("IsValid"))
            {
                try
                {
                    var result = _context.Assignments.Any(a => a.TopicId == assignment.TopicId && a.OwnerId == assignment.OwnerId);
                    if (!result)
                    {
                        prepareAssignment(assignment);
                        _context.Add(assignment);
                        _context.SaveChanges();
                    }
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
            return validation;
        }
        public Dictionary<string, string> UpdateAssignment(Assignment assignment)
        {
            if (assignment == null) ServiceExceptions.throwArgumentExceptionForObject(nameof(UpdateAssignment), nameof(assignment));
            var validation = Validation.ValidateAssignment(assignment,_context);
            if (validation.ContainsKey("IsValid"))
            {
                var dbAssignment = _context.Assignments.Find(assignment.Id);
                if (dbAssignment != null)
                {
                    try
                    {
                        var result = _context.Assignments.Any(a => a.TopicId == assignment.TopicId && a.OwnerId == assignment.OwnerId);
                        if (result)
                        {
                            prepareAssignment(assignment, dbAssignment);
                            _context.Update(dbAssignment);
                            _context.SaveChanges();
                        }
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
            }
            return validation;
        }
    }
}

