using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entities
{
    public class CustomerList
    {
        public string  ClientCustomerID { get; set; }
        public string AccountID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address1 { get; set; }

        public string InsertedBy { get; set; }

        public int cnt { get; set; }
    }
}
