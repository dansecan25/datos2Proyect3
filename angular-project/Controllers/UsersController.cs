using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using ServerFilesTemp.Models; 
namespace ServerFilesTemp.Controllers
{
    [ApiController]
    [Route("users")]
    public class UsersController : ControllerBase
    {
        [HttpPost]
        public IActionResult PostUser([FromBody] User user)
        {
            // Read the existing user data from db.json
            var jsonData = System.IO.File.ReadAllText("db.json");
            var users = JsonConvert.DeserializeObject<List<User>>(jsonData) ?? new List<User>();

            // Add the new user to the list
            users.Add(user);

            // Serialize the updated user data back to JSON
            var updatedJsonData = JsonConvert.SerializeObject(users);

            // Write the updated JSON data to db.json
            System.IO.File.WriteAllText("db.json", updatedJsonData);

            return Ok();
        }
    }
}
