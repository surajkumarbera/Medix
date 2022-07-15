namespace MediX.Models
{
    public class Prescriptions
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public bool Verified { get; set; }

    }
}