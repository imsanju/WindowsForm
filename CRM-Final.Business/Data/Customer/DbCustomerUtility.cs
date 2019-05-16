using System;
using System.Collections.Generic;
using CRM_Final.Business.Models;
using System.Data.SqlClient;

namespace CRM_Final.Business.Data
{
    public class DbCustomerUtility : ICustomerUtility
    {
        public Customer CreateNewCustomer(Customer newCustomer)
        {
            Customer customerToReturn = null;

            SqlCommand cmd = DbManager.GetDbCommandObject();

            cmd.CommandText = @"
                INSERT INTO [SalesLT].[Customer]
                (FirstName, MiddleName, LastName, Suffix, EmailAddress, Phone, CompanyName, SalesPerson, PasswordHash, PasswordSalt, RowGuid)
                VALUES
                (@FirstName, @MiddleName, @LastName, @Suffix, @EmailAddress, @Phone, @CompanyName, @SalesPerson, @PasswordHash, @PasswordSalt, @RowGuid)

                SELECT @@Identity from [SalesLT].[Customer];
            ";

            cmd.Parameters.AddWithValue("@FirstName", newCustomer.FirstName);
            cmd.Parameters.AddWithValue("@MiddleName", newCustomer.MiddleName);
            cmd.Parameters.AddWithValue("@LastName", newCustomer.LastName);
            cmd.Parameters.AddWithValue("@Suffix", newCustomer.Suffix);
            cmd.Parameters.AddWithValue("@EmailAddress", newCustomer.Email);
            cmd.Parameters.AddWithValue("@Phone", newCustomer.Phone);
            cmd.Parameters.AddWithValue("@CompanyName", newCustomer.CompanyName);
            cmd.Parameters.AddWithValue("@SalesPerson", newCustomer.SalesPerson);
            cmd.Parameters.AddWithValue("@PasswordHash", newCustomer.PasswordHash);
            cmd.Parameters.AddWithValue("@PasswordSalt", newCustomer.PasswordSalt);
            cmd.Parameters.AddWithValue("@RowGuid", newCustomer.RowGuid);

            object objNewCustomerId;
            try
            {
                cmd.Connection.Open();
                objNewCustomerId = cmd.ExecuteScalar();
                cmd.Connection.Close();
            }
            catch (Exception)
            {
                throw;
            }
            customerToReturn = GetById(Convert.ToInt32(objNewCustomerId));
            return customerToReturn;
        }

