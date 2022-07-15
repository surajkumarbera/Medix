using System;

namespace MediX.Models
{
    public class SignIn
    {
        public String Email { get; set; }
        public String Password { get; set; }

        public bool IsValid()
        {
            return this.Email != null && this.Password != null;
        }
    }
}