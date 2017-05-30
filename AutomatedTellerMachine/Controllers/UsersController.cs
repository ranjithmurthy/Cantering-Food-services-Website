using AutomatedTellerMachine.Models;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;

namespace AutomatedTellerMachine.Controllers
{
    public class UsersController : ApiController
    {
        // 24 = 192 bits
        private const int SaltByteSize = 24;

        private const int HashByteSize = 24;
        private const int HasingIterationsCount = 10101;

        private ApplicationUserManager _userManager;
        private ApplicationDbContext db = new ApplicationDbContext();

        public UsersController()
        {
        }

        public UsersController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get { return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
            private set { _userManager = value; }
        }

        public IEnumerable<ApplicationUser> Get()
        {
            using (var db = new ApplicationDbContext())
            {
                var user = db.Users;

                return user.ToList();
            }
        }

        //
        // POST: api/Users/Validate
        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<HttpResponseMessage> Validate(LoginViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await UserManager.FindAsync(model.Email, model.Password);

                    if (user != null)
                        return Request.CreateResponse(HttpStatusCode.OK, true);
                    ModelState.AddModelError("", "Invalid username or password.");

                    var messages = string.Join("; ", ModelState.Values
                        .SelectMany(x => x.Errors)
                        .Select(x => x.ErrorMessage));

                    return Request.CreateResponse(HttpStatusCode.BadRequest, messages);
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest, "404");
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }
    }
}