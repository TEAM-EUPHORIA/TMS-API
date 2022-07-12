using TMS.BAL;

namespace TMS.API.Services
{
    public partial class ReviewService
    {
<<<<<<< HEAD
        private static void SetUpMomDetails(MOM mom, int createdBy)
=======
        private static void SetUpMomDetails(Mom mom)
>>>>>>> 985156ff0e154d4657b256b533b831ed906b2839
        {
            mom.CreatedOn = DateTime.Now;
            mom.CreatedBy = createdBy;
        }
<<<<<<< HEAD
        private static void SetUpMomDetails(MOM mom, MOM dbMom, int updatedBy)
=======
        private static void SetUpMomDetails(Mom mom, Mom dbMom)
>>>>>>> 985156ff0e154d4657b256b533b831ed906b2839
        {
            dbMom.Agenda = mom.Agenda;
            dbMom.PurposeOfMeeting = mom.PurposeOfMeeting;
            dbMom.MeetingNotes = mom.MeetingNotes;
            dbMom.UpdatedOn = DateTime.Now;
            dbMom.UpdatedBy = updatedBy;
        }
    }
}