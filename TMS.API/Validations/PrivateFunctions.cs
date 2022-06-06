using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMS.API.ViewModels;
using TMS.BAL;

namespace TMS.API
{
    public static partial class Validation
    {
        private static void AddEntery(string key)
        {
            result.Add($"{key}", $"InValid data");
        }
        private static void AddEntery(string key,string value)
        {
            result.Add($"{key}", $"{value}");
        }
        private static void AddEnteryForUsers(bool reviewerId,bool traineeId)
        {
            if(!reviewerId) AddEntery($"{nameof(reviewerId)}","Can't find the User");
            if(!traineeId)AddEntery($"{nameof(reviewerId)}","Can't find the User");
        }
        private static void AddEnteryForUserAndReview(bool userExists,bool reviewExists, bool momExists)
        {
            if(!userExists) AddEntery("UserExists","Can't find the User");
            if(!reviewExists) AddEntery("reviewExists","Can't find the Review or Review was not completed");
            if(momExists)AddEntery("momExists","MOM Already Exists");
        }
        private static void AddEnteryForCourseAndUser(bool courseExists,bool userExists,int courseId)
        {
            if(!courseExists && courseId != 0) AddEntery("courseExists","Can't find the Course");
            if(!userExists) AddEntery("userExists","Can't find the User");
            

        }
         private static void AddEnteryForCourseAndUserWithDept(bool courseExists,bool userExists,bool departmentExists,int courseId,bool courseValidationExists)
        {
            if(!courseExists && courseId != 0) AddEntery("courseExists","Can't find the Course");
            if(!userExists) AddEntery("UserExists","Can't find the User");
            if(!departmentExists)AddEntery("departmetExists","Can't find the Department");
            if(courseValidationExists)AddEntery("courseValidationExists","Alraedy course Exists for this Department");
            
        }
         private static void AddEnteryForValidateTopic(bool sa,bool courseExists,int courseId)
        {
            if(!courseExists && courseId != 0) AddEntery("courseExists","Can't find the Course");
            if(sa)AddEntery("Invalid Id","Alraedy Topic Exists for this Course");

        }
        private static void AddEnteryForTopic(bool sa)
        {
            
            if(sa)AddEntery("Invalid Id","Alraedy Topic Exists for this Course");
        }
        private static void AddEnteryForCourseTopicAndUser(bool courseExists,bool topicExists,bool userExists)
        {
            if(!courseExists) AddEntery("courseExists","Can't find the Course");
            if(!topicExists) AddEntery("topicExists","Can't find the Topic in the Course");
            if(!userExists) AddEntery("userExists","Can't find the User");
        }
        private static void checkIdForCourseTopicAndUser(int courseId,int topicId,int userId)
        {
            checkIdAndAddEntery(courseId,"Course Id");
            checkIdAndAddEntery(topicId,"Topic Id");
            checkIdAndAddEntery(userId,"Owner Id");
        }
        private static void validateAndAddEntery(string input,string regex)
        {
            bool result = Regex.Match(input,regex).Success;
            if(!result)AddEntery(input,"Invalid Data");
        }
        private static void checkIdAndAddEntery(int id,string nameOfKey)
        {
            if(id==0) AddEntery(nameOfKey);
        }
    }
}