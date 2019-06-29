namespace Chyld.Elysium.Security
{
    using System;
    using System.Linq;

    using Chyld.Elysium.Data;

    /* *** *** *** *** *** *** *** *** *** *** *** ***  */
    public class Authentication
    {
        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */
        private ElysiumDB m_DB;

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */
        public Int32 UserId { get; set; }
        public String Username { get; set; }
        public String Password { get; set; }
        public Boolean IsValidUser { get; set; }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */
        public Authentication(String username, String password)
        {
            m_DB = new ElysiumDB();

            UserId = 0;
            Username = username;
            Password = password;
            IsValidUser = false;

            Customer customer = m_DB.Customers.SingleOrDefault(c => c.Username == Username && c.Password == Password);

            if(customer != null)
            {
                UserId = customer.UserId;
                IsValidUser = true;
            }
        }
    }
}
