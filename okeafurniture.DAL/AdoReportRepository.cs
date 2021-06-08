using System;
using System.Collections.Generic;
using okeafurniture.CORE.DTOs;
using okeafurniture.CORE.Interfaces;
using System.Data.SqlClient;
using System.Data;

namespace okeafurniture.DAL
{
    public class AdoReportRepository : IReportRepository
    {
        private string _sqlConnectionString;
        public AdoReportRepository(string sqlConnectionString)
        {
            _sqlConnectionString = sqlConnectionString;
        }



        public Response<List<TopItemListItem>> GetTopItems()
        {

            List<TopItemListItem> itemList = new List<TopItemListItem>();

            using (var connection = new SqlConnection(_sqlConnectionString))
            {
                var command = new SqlCommand("TopItems", connection);
                command.CommandType = CommandType.StoredProcedure;

                try
                {
                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var row = new TopItemListItem();

                            row.ItemId = (int)reader["ItemId"];
                            row.ItemName = reader["ItemName"].ToString();
                            row.UnitPrice =      (decimal)reader["UnitPrice"];
                            row.UnitsSold = (int)reader["UnitsSold"];
                            row.Revenue = (decimal)reader["Revenue"];

                            itemList.Add(row);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                Response<List<TopItemListItem>> response = new Response<List<TopItemListItem>>();
                response.Data = itemList;
                response.Success = true;
                response.Message = "Successfully retrieved Top Items report.";

                return response;
            }
        }

        public Response<List<TopCustomerListItem>> GetTopCustomers()
        {

            List<TopCustomerListItem> customerList = new List<TopCustomerListItem>();

            using (var connection = new SqlConnection(_sqlConnectionString))
            {
                var command = new SqlCommand("TopCustomers", connection);
                command.CommandType = CommandType.StoredProcedure;

                try
                {
                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var row = new TopCustomerListItem();

                            row.AccountId = (int)reader["AccountId"];
                            row.FullName = reader["FullName"].ToString();
                            row.Email = reader["Email"].ToString();
                            row.TotalSpent = (decimal)reader["TotalSpent"];

                            customerList.Add(row);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                Response<List<TopCustomerListItem>> response = new Response<List<TopCustomerListItem>>();
                response.Data = customerList;
                response.Success = true;
                response.Message = "Successfully retrieved Top Customers report.";

                return response;
            }
        }
    }
}
