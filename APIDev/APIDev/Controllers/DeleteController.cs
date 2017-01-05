using APIDev.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIDev.Controllers
{
    public class DeleteController : ApiController
    {
        // GET api/delete
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/delete/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/delete
        public bool Post([FromBody] Data.DataPost.Delete data)
        {
            return Data.Delete(data);
        }

        // PUT api/delete/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/delete/5
        public void Delete(int id)
        {
        }
    }
}
