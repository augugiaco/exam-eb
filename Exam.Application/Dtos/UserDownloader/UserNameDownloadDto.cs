using System.Text;

namespace Exam.Application.Dtos.UserDownloader
{
    public class UserNameDownloadDto
    {
        public string title { get; set; }
        public string first { get; set; }
        public string last { get; set; }
        public string FullName
        {
            get
            {
                var result = new StringBuilder();
                
                //if (string.IsNullOrWhiteSpace(title) == false)
                //    result.Append($"{title}. ");

                if (string.IsNullOrWhiteSpace(first) == false)
                    result.Append($"{first} ");

                if (string.IsNullOrWhiteSpace(last) == false)
                    result.Append($"{last}");

                return result.ToString();
            }
            
        }

    }
}