        public List<Customer> CustomerSearch(string query)
        {
            List<Customer> customers = new List<Customer>();

            SqlCommand cmd = DbManager.GetDbCommandObject();
            cmd.CommandText = @"
                SELECT *
                FROM [SalesLT].[Customer]
                WHERE
                    [SalesLT].[Customer].[FirstName] LIKE '%' + @query + '%' OR
                    [SalesLT].[Customer].[LastName] LIKE '%' + @query + '%' OR
                    [SalesLT].[Customer].[CompanyName] LIKE '%' + @query + '%' OR
                    [SalesLT].[Customer].[SalesPerson] LIKE '%' + @query + '%' OR
                    [SalesLT].[Customer].[EmailAddress] LIKE '%' + @query + '%' OR
                    [SalesLT].[Customer].[Phone] LIKE '%' + @query + '%'
            ";

            cmd.Parameters.AddWithValue("@query", query);

            try
            {
                cmd.Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

                while (reader.Read())
                {
                    Customer newC = BuildCustomer(reader);
                    customers.Add(newC);
                }
                reader.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return customers;
        }

        public void DeleteCustomer(Customer customerToDelete)
        {
            SqlCommand cmd = DbManager.GetDbCommandObject();

            cmd.CommandText = @"
                DELETE [SalesLT].[Customer]
                WHERE CustomerID = @CustomerID
            ";
            DateTime lastModified = DateTime.Now;
            cmd.Parameters.AddWithValue("@CustomerID", customerToDelete.CustomerID);
            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Customer GetById(int customerId)
        {
            Customer customerToReturn = null;
            SqlCommand cmd = DbManager.GetDbCommandObject();

            cmd.CommandText = @"
                SELECT *
                FROM [SalesLT].[Customer]
                WHERE [SalesLT].[Customer].[CustomerID] = @CustomerId
            ";
            cmd.Parameters.AddWithValue("@CustomerId", customerId);

            try
            {
                cmd.Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

                if (reader.Read())
                {
                    customerToReturn = BuildCustomer(reader);
                }
                reader.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return customerToReturn;
        }

        public List<Customer> GetList()
        {
            List<Customer> customers = new List<Customer>();

            SqlCommand cmd = DbManager.GetDbCommandObject();
            cmd.CommandText = "SELECT * FROM [SalesLT].[Customer]";

            try
            {
                cmd.Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

                while (reader.Read())
                {
                    Customer newC = BuildCustomer(reader);
                    customers.Add(newC);
                }
                reader.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return customers;
        }

        public void UpdateCustomer(Customer customerToUpdate)
        {
            SqlCommand cmd = DbManager.GetDbCommandObject();

            cmd.CommandText = @"
                UPDATE [SalesLT].[Customer]
                SET FirstName = @FirstName,
                    MiddleName = @MiddleName,
                    LastName = @LastName,
                    Suffix = @Suffix,
                    EmailAddress = @Email,
                    Phone = @Phone,
                    CompanyName = @CompanyName,
                    SalesPerson = @SalesPerson,
                    ModifiedDate = @ModifiedDate
                WHERE CustomerID = @CustomerID
            ";
            DateTime lastModified = DateTime.Now;
            cmd.Parameters.AddWithValue("@CustomerID", customerToUpdate.CustomerID);
            cmd.Parameters.AddWithValue("@FirstName", customerToUpdate.FirstName);
            cmd.Parameters.AddWithValue("@MiddleName", customerToUpdate.MiddleName);
            cmd.Parameters.AddWithValue("@LastName", customerToUpdate.LastName);
            cmd.Parameters.AddWithValue("@Suffix", customerToUpdate.Suffix);
            cmd.Parameters.AddWithValue("@Email", customerToUpdate.Email);
            cmd.Parameters.AddWithValue("@Phone", customerToUpdate.Phone);
            cmd.Parameters.AddWithValue("@CompanyName", customerToUpdate.CompanyName);
            cmd.Parameters.AddWithValue("@SalesPerson", customerToUpdate.SalesPerson);
            cmd.Parameters.AddWithValue("@ModifiedDate", lastModified);

            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception)
            {
                throw;
            }
            customerToUpdate.ModifiedDate = lastModified;
        }

        private static Customer BuildCustomer(SqlDataReader reader)
        {
            return new Customer
            {
                CustomerID = (int)reader["CustomerID"],
                FirstName = (reader["FirstName"] == DBNull.Value) ? "" : (string)reader["FirstName"],
                MiddleName = (reader["MiddleName"] == DBNull.Value) ? "" : (string)reader["MiddleName"],
                LastName = (reader["LastName"] == DBNull.Value) ? "" : (string)reader["LastName"],
                Suffix = (reader["Suffix"] == DBNull.Value) ? "" : (string)reader["Suffix"],
                Email = (reader["EmailAddress"] == DBNull.Value) ? "" : (string)reader["EmailAddress"],
                Phone = (reader["Phone"] == DBNull.Value) ? "" : (string)reader["Phone"],
                CompanyName = (reader["CompanyName"] == DBNull.Value) ? "" : (string)reader["CompanyName"],
                SalesPerson = (reader["SalesPerson"] == DBNull.Value) ? "" : (string)reader["SalesPerson"],
                ModifiedDate = (DateTime)reader["ModifiedDate"]
            };
        }
    }
}
