using TMS.BAL;

namespace TMS.API.Services
{
    public partial class ReviewService
    {
        /// <summary>
        /// used to SetUpMomDetails to user.
        /// </summary>
        /// <param name="mom"></param>
        /// <param name="createdBy"></param>
        private static void SetUpMomDetails(MOM mom, int createdBy)
        {
            mom.CreatedOn = DateTime.Now;
            mom.CreatedBy = createdBy;
        }

        /// <summary>
        /// used to SetUpMomDetails to user.
        /// </summary>
        /// <param name="mom"></param>
        /// <param name="dbMom"></param>
        /// <param name="updatedBy"></param>

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