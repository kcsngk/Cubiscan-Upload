using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CubiscanUpload.com.netsuite.webservices;

namespace CubiscanUpload
{
    class NetsuiteUser
    {
        public string AccountNumber { get; set; }
        public string Email { get; set; }
        public string RoleID { get; set; }
        public string Password { get; set; }

        public NetsuiteUser()
        {
        }

        public NetsuiteUser(string accountNumber, string email, string roleID, string password)
        {
            this.AccountNumber = accountNumber;
            this.Email = email;
            this.RoleID = roleID;
            this.Password = password;
        }
        public Passport prepare(NetsuiteUser NSuser)
        {
            Passport passport = new Passport();
            passport.account = NSuser.AccountNumber;
            passport.email = NSuser.Email;
            RecordRef role = new RecordRef();
            role.internalId = NSuser.RoleID;
            passport.role = role;
            passport.password = NSuser.Password;
            return passport;
        }
    }
}
