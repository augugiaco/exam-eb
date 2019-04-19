using System;

namespace Exam.Application.Dtos.UserDownloader
{
    public class UserDownloadDto
    {
        public UserBirthDateDownloadDto dob { get; set; }
        public string gender { get; set; }
        public string email { get; set; }
        public UserNameDownloadDto name { get; set; }
        public string uuid { get; set; }
        public string username { get; set; }
        public UserLoginDownloadDto login { get; set; }
        public UserLocationDownloadDto location { get; set; }
    }
}
