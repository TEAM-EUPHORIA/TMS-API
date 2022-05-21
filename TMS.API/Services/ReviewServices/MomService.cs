using Microsoft.EntityFrameworkCore;
using TMS.API.UtilityFunctions;
using TMS.BAL;

namespace TMS.API.Services
{
    public partial class ReviewService
    {
        public IEnumerable<MOM> GetListOfMomByUserId(int userId)
        {
            if (userId == 0) ServiceExceptions.throwArgumentExceptionForId(nameof(GetListOfMomByUserId));
            try
            {
                var result = _context.MOMs.Where(m=>m.OwnerId==userId).Include(m=>m.Review).Include(m=>m.OwnerId).Include(r=>r.Status);
                return result;
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.DbContextInjectionFailed(ex, _logger, nameof(ReviewService), nameof(GetListOfMomByUserId));
                throw;
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(ReviewService), nameof(GetListOfMomByUserId));
                throw;
            }
        }
        public MOM GetMomById(int momId)
        {
            if (momId == 0) ServiceExceptions.throwArgumentExceptionForId(nameof(GetMomById));
            try
            {
                return _context.MOMs.Where(m=>m.Id==momId).Include(m=>m.Review).Include(m=>m.Owner).Include(m=>m.Status).FirstOrDefault();
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.DbContextInjectionFailed(ex, _logger, nameof(ReviewService), nameof(GetMomById));
                throw;
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(ReviewService), nameof(GetMomById));
                throw;
            }
        }
        public Dictionary<string,string> CreateMom(MOM mom)
        {
             if (mom == null) ServiceExceptions.throwArgumentExceptionForObject(nameof(CreateMom), nameof(mom));
            var validation = Validation.ValidateMOM(mom,_context);
            if (validation.ContainsKey("IsValid"))
            {
                try
                {
                    SetUpMomDetails(mom);
                    CreateAndSaveMom(mom);
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.DbContextInjectionFailed(ex, _logger, nameof(ReviewService), nameof(CreateMom));
                    throw;
                }
                catch (Exception ex)
                {
                    TMSLogger.GeneralException(ex, _logger, nameof(ReviewService), nameof(CreateMom));
                    throw;
                }
            }
            return validation;
        }
        public Dictionary<string,string> UpdateMom(MOM mom)
        {
            if (mom == null) ServiceExceptions.throwArgumentExceptionForObject(nameof(UpdateMom), nameof(mom));
            var validation = Validation.ValidateMOM(mom,_context);
            if (validation.ContainsKey("IsValid"))
            {
                try
                {
                    var dbMom = _context.MOMs.Where(m=>m.Id==mom.Id).FirstOrDefault();
                    if (dbMom != null)
                    {
                        SetUpMomDetails(mom, dbMom);
                        UpdateAndSaveMom(dbMom);
                    }
                    validation.Add("Invalid Id","Not Found");
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.DbContextInjectionFailed(ex, _logger, nameof(ReviewService), nameof(UpdateMom));
                    throw;
                }
                catch (Exception ex)
                {
                    TMSLogger.GeneralException(ex, _logger, nameof(ReviewService), nameof(UpdateMom));
                    throw;
                }
            }
            return validation;
        }
        private void CreateAndSaveMom(MOM mom)
        {
            _context.MOMs.Add(mom);
            _context.SaveChanges();
        }
        private void SetUpMomDetails(MOM mom)
        {
            mom.CreatedOn = DateTime.UtcNow;
        }
        private void UpdateAndSaveMom(MOM mom)
        {
            _context.MOMs.Update(mom);
            _context.SaveChanges();
        }
        private void SetUpMomDetails(MOM mom, MOM dbMom)
        {
            dbMom.Agenda = mom.Agenda;
            dbMom.PurposeOfMeeting = mom.PurposeOfMeeting;
            dbMom.MeetingNotes = mom.MeetingNotes;
            dbMom.UpdatedOn = DateTime.UtcNow;
        }
    }
}