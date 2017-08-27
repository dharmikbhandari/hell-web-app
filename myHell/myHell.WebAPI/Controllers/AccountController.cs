using myHell.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace myHell.WebAPI.Controllers
{
    public class AccountController : ApiController
    {
        MyHellServices _myHellServices = new MyHellServices();

        [HttpPost]
        [Route("api/account/login/")]
        public IHttpActionResult Login([FromBody]dynamic data)
        {
            bool isValid = false;

            //isValid = _myHellServices.IsValidLogin(data.Email,data.Password);
            isValid = _myHellServices.IsValidLogin(data.Email.ToString(), data.Password.ToString());

            if (isValid)
            {
                return Ok();
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
