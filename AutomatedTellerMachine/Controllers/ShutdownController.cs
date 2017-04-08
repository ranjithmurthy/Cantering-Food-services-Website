using System.Collections.Generic;
using System.Web.Http;

namespace AutomatedTellerMachine.Controllers
{
    public class ShutdownController : ApiController
    {
        // GET: api/Shutdown
        public IEnumerable<string> Get()
        {
            return new[] {"value1", "value2"};
        }

        // GET: api/Shutdown/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Shutdown
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Shutdown/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Shutdown/5
        public void Delete(int id)
        {
        }
    }
}