using System;

namespace AutomatedTellerMachine.Models
{
    public class RegisterModel : RegisterViewModel
    {
        public string PasswordHash
        {
            get { return CryptoExtensions.HashPassword(Password); }
        }

        public string SecurityStamp
        {
            get { return Guid.NewGuid().ToString(); }
        }

        public bool PasswordConfirmed
        {
            get
            {
                if (Password == ConfirmPassword)
                    return true;
                return false;
            }
        }
    }
}