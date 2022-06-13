using TMS.BAL;

namespace TMS.API.Services
{
    public partial class ReviewService
    {
        private void SetUpMomDetails(MOM mom)
        {
            mom.CreatedOn = DateTime.Now;
        }
        private void SetUpMomDetails(MOM mom, MOM dbMom)
        {
            dbMom.Agenda = mom.Agenda;
            dbMom.PurposeOfMeeting = mom.PurposeOfMeeting;
            dbMom.MeetingNotes = mom.MeetingNotes;
            dbMom.UpdatedOn = DateTime.Now;
        }
    }
}