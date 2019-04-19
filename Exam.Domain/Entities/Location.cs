namespace Exam.Domain.Entities
{
    public class Location:EntityBase
    {
        public string UserId { get; set; }

        public User User { get; set; }

        public string State { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string PostCode { get; set; }

    }
}
