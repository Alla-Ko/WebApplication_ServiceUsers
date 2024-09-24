using Microsoft.AspNetCore.Mvc;
using WebApplication_Services.Models;
using WebApplication_Services.Services;

namespace WebApplication_Services.Controllers;

[Route("api/[controller]")] //
[ApiController]
public class HomeController : Controller
{
    private readonly IServiceUsers? _serviceUsers;

    public HomeController(IServiceUsers? serviceUsers)
    {

        _serviceUsers = serviceUsers;
        //serviceUsers.Create(new Models.User { Id = Guid.NewGuid().ToString(), Email = "ivan.ivanov@example.com", FirstName = "Ivan", LastName = "Ivanov" });
        //serviceUsers.Create(new Models.User { Id = Guid.NewGuid().ToString(), Email = "olga.petrenko@example.com", FirstName = "Olga", LastName = "Petrenko" });
        //serviceUsers.Create(new Models.User { Id = Guid.NewGuid().ToString(), Email = "petro.sydorchuk@example.com", FirstName = "Petro", LastName = "Sydorchuk" });
        //serviceUsers.Create(new Models.User { Id = Guid.NewGuid().ToString(), Email = "anna.melnyk@example.com", FirstName = "Anna", LastName = "Melnyk" });
        //serviceUsers.Create(new Models.User { Id = Guid.NewGuid().ToString(), Email = "oleg.danylchenko@example.com", FirstName = "Oleg", LastName = "Danylchenko" });
        //serviceUsers.Create(new Models.User { Id = Guid.NewGuid().ToString(), Email = "iryna.zavadiuk@example.com", FirstName = "Iryna", LastName = "Zavadiuk" });
        //serviceUsers.Create(new Models.User { Id = Guid.NewGuid().ToString(), Email = "dmytro.melnyk@example.com", FirstName = "Dmytro", LastName = "Melnyk" });
        //serviceUsers.Create(new Models.User { Id = Guid.NewGuid().ToString(), Email = "inna.vovk@example.com", FirstName = "Inna", LastName = "Vovk" });
        //serviceUsers.Create(new Models.User { Id = Guid.NewGuid().ToString(), Email = "yulia.tsygankov@example.com", FirstName = "Yulia", LastName = "Tsygankov" });
        //serviceUsers.Create(new Models.User { Id = Guid.NewGuid().ToString(), Email = "viktor.zakharchenko@example.com", FirstName = "Viktor", LastName = "Zakharchenko" });


    }
    [HttpGet]
    public JsonResult Get() =>
         Json(_serviceUsers?.Read());

    [HttpGet("{email}")]
    public JsonResult GetUser(String email)
    {
        // Приводимо рядковий id до ObjectId

        return Json(_serviceUsers.GetUserByEmail(email));
    }
    //[HttpGet("{id}")]
    //public JsonResult GetUser(ObjectId id)
    //{
    //    // Приводимо рядковий id до ObjectId

    //    return Json(_serviceUsers.GetUserById(id));
    //}
    [HttpPost]
    public JsonResult PostUser(User? user)
    {


        return Json(_serviceUsers?.Create(user));
    }

    [HttpPut]
    public JsonResult PutUser(User? user) => Json(_serviceUsers?.Update(user));

    [HttpDelete("{id}")]
    public IActionResult DeleteUser(string id)
    {
        Console.WriteLine(id);

        var user = _serviceUsers.GetUserById(id);
        if (user != null)
        {
            Console.WriteLine(user.Id);
            _serviceUsers.Delete(id);


            return Ok();
        }
        else
        {
            return NotFound($"User with ID {id} not found.");
        }
    }
}
