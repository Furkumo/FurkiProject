using FurkiProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;




namespace FurkiProject.Controllers{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly string _connectionString;
        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _connectionString = configuration.GetConnectionString("DefaultConnection");

        }

        public IActionResult Index()
        {
            List<Player> players = new List<Player>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string query = "SELECT TOP 30 * FROM dbo.Players ORDER BY NEWID()";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();


                while (reader.Read())
                {
                    players.Add(new Player
                    {
                        Id = (int)reader["id"],
                        Futbolcu_Ismi = reader["Futbolcu_Ismi"].ToString(),
                        Futbolcu_Bilgileri = reader["Futbolcu_Bilgileri"].ToString()
                    });
                }
            }

            return View(players);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}