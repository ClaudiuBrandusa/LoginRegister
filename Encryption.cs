using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace LoginRegister
{
    public class Encryption
    {
        string salt;
        AdminControl ac;
        public Encryption()
        {
            ac = new AdminControl();
            // we will verify if the database has already a salt or not
            if (ac.HasSalt())
            {
                salt = ac.GetSalt();
            }
            else
            {
                salt = BCrypt.Net.BCrypt.GenerateSalt(11);
                ac.SetSalt(salt);
            }
        }
        public string PassHash(string data)
        {
            return BCrypt.Net.BCrypt.HashPassword(data,salt);
        }
        public bool Verify(string data, string hashed)
        {
            return Convert.ToBoolean(BCrypt.Net.BCrypt.Verify(data, hashed));
        }
    }
}
