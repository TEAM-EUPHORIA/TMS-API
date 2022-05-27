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
        private static void AddEnteryForUsers(bool firstUser,bool secondUser)
        {
            if(!firstUser) AddEntery("Invalid Id","Can't find the User");
            if(!secondUser) AddEntery("Invalid Id","Can't find the User");
        }
        private static void AddEnteryForUserAndReview(bool userExists,bool reviewExists)
        {
            if(!userExists) AddEntery("Invalid Id","Can't find the User");
            if(!reviewExists) AddEntery("Invalid Id","Can't find the Review");
        }
        private static void AddEnteryForCourseAndUser(bool courseExists,bool userExists,int courseId)
        {
            if(!courseExists && courseId != 0) AddEntery("Invalid Id","Can't find the Course");
            if(!userExists) AddEntery("Invalid Id","Can't find the User");
        }
        private static void AddEnteryForCourseTopicAndUser(bool courseExists,bool topicExists,bool userExists)
        {
            if(!courseExists) AddEntery("Invalid Id","Can't find the Course");
            if(!topicExists) AddEntery("Invalid Id","Can't find the Topic in the Course");
            if(!userExists) AddEntery("Invalid Id","Can't find the User");
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