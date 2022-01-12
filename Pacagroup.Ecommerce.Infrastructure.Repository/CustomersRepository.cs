using System;
using Pacagroup.Ecommerce.Domain.Entity;
using Pacagroup.Ecommerce.Infrastructure.Interface;
using Pacagroup.Ecommerce.Transversal.Common;
using Dapper;
using System.Data;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Pacagroup.Ecommerce.Infrastructure.Repository
{
    public class CustomersRepository : ICustomersRepository
    {
        private readonly IConnectionFactory _connectionFactory;

        //no se usa la palabra new para evitar el acomplamiento entre componentes
        //no utilizamos la palabra new por q estamos utilizando inyeccion de dependencias
        public CustomersRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        #region Métodos Síncronos
        public bool Insert(Customers customers)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var query = "CustomersInsert";
                var pameters = new DynamicParameters();
                pameters.Add("CustomerID", customers.Customerid);
                pameters.Add("CompanyName", customers.CompanyName);
                pameters.Add("ContactName", customers.ContactName);
                pameters.Add("ContactTitle", customers.ContactTitle);
                pameters.Add("Address", customers.Address);
                pameters.Add("City", customers.City);
                pameters.Add("Region", customers.Region);
                pameters.Add("PostalCode", customers.PostalCode);
                pameters.Add("Country", customers.Country);
                pameters.Add("Phone", customers.Phone);
                pameters.Add("Fax", customers.Fax);

                var result = connection.Execute(query, param: pameters, commandType: CommandType.StoredProcedure);
                return result > 0;
            }
        }
        public bool Update(Customers customers)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var query = "CustomersUpdate";
                var pameters = new DynamicParameters();
                pameters.Add("CustomerID", customers.Customerid);
                pameters.Add("CompanyName", customers.CompanyName);
                pameters.Add("ContactName", customers.ContactName);
                pameters.Add("ContactTitle", customers.ContactTitle);
                pameters.Add("Address", customers.Address);
                pameters.Add("City", customers.City);
                pameters.Add("Region", customers.Region);
                pameters.Add("PostalCode", customers.PostalCode);
                pameters.Add("Country", customers.Country);
                pameters.Add("Phone", customers.Phone);
                pameters.Add("Fax", customers.Fax);

                var result = connection.Execute(query, param: pameters, commandType: CommandType.StoredProcedure);
                return result > 0;
            }

        }
        public bool Delete(string Customerid)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var query = "CustomersDelete";
                var pameters = new DynamicParameters();
                pameters.Add("CustomerID",Customerid);            
                var result = connection.Execute(query, param: pameters, commandType: CommandType.StoredProcedure);
                return result > 0;
            }

        }
        public Customers Get(string customerId)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var query = "CustomersGetByID";
                var pameters = new DynamicParameters();
                pameters.Add("CustomerID", customerId);
                var customer = connection.QuerySingle<Customers>(query, param: pameters, commandType: CommandType.StoredProcedure);
                return customer ;
            }

        }
        public IEnumerable<Customers>GetAll()
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var query = "CustomersList";
                var customers = connection.Query<Customers>(query,commandType: CommandType.StoredProcedure);
                return customers;
            }

        }
        #endregion

        #region Metodos asincronos
        public async Task<bool>InsertAsync(Customers customers)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var query = "CustomersInsert";
                var pameters = new DynamicParameters();
                pameters.Add("CustomerID", customers.Customerid);
                pameters.Add("CompanyName", customers.CompanyName);
                pameters.Add("ContactName", customers.ContactName);
                pameters.Add("ContactTitle", customers.ContactTitle);
                pameters.Add("Address", customers.Address);
                pameters.Add("City", customers.City);
                pameters.Add("Region", customers.Region);
                pameters.Add("PostalCode", customers.PostalCode);
                pameters.Add("Country", customers.Country);
                pameters.Add("Phone", customers.Phone);
                pameters.Add("Fax", customers.Fax);

                var result = await connection.ExecuteAsync(query, param: pameters, commandType: CommandType.StoredProcedure);
                return result > 0;
            }
        }
        public async Task<bool> UpdateAsync(Customers customers)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var query = "CustomersUpdate";
                var pameters = new DynamicParameters();
                pameters.Add("CustomerID", customers.Customerid);
                pameters.Add("CompanyName", customers.CompanyName);
                pameters.Add("ContactName", customers.ContactName);
                pameters.Add("ContactTitle", customers.ContactTitle);
                pameters.Add("Address", customers.Address);
                pameters.Add("City", customers.City);
                pameters.Add("Region", customers.Region);
                pameters.Add("PostalCode", customers.PostalCode);
                pameters.Add("Country", customers.Country);
                pameters.Add("Phone", customers.Phone);
                pameters.Add("Fax", customers.Fax);

                var result =await connection.ExecuteAsync(query, param: pameters, commandType: CommandType.StoredProcedure);
                return result > 0;
            }

        }
        public async Task<bool> DeleteAsync(string Customerid)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var query = "CustomersDelete";
                var pameters = new DynamicParameters();
                pameters.Add("CustomerID", Customerid);
                var result =await connection.ExecuteAsync(query, param: pameters, commandType: CommandType.StoredProcedure);
                return result > 0;
            }

        }
        public async Task<Customers> GetAsync(string customerId)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var query = "CustomersGetByID";
                var pameters = new DynamicParameters();
                pameters.Add("CustomerID", customerId);
                var customer = await connection.QuerySingleAsync<Customers>(query, param: pameters, commandType: CommandType.StoredProcedure);
                return customer;
            }

        }
        public async Task<IEnumerable<Customers>>  GetAllAsync()
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var query = "CustomersList";
                var customers = await connection.QueryAsync<Customers>(query, commandType: CommandType.StoredProcedure);
                return customers;
            }

        }      
        #endregion
    }
}
