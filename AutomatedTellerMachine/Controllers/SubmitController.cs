using System.Collections.Generic;
using System.Web.Http;
using AutomatedTellerMachine.Models;

namespace AutomatedTellerMachine.Controllers
{
    public class SubmitController : ApiController
    {
        // GET: api/Submit
        public IEnumerable<string> Get()
        {
            return new[] {"value1", "value2"};
        }

        // GET: api/Submit/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Submit
        public void Post([FromBody] FeedbackViewModel userFeedback)
        {
        }

        // PUT: api/Submit/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Submit/5
        public void Delete(int id)
        {
        }
    }
}