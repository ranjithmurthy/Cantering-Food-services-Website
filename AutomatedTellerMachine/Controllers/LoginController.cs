using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Security;
using AutomatedTellerMachine.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;

namespace AutomatedTellerMachine.Controllers
{
    public class LoginController : ApiController
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();


        private ApplicationUserManager _userManager;

        public LoginController()
        {
        }

        public LoginController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }


        // GET: api/Login
        public IEnumerable<string> Get()
        {
            return new[] { "value1", "value2" };
        }

        // GET: api/Login/5
        public string Get(int id)
        {
            return "value";
        }



        ////http://localhost:56431/api/Login/CreateUser
        //[System.Web.Http.HttpPost]
        //public HttpResponseMessage CreateUser([FromBody] LoginViewModel model)
        //{

        //    if (!ModelState.IsValid)
        //        return Request.CreateResponse(HttpStatusCode.BadRequest);

        //    try
        //    {
        //        MembershipCreateStatus status;

        //        Membership.CreateUser(model.Username, model.Password, model.Username,
        //            "this is my very safe password question", "this is my very safe password answer", true, out status);

        //        return Request.CreateResponse(HttpStatusCode.OK, status.ToString());
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
        //    }
        //}




        //
        // POST: /Account/Register

        // POST: api/Login/CreateUser
        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<HttpResponseMessage> CreateUser([FromBody] LoginViewModel model)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    var user = new ApplicationUser() { UserName = model.Email, Email = model.Email };

                    //   string outputdata = Newtonsoft.Json.JsonConvert.SerializeObject(user);

                    IdentityResult result = await UserManager.CreateAsync(user, model.Password);


                    if (result.Succeeded)
                    {
                        UserManager.AddClaim(user.Id, new Claim(ClaimTypes.GivenName, model.Username));

                        // var service = new SurveryManageService(HttpContext.GetOwinContext().Get<ApplicationDbContext>());
                        //service.CreateCheckingAccount(model.FirstName, model.LastName, user.Id, 0);

                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.Conflict);
                    }


                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Forbidden);
                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }



        }


        // PUT: api/Login/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Login/5
        public void Delete(int id)
        {
        }
    }
}