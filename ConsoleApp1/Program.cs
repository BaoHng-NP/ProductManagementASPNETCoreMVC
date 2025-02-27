using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
namespace ConsoleApp1
{

    class Program
    {
        private static string GetConnectionString()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", true, true).Build();
            return configuration["ConnectionStrings:DefaultConnectionStringDB"];
        }


        static void Main()
        {
            string connectionString = GetConnectionString();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    Console.WriteLine("✅ Kết nối thành công!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("❌ Lỗi kết nối: " + ex.Message);
                }
            }
        }
    }
}
