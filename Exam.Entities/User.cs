using System;

namespace Exam.Model
{
    public class User:EntityBase
    {
        public string IdValue { get; set; }

        public string Gender { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public DateTime BirthDate { get; set; }

        public string Uuid { get; set; }

        public string UserName { get; set; }

        public virtual Location Location { get; set; }

    }
}
