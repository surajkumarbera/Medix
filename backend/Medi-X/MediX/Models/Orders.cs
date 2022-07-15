using System;

namespace MediX.Models
{
    public class Orders
    {
        public int Id { get; set; }

        public int? UserId { get; set; }
        public User User { get; set; }

        public int? PrescriptionsId { get; set; }
        public Prescriptions Prescriptions { get; set; }

        public DateTime DateTime { get; set; }

    }
}