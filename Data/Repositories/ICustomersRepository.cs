using Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public interface ICustomersRepository 
    {
        IEnumerable<Customers> GetAll();

        IEnumerable<dynamic> DublicateDataCustomers(string query);

        IEnumerable<CustomerList> DublicateDataCustomersAuto(string query);

        IEnumerable<CustomersDublicate> DublicateDataVehiclesAutoLocal(string query);

        IEnumerable<CustomerList> DublicateDataVehiclesAuto(string query);

        IEnumerable<Customers> GetDataById(string ClientCustomerID, string AccountID);

        IEnumerable<Vehicles> GetVehiclesData(string CustomersID);

        void Delete(string CustomerID);

        void DeleteAndBackUp(Customers customers);

        void DeleteAndBackUpVehicles(Vehicles vehicles);
    }
}
