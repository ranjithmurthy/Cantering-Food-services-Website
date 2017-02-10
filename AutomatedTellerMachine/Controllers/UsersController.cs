using AutomatedTellerMachine.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Runtime.Serialization;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Data.Entity.Validation;

namespace AutomatedTellerMachine.Controllers
{

    public class UsersController : ApiController
    {

        public IEnumerable<ApplicationUser> Get()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var user = db.Users;

                return user.ToList();
            }
        }
        // 24 = 192 bits
        private const int SaltByteSize = 24;
        private const int HashByteSize = 24;
        private const int HasingIterationsCount = 10101;


      


        //http://localhost:56431/api/Users/a/s
        // [Route("UsersController/{email}/{password}")]
        public IHttpActionResult GetStatusLoginCredentials(string email, string password)
        {
            
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var user = db.Users.Where(x => x.UserName == email).ToList();
                
                var onUser = user.FirstOrDefault();

                bool userOk = false, passwordOK = false;

                if (onUser != null)
                {
                    userOk = true;
                    passwordOK = CryptoExtensions.VerifyHashedPassword(onUser.PasswordHash, password);
                }
                
                if (passwordOK && userOk)
                {

                    return Ok(onUser.Email);
                }
                else
                {
                    return NotFound();

                }

            };

            
        }

        //TODO:POSTMETHOD RECHECK 
        // POST: api/users
        public HttpResponseMessage Post([FromBody] RegisterModel userRegister)
        {
            try
            {
                using (ApplicationDbContext context = new ApplicationDbContext())
                {
                    
                    context.Users.Add(new ApplicationUser
                    {
                        UserName = userRegister.Email,
                        PasswordHash = userRegister.PasswordHash,
                        SecurityStamp = userRegister.SecurityStamp,
                        Email = userRegister.Email
                    });
                   
                
                    context.SaveChanges();
                };

                var message = Request.CreateResponse(HttpStatusCode.Created, userRegister);
                message.Headers.Location = new Uri(Request.RequestUri + userRegister.Email + "/" + userRegister.Password);

                return message;


            }
            catch (DbEntityValidationException expe)
            {
                System.Diagnostics.Debug.WriteLine("EXC: ");
                foreach (DbEntityValidationResult result in expe.EntityValidationErrors)
                {
                    foreach (DbValidationError error in result.ValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine(error.ErrorMessage);
                    }
                }

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, expe);

            }
            
            
        }
    }
}
