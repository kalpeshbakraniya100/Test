using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entities
{
    public class VehiclesDublicate
    {
		public int VId { get; set; }
		public int VehicleID { get; set; }
		public string LicenseNum { get; set; }
		public string LicenseState { get; set; }
		public int CustomerID { get; set; }
		public int InvoiceID { get; set; }
		public int VYear { get; set; }
		public string VModel { get; set; }
		public string VMake { get; set; }
		public string Vin { get; set; }
		public string ClientVehicleID { get; set; }
		public int VehicleHealth { get; set; }
		public string Reason { get; set; }
		public byte isVinRetrievable { get; set; }
		public int Mileage { get; set; }
		public string SubModel { get; set; }
	}
}
