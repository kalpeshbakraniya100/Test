using Dapper;
using Data.Entities;
using Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Data.Repositories
{
    public class CustomersRepository: ICustomersRepository
    {
        protected IConnectionFactory _connectionFactory;

        public CustomersRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public IEnumerable<Customers> GetAll()
        {
            using var conn = _connectionFactory.GetConnection;
            return SimpleCRUD.GetList<Customers>(conn);
        }

        //public IEnumerable<Customers> cz(string sp_name)
        //{
        //    using (var conn = _connectionFactory.GetConnection)
        //    {
        //        return conn.Query<Customers>(sp_name, commandType: CommandType.StoredProcedure);
        //    }
        //}
        
        public IEnumerable<dynamic> DublicateDataCustomers(string sp_name)
        {
            using var conn = _connectionFactory.GetConnection;
            return conn.Query<dynamic>(sp_name, commandType: CommandType.StoredProcedure, commandTimeout: 0);
        }

        //IEnumerable<CustomerList> DublicateDataCustomersAuto(string query);
        public IEnumerable<CustomerList> DublicateDataCustomersAuto(string sp_name)
        {
            using var conn = _connectionFactory.GetConnection;
            return conn.Query<CustomerList>(sp_name, commandType: CommandType.StoredProcedure, commandTimeout: 0);
        }


        public IEnumerable<Customers> GetDataById(string clientCustomerID, string accountID)
        {            
            var queryString = "SELECT * FROM Customers WHERE ClientCustomerID='" + clientCustomerID + "'AND AccountID='" + accountID + "' ";
            using var conn = _connectionFactory.GetConnection;
            return conn.Query<Customers>(queryString).AsList();
        }

        public void Delete(string customerID)
        {
           using var conn = _connectionFactory.GetConnection;
           var GetBackupdata = "SELECT * FROM Customers WHERE CustomerID = '" + customerID + "'";
           var queryString = "DELETE FROM Customers WHERE CustomerID = '" + customerID + "'";
           var data = conn.QuerySingle<Customers>(GetBackupdata);
           CustomersDublicate customersDublicate = new CustomersDublicate();
            customersDublicate.CustomerID = data.CustomerID;
            customersDublicate.ClientCustomerID = data.ClientCustomerID;
            customersDublicate.AccountID = data.AccountID;
            customersDublicate.FirstName = data.FirstName;
            customersDublicate.EmailAddress = data.EmailAddress;
            customersDublicate.Email4 = data.Email4;
            customersDublicate.Email3 = data.Email3;
            customersDublicate.AAAAccID = data.AAAAccID;
            customersDublicate.Active = data.Active;
            customersDublicate.Address1 = data.Address1;
            customersDublicate.BusinessSIC = data.BusinessSIC;
            customersDublicate.CustomerBirthday = data.CustomerBirthday is null ? Convert.ToDateTime(data.CustomerBirthday).AddYears(+1970) : data.CustomerBirthday;  
            customersDublicate.ContactPreference1 = data.ContactPreference1;
            customersDublicate.ContactPreference2 = data.ContactPreference2;
            customersDublicate.ContactPreference3 = data.ContactPreference3;
            customersDublicate.ContactPreference4 = data.ContactPreference4;
            customersDublicate.CommOrder1 = data.CommOrder1;
            customersDublicate.CommOrder2 = data.CommOrder2;
            customersDublicate.CommOrder3 = data.CommOrder3;
            customersDublicate.CommOrder4 = data.CommOrder4;
            customersDublicate.LastModifiedBy = data.LastModifiedBy;
            customersDublicate.Longitude = data.Longitude;
            customersDublicate.Latitude = data.Latitude;
            customersDublicate.Phone5 = data.Phone5;
            customersDublicate.MemID = data.MemID;
            customersDublicate.PolicyStartDate = data.PolicyStartDate is null? Convert.ToDateTime(data.PolicyStartDate).AddYears(+1970) : Convert.ToDateTime(data.PolicyStartDate);
            customersDublicate.yahoomessenger = data.yahoomessenger;
            customersDublicate.skype = data.skype;
            customersDublicate.googleplus = data.googleplus;
            customersDublicate.InsertDate = data.InsertDate is null ? Convert.ToDateTime(data.InsertDate).AddYears(+1970) : Convert.ToDateTime(data.InsertDate); 
            customersDublicate.InsertedBy = data.InsertedBy;
            customersDublicate.LocationID = data.LocationID;
            customersDublicate.PolicyType = data.PolicyType;
            customersDublicate.RenewDate = data.RenewDate is null ? Convert.ToDateTime(data.RenewDate).AddYears(+1970) : Convert.ToDateTime(data.RenewDate);
            customersDublicate.Status = data.Status;
            customersDublicate.PolicyNum = data.PolicyNum;
            customersDublicate.AgentLocationID = data.AgentLocationID;
            customersDublicate.CustomerType = data.CustomerType;
            customersDublicate.LastModifiedDate = data.LastModifiedDate is null? Convert.ToDateTime(data.LastModifiedDate).AddYears(+1970) : Convert.ToDateTime(data.LastModifiedDate);
            customersDublicate.Phone4 = data.Phone4;
            customersDublicate.facebook = data.facebook;
            customersDublicate.Notes = data.Notes;
            customersDublicate.zip = data.zip;
            customersDublicate.ST = data.ST;
            customersDublicate.City = data.City;
            customersDublicate.WorkPhone = data.WorkPhone;
            customersDublicate.HomePhone = data.HomePhone;
            customersDublicate.CellPhone = data.CellPhone;
            customersDublicate.twitter = data.twitter;
            customersDublicate.EmailAddress2 = data.EmailAddress2;
            customersDublicate.gender = data.gender;
            customersDublicate.Company = data.Company;
            customersDublicate.CustNameCombo = data.CustNameCombo;
            customersDublicate.NickName = data.NickName;
            customersDublicate.title = data.title;
            customersDublicate.LastName = data.LastName;
            customersDublicate.DId = 0;
            using var conn100 = _connectionFactory.CreateDbConnection(DbConnectionString.ConnectionLocal);
            if (customersDublicate.DId == 0)
            {
                customersDublicate.DId = (int)SimpleCRUD.Insert<CustomersDublicate>(conn100, customersDublicate);
                if (customersDublicate.DId > 0)
                {
                    conn.Query<Customers>(queryString);
                }
            }
        }

        public void DeleteAndBackUp(Customers customers)
        {
            using var conn = _connectionFactory.GetConnection;
            using var conn100 = _connectionFactory.CreateDbConnection(DbConnectionString.ConnectionLocal);
            CustomersDublicate customersDublicate = new CustomersDublicate();
            PropMap<Customers, CustomersDublicate>.Copy(customers, customersDublicate);
            customersDublicate.CustomerBirthday = customers.CustomerBirthday is null ? Convert.ToDateTime(customers.CustomerBirthday).AddYears(+1970) : customers.CustomerBirthday;
            customersDublicate.PolicyStartDate = customers.PolicyStartDate is null ? Convert.ToDateTime(customers.PolicyStartDate).AddYears(+1970) : Convert.ToDateTime(customers.PolicyStartDate);
            customersDublicate.InsertDate = customers.InsertDate is null ? Convert.ToDateTime(customers.InsertDate).AddYears(+1970) : Convert.ToDateTime(customers.InsertDate);
            customersDublicate.RenewDate = customers.RenewDate is null ? Convert.ToDateTime(customers.RenewDate).AddYears(+1970) : Convert.ToDateTime(customers.RenewDate);
            customersDublicate.LastModifiedDate = customers.LastModifiedDate is null ? Convert.ToDateTime(customers.LastModifiedDate).AddYears(+1970) : Convert.ToDateTime(customers.LastModifiedDate);
            if (customers != null)
            {
                if(customersDublicate.DId == 0)
                {
                    customersDublicate.DId = (int)SimpleCRUD.Insert<CustomersDublicate>(conn100, customersDublicate);
                    if (customersDublicate.DId > 0)
                    {
                        SimpleCRUD.Delete<Customers>(conn, customers);
                    }
                }
            }
        }    
        
        public void DeleteAndBackUpVehicles(Vehicles vehicles)
        {
            using var conn = _connectionFactory.GetConnection;
            using var conn100 = _connectionFactory.CreateDbConnection(DbConnectionString.ConnectionLocal);
            VehiclesDublicate vehiclesDublicate = new VehiclesDublicate();
            PropMap<Vehicles, VehiclesDublicate>.Copy(vehicles, vehiclesDublicate);
            if (vehicles != null)
            {
                if(vehicles.VehicleID == 0)
                {
                    vehicles.VehicleID = (int)SimpleCRUD.Insert<VehiclesDublicate>(conn100, vehiclesDublicate);
                    if (vehicles.VehicleID > 0)
                    {
                        SimpleCRUD.Delete<Vehicles>(conn, vehicles);
                    }
                }
            }
        }

        public IEnumerable<CustomersDublicate> DublicateDataVehiclesAutoLocal(string sp_name)
        {
            using var conn = _connectionFactory.CreateDbConnection(DbConnectionString.LocaDb);
            return conn.Query<CustomersDublicate>(sp_name, commandType: CommandType.StoredProcedure, commandTimeout: 0);
        }

        public IEnumerable<Vehicles> GetVehiclesData(string CustomersID)
        {
            var queryString = "SELECT * FROM Customers WHERE ClientCustomerID='" + CustomersID + "' ";
            using var conn = _connectionFactory.GetConnection;
            return conn.Query<Vehicles>(queryString).AsList();
        }

        public IEnumerable<CustomerList> DublicateDataVehiclesAuto(string sp_name)
        {
            using var conn = _connectionFactory.GetConnection;
            return conn.Query<CustomerList>(sp_name, commandType: CommandType.StoredProcedure, commandTimeout: 0);
        }
    }
}
