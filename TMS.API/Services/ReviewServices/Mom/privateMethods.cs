using TMS.BAL;

namespace TMS.API.Services
{
    public partial class ReviewService
    {
        private static void SetUpMomDetails(MOM mom, int createdBy)
        {
            mom.CreatedOn = DateTime.Now;
            mom.CreatedBy = createdBy;
        }
        private static void SetUpMomDetails(MOM mom, MOM dbMom, int updatedBy)
        {
            dbMom.Agenda = mom.Agenda;
            dbMom.PurposeOfMeeting = mom.PurposeOfMeeting;
            dbMom.MeetingNotes = mom.MeetingNotes;
            dbMom.UpdatedOn = DateTime.Now;
            dbMom.UpdatedBy = updatedBy;
        }
    }
}