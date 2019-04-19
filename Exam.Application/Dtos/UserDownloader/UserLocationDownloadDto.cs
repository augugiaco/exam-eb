using System;
using System.Collections.Generic;
using System.Text;

namespace Exam.Application.Dtos.UserDownloader
{
    public class UserLocationDownloadDto
    {
        public string state { get; set; }
        public string city { get; set; }
        public string street { get; set; }
        public string postcode { get; set; }
    }
}
