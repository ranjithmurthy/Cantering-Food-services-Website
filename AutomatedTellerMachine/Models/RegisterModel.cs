using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutomatedTellerMachine.Models
{
    public class RegisterModel : RegisterViewModel
    {
        public string PasswordHash {
            get
            {

                return CryptoExtensions.HashPassword(Password);
            }
        }
        public string SecurityStamp
        {
            get
            {
                return Guid.NewGuid().ToString();
            }
        }

        public Boolean PasswordConfirmed
        {
            get
            {
                if (Password == ConfirmPassword)
                    return  true;
                else
                    return false;
            }
        }

    }
}