using K2DemoWorkFlow.ViewModel;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace K2DemoWorkFlow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public static List<LoginVM> user { get; set; }
        public UserController()
        {
            user=new List<LoginVM>();
            user.Add(new LoginVM() { Password = "123", UserName = "SURE\\MHANNA" });
            user.Add(new LoginVM() { Password = "123", UserName = "SURE\\ANADER" });
            user.Add(new LoginVM() { Password = "123", UserName = "SURE\\hrashwan" });

        }
        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UserController>
        [HttpPost]
        public ActionResult Post([FromBody] LoginVM model)
        {
           var loginUser= user.FirstOrDefault(X => X.UserName.ToLower() ==("SURE\\"+ model.UserName).ToLower() && X.Password == model.Password);
            if (loginUser != null)
            {
                return (Ok(new {login=true,user=loginUser.UserName}));
            }
            return (NotFound(new { login = false}));
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
