namespace TMS.API
{
    public static partial class Validation
    {
        static Dictionary<string,string> result = new Dictionary<string, string>();
        static string fullNameValidation = @"^(?!([ ])\1)(?!.*([A-Za-z])\2{2})\w[a-zA-Z\s]*$",
         nameValidation = @"^(?!([ ])\1)(?!.*([A-Za-z0\d])\2{2})\w[a-zA-Z&.#\d\s]*$",
         userNameValidation = @"^(?!([ ])\1)(?!.*([A-Za-z])\2{2})\w[a-zA-Z.\s]*$",
         emailValidation = @"^([0-9a-zA-Z.]){3,}@[a-zA-z]{3,}(.[a-zA-Z]{2,}[a-zA-Z]*){0,}$",
         passwordValidation = @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$",
         Image = @"^data:image\/[a-zA-Z]+;base64,",
         dateValidation = @"^\d{1,2}-\d{1,2}-\d{4}$",

         timeValidation =  @"^(1[0-2]|0?[1-9]):([0-5]?[0-9])\s(●?[AP]M)?$",
         //timeValidation = @"((\d{0}[0-9]|\d{0}[1]\d{0}[0-2])(\:)\d{0}[0-5]\d{0}[0-9]\s(AM|PM))",
         modeValidation = @"^((online)|(offline)|(Online)|(Offilne)){1}$",
         //durationValidation = @"^\d+ ((hr)|(hrs)|(mins)){1}$",
         durationValidation = @"^(\d+ ((hr)|(hrs)|(mins)){1}$)|(\d+ ((hr)|(hrs)){1})\s(\d+ ((min)|(mins)){1})$",
        
         contentValidation = @"([A-Za-z0-9!?@#$%^&*()\-+\\\/.,:;'{}\[\]<>~]{20,1000})*$",
         feedbackValidation = @"^(?!([ ])\1)(?!.*([A-Za-z])\2{2})\w[a-zA-Z.,\s]{5,100}$",
         base64Validation = @"^data:application\/pdf;base64,";
        static bool attendanceExists = false,
         assignmentExists = false,
         courseUsertExists = false,
         courseExists = false,
         courseValidationExists=false,
         topicValidationExists=false,
         departmentExists = false,
         momExists = false,
         reviewExists = false,
         reviewStatusExists = false,
         topicExists = false,
         traineeFeedbackExists = false,
         courseFeedbackExists = false,
         userExists = false,
         revieweExists=false,
         reviewerAvailabilityExists=false,
         traineeAvailabilityExists=false,
         traineeExists = false;
    }
        
}