using System;
using System.Collections.Generic;
using okeafurniture.CORE.DTOs;
using okeafurniture.CORE.Interfaces;
using okeafurniture.CORE;
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



        public Response<List<TopItems>> GetTopItems(int categoryId)
        {

            List<TopItems> itemList = new List<TopItems>();

            //Return agent information for only retired agents (security clearance retired)
            using (var connection = new SqlConnection(_sqlConnectionString))
            {
                var command = new SqlCommand("TopItems", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@categoryId", categoryId);

                try
                {
                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            /*
                            public string AgencyName { get; set; }
                            public Guid BadgeId { get; set; }
                            public string NameLastFirst { get; set; }}
                            public DateTime DeactivationDate { get; set; }
                             */
                            var row = new TopItems();

                            row.CategoryId = (int)reader["CategoryId"];
                            row.CategoryName = reader["CategoryName"].ToString();
                            row.ItemName =      reader["ItemName"].ToString();
                            row.ItemDescription = reader["ItemDescription"].ToString();
                            row.UnitPrice = (decimal)reader["UnitPrice"];

                            itemList.Add(row);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                Response<List<TopItems>> response = new Response<List<TopItems>>();
                response.Data = itemList;
                response.Success = true;
                response.Message = "Successfully retrieved report.";

                return response;
            }
            /*
Create PROCEDURE TopItems (@categoryId as int)
AS BEGIN
SELECT a.CategoryId, a.CategoryName, ay.ItemName, ay.ItemDescription, ay.UnitPrice
FROM Categories as a
INNER JOIN ItemCategories as aa on aa.CategoryId = a.CategoryId
INNER JOIN Items AS ay on ay.ItemId=a.CategoryId
Where a.CategoryId=@categoryId;
END
            */
        }
    }
}
