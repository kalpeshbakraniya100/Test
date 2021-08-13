using Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entities
{
	[Table("Customers")]
    public class CustomersDublicate
    {
		[Key]
        public int DId { get; set; }
        public int CustomerID { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string title { get; set; }
		public string NickName { get; set; }
		public string CustNameCombo { get; set; }
		public string Company { get; set; }
		public string gender { get; set; }
		public string EmailAddress { get; set; }
		public string EmailAddress2 { get; set; }
		public string CellPhone { get; set; }
		public string HomePhone { get; set; }
		public string WorkPhone { get; set; }
		public string Address1 { get; set; }
		public string Address2 { get; set; }
		public string City { get; set; }
		public string ST { get; set; }
		public string zip { get; set; }
		public string Notes { get; set; }
		public int AccountID { get; set; }
		public DateTime? InsertDate { get; set; } = DateTime.Now;
		public string InsertedBy { get; set; }
		public string ClientCustomerID { get; set; }
		public string ClientLocationID { get; set; }
		public int LocationID { get; set; }
		public string Active { get; set; }
		public string PolicyType { get; set; }
		public DateTime RenewDate { get; set; }
		public string Status { get; set; }
		public string PolicyNum { get; set; }
		public string AgentLocationID { get; set; }
		public string CustomerType { get; set; }
		public DateTime? CustomerBirthday { get; set; } = DateTime.Now;
		public DateTime? LastModifiedDate { get; set; } = DateTime.Now;
		public string LastModifiedBy { get; set; }
		public string CommOrder1 { get; set; }
		public string CommOrder2 { get; set; }
		public string CommOrder3 { get; set; }
		public string CommOrder4 { get; set; }
		public string Phone4 { get; set; }
		public string facebook { get; set; }
		public string twitter { get; set; }
		public string googleplus { get; set; }
		public string skype { get; set; }
		public string yahoomessenger { get; set; }
		public string ContactPreference1 { get; set; }
		public string ContactPreference2 { get; set; }
		public string ContactPreference3 { get; set; }
		public string ContactPreference4 { get; set; }
		public int BusinessSIC { get; set; }
		public DateTime? PolicyStartDate { get; set; } = DateTime.Now;
		public string MemID { get; set; }
		public int AAAAccID { get; set; }
		public string Email3 { get; set; }
		public string Email4 { get; set; }
		public string Phone5 { get; set; }
		public string Latitude { get; set; }
		public string Longitude { get; set; }
	}
}
