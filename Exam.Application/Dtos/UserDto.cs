using System;

namespace Exam.Application.Dtos
{
    public class UserDto
    {
        public string IdValue { get; set; }

        public string Gender { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public DateTime BirthDate { get; set; }

        public string Uuid { get; set; }

        public string UserName { get; set; }

        public LocationDto Location { get; set; }

        public bool IsOldest { get; set; }
    }
}
