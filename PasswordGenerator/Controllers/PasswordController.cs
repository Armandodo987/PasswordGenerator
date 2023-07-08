using Microsoft.AspNetCore.Mvc;
using PasswordGenerator.Class;
using PasswordGenerator.Interfaces;

namespace PasswordGenerator.Controllers
{

    [Route("api/[OTP]")]
    [ApiController]
    public class PasswordController : ControllerBase
    {
        private readonly IPasswordService _passwordService;

        public PasswordController(IPasswordService passwordService) 
        {
            _passwordService = passwordService;
        }

        /// <summary>
        /// generate the password based on timedate and userid
        /// </summary>
        /// <param name="model">class for UserId and DateTime</param>
        /// <returns></returns>
        [HttpPost]
        [Route("generate")]
        public ActionResult<string> Generate(GenerateOTPViewModel model)
        {
            return Ok(_passwordService.GenerateOTP(model.UserId, ((DateTimeOffset)model.DateTime).ToUnixTimeSeconds()));
            
        }

        /// <summary>
        /// validate the password for up to 30 seconds.
        /// </summary>
        /// <param name="model">class for OTP and UserId</param>
        /// <returns></returns>
        [HttpPost]
        [Route("validate")]
        public ActionResult<bool> Validate([FromBody]ValidateOTPViewModel model)
        {
            var dateNow = ((DateTimeOffset)DateTime.Now).ToUnixTimeSeconds();
            for (var i = dateNow; i > dateNow - 30; i--)
            {
                if (_passwordService.GenerateOTP(model.UserId, i) == model.OTP)
                {
                    return Ok(true);
                }
            }
            return Ok(false);
        }
    }
}
