using AyurYogi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace AyurYogi.Controllers
{
    [Route("api/Register")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        SqlConnection connection = null;
        public RegisterController(IConfiguration configuration)
        {
            _configuration = configuration;   
            connection = new SqlConnection(_configuration.GetConnectionString("DBConn").ToString());
        }

        [HttpPost]
        [Route("RegisterNewUser")]
        public Responce RegisterNewUser(Users users)
        {
            Responce responce = new Responce(); 
            DAL dal = new DAL();
            responce = dal.Registration(users, connection);
            return responce;
        }

        [HttpPost]
        [Route("Login")]
        public Responce Login(Users users)
        {
            Responce responce = new Responce();
            DAL dal = new DAL();
            responce = dal.Login(users, connection); 
            return responce;
        }

    }
}
