using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using LibraryManagementSystem.API.Models;

namespace LibraryManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PeopleController : ControllerBase
    {
        private List<Person> People { get; } = new List<Person>();

        public PeopleController()
        {
            string jsonFile = System.IO.File.ReadAllText("./Resources/64KB.json");
            var peopleData = JsonSerializer.Deserialize<List<Person>>(jsonFile, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (peopleData != null)
            {
                People = peopleData;
            }
        }

        // GET: api/people
        [HttpGet]
        public ActionResult<List<Person>> GetPeople()
        {
            return Ok(People);
        }
    }
}