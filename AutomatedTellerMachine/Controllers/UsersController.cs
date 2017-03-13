using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Security;
using AutomatedTellerMachine.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace AutomatedTellerMachine.Controllers
{
    public class UsersController : ApiController
    {
        // 24 = 192 bits
        private const int SaltByteSize = 24;

        private const int HashByteSize = 24;
        private const int HasingIterationsCount = 10101;
        private  ApplicationDbContext db = new ApplicationDbContext();


        private ApplicationUserManager _userManager;

        public UsersController()
        {
        }

        public UsersController(ApplicationUserManager userManager)
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
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, true);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid username or password.");

                        string messages = string.Join("; ", ModelState.Values
                                            .SelectMany(x => x.Errors)
                                            .Select(x => x.ErrorMessage));


                        return Request.CreateResponse(HttpStatusCode.BadRequest, messages);
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "404");
                }

            }
            catch (Exception e)
            {
            
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
                
            }


        }

        //TODO:POSTMETHOD RECHECK
        // POST: api/users
        //public HttpResponseMessage Post([FromBody] RegisterModel userRegister)
        //{
        //    try
        //    {
        //        using (var context = new ApplicationDbContext())
        //        {
        //            context.Users.Add(new ApplicationUser
        //            {
        //                UserName = userRegister.Email,
        //                PasswordHash = userRegister.PasswordHash,
        //                SecurityStamp = userRegister.SecurityStamp,
        //                Email = userRegister.Email
        //            });

        //            context.SaveChanges();
        //        }
        //        ;

        //        var message = Request.CreateResponse(HttpStatusCode.Created, userRegister);
        //        message.Headers.Location = new Uri(Request.RequestUri + userRegister.Email + "/" + userRegister.Password);

        //        return message;
        //    }
        //    catch (DbEntityValidationException expe)
        //    {
        //        Debug.WriteLine("EXC: ");
        //        foreach (var result in expe.EntityValidationErrors)
        //            foreach (var error in result.ValidationErrors)
        //                Debug.WriteLine(error.ErrorMessage);

        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, expe);
        //    }
        //}
    }
}