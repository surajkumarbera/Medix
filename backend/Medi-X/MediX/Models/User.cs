using System.Text.RegularExpressions;

namespace MediX.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string Role { get; set; }
        public string Pincode { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }

        public bool IsUserValid()
        {

            if (IsNameValid() && IsEmailValid() && Roles.IsAValidRole(this.Role) && IsMobileNoValid() && IsPincodeValid() && IsAddressValid() && IsPasswordValid())
                return true;

            return false;
        }

        public bool IsNameValid()
        {
            return this.Name != null && Regex.Match(this.Name, @"[a-zA-Z0-9 ]{3,255}").Success;
        }

        public bool IsEmailValid()
        {
            return this.Email != null && Regex.Match(this.Email, @"[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+").Success;
        }

        public bool IsMobileNoValid()
        {
            return this.MobileNo != null && Regex.Match(this.MobileNo, @"0?[0-9]{10}").Success;
        }

        public bool IsPincodeValid()
        {
            return this.Pincode != null && Regex.Match(this.Pincode, @"[0-9]{6}").Success;
        }

        public bool IsAddressValid()
        {
            return this.Address != null && Regex.Match(this.Address, @"[a-zA-z0-9/\\''(), :-]{2,255}").Success;
        }
        public bool IsPasswordValid()
        {
            return this.Password != null && Regex.Match(this.Password, @"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$").Success;
        }
    }
}