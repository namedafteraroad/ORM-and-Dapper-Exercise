using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;
using System;
using System.IO;

namespace ORM_Dapper
{
    public class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string connString = config.GetConnectionString("DefaultConnection");
            

            IDbConnection conn = new MySqlConnection(connString);
            DapperDepartmentRepository repo = new DapperDepartmentRepository(conn);

            Console.WriteLine("Hello user, here are the current departments:");
            Console.WriteLine("Press  Enter");
            Console.ReadLine();
            var depos = repo.GetAllDepartments();

            foreach (var depo in depos)
            {
                Console.WriteLine($"Id: {depo.DepartmentId} Name:{depo.Name}");
            }

            

            var repo2 = new DapperProductRepository(conn);

            repo2.CreateProduct("New Stuff", 89.2, 2);

            var products = repo2.GetAllProducts();

            foreach (var product in products) {  Console.WriteLine($"{product.Name}, {product.Price}, {product.ProductID}, {product.Price}, {product.CategoryID}, {product.OnSale}, {product.StockLevel}");}
        }
    }
}
