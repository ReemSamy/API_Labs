using DayThree.Data.Context;
using DayThree.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DayThree.Controllers
{
        #region Used Data
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly UserManager<Employee> _userManager;

        public DataController(UserManager<Employee> userManager)
        {
            _userManager=userManager;
        }
        #endregion

        #region Getting Data
        [HttpGet]
        [Authorize]
        public async Task< ActionResult> GetSecureData()
        {
            //  var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            // var User = await _userManager.FindByIdAsync(UserId);
            var user = await _userManager.GetUserAsync(User);
            return Ok(new String[] {
                "Reem",
                "Samy",
                "Mustafa",
               user!.Email!,
               user!.Department

                });
        }
        #endregion

        #region Manager Accsses
        [HttpGet]
        [Authorize(Policy="Manager")]
        [Route("ForManagers")]
        public ActionResult GetSecureDataForManagers()
        {
            return Ok(new string[]
            {
                "Reem",
                "Khadija",
                "Yazen",
                "Jamal",
                "This Data From Manager Only"
            });
        }
        #endregion

        #region Employee Accssec
        [HttpGet]
        [Authorize(Policy = "Employee")]
        [Route("ForEmployee")]
        public ActionResult GetSecureDataForEmployees()
        {
            return Ok(new string[]
            {
                "Malek",
                "Samy",
                "Mustafa",
                "Abdullrahman",
                "Tasbeeh",
                "This Data From Employee Only"
            });
        }
    }
}
        #endregion
