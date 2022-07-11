using TMS.BAL;

namespace TMS.API.Services
{
    public partial class ReviewService
    {
        private static void SetUpMomDetails(Mom mom)
        {
            mom.CreatedOn = DateTime.Now;
        }
        private static void SetUpMomDetails(Mom mom, Mom dbMom)
        {
            dbMom.Agenda = mom.Agenda;
            dbMom.PurposeOfMeeting = mom.PurposeOfMeeting;
            dbMom.MeetingNotes = mom.MeetingNotes;
            dbMom.UpdatedOn = DateTime.Now;
        }
    }
}