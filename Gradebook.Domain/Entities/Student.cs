namespace Gradebook.Domain.Entities
{
    public class Student : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateOnly DOB { get; set; }
        public int YearEnrolled { get; set; }
    }
}
