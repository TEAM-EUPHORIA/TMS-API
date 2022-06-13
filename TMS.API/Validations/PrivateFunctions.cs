using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMS.API.ViewModels;
using TMS.BAL;

namespace TMS.API
{
    public partial class Validation
    {
        public void AddEntery(string key,string value)
        {
            result.Add(key,value);
        }
        public void AddEnteryValidateCourseUser(bool courseExists,bool userexists)
        {
            if(!courseExists) AddEntery("courseId","Can't find the course");
            if(!userExists) AddEntery("userId, roleId","Can't find the user");
        }
        public void AddEnteryValidateAssignment(bool courseExists,bool topicExists,bool userexists)
        {
            if(!courseExists) AddEntery("courseId","Can't find the course");
            if(!topicExists) AddEntery("topicId","Can't find the topic");
            if(!userExists) AddEntery("ownerId","Can't find the user");
        }
        public void AddEnteryValidateAttendance(bool courseExists,bool topicExists,bool userexists)
        {
            if(!courseExists) AddEntery("courseId","Can't find the course");
            if(!topicExists) AddEntery("topicId","Can't find the topic");
            if(!userExists) AddEntery("ownerId","Can't find the user");
        }
        public void AddEnteryValidateCourse(bool courseExists, bool userExists, bool departmentExists, bool isCourseNameAvailable)
        {
            if(!userExists) AddEntery("trainerId","can't find the user");
            if(!courseExists) AddEntery("courseId","can't find the course");
            if(!departmentExists) AddEntery("departmentId","can't find the department");
            if(!isCourseNameAvailable) AddEntery("name","Course with that name already exists");
        }
        public void AddEnteryValidateCourseFeedback(bool courseExists, bool userExists)
        {
            if(!userExists) AddEntery("traineeId","can't find the user");
            if(!courseExists) AddEntery("courseId","can't find the course");
        }
        public void AddEnteryValidateMOM(bool userExists, bool reviewExists, bool momExists)
        {
            if(!userExists) AddEntery("traineeId","can't find the user");
            if(!reviewExists) AddEntery("reviewId","can't find the review");
            if(momExists) AddEntery("mom","mom already exists");
        }
        public void AddEnteryValidateTopic(bool isTopicNameAvailabe, bool courseExists, bool topicExists)
        {
            if(!courseExists) AddEntery("courseId","can't find the course");
            if(!topicExists) AddEntery("topicId","can't find the topic");
            if(!isTopicNameAvailabe) AddEntery("name","Topic with that name already exists");
        }
        public void AddEnteryValidateTraineeFeedback(bool courseExists,bool userExists, bool traineeExists)
        {
            if(!courseExists) AddEntery("courseId","can't find the course");
            if(!userExists) AddEntery("trainerId","can't find the user");
            if(!traineeExists) AddEntery("traineeId","can't find the user");
        }
        public void checkIdForCourseTopicUser(int courseId, int topicId, int ownerId)
        {
            if(courseId == 0) AddEntery("courseId","can't be zero");
            if(topicId == 0) AddEntery("topicId","can't be zero");
            if(ownerId == 0) AddEntery("ownerId","can't be zero");
        }
        public void validateAndAddEntery(string key,string input,string regex)
        {
            if(!Regex.Match(input,regex).Success)
                AddEntery(key,"Invalid Data");
        }
        public void checkIdAndAddEntery(int id,string key)
        {
            if(id == 0) AddEntery(key,"can't be zero");
        }
    }
}