using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using LibraryManagementSystem.API.Models;

namespace LibraryManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatronController : ControllerBase
    {
        private List<Patron> Patrons { get; } = new List<Patron>();

        public PatronController()
        {
            string jsonFile = System.IO.File.ReadAllText("./Resources/patrons.json");
            var patronsData = JsonSerializer.Deserialize<List<Patron>>(jsonFile, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (patronsData != null)
            {
                Patrons = patronsData;
            }
        }

        // GET: api/patrons
        [HttpGet]
        public ActionResult<List<Patron>> GetPatrons()
        {
            return Ok(Patrons);
        }
    }
}