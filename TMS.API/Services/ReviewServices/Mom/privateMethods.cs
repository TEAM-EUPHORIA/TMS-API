using TMS.BAL;

namespace TMS.API.Services
{
    public partial class ReviewService
    {
        private void CreateAndSaveMom(MOM mom,AppDbContext dbContext)
        {
            dbContext.MOMs.Add(mom);
            dbContext.SaveChanges();
        }
        private void SetUpMomDetails(MOM mom)
        {
            mom.CreatedOn = DateTime.UtcNow;
        }
        private void UpdateAndSaveMom(MOM mom,AppDbContext dbContext)
        {
            dbContext.MOMs.Update(mom);
            dbContext.SaveChanges();
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