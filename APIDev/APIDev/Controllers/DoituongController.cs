using APIDev.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIDev.Controllers
{
    public class DoituongController : ApiController
    {
        // GET api/doituong
        public IEnumerable<Data.Doituong> Get()
        {
            return Data.Doituong.Getall();
        }

        // GET api/doituong/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/doituong
        public void Post([FromBody]string value)
        {
        }

        // PUT api/doituong/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/doituong/5
        public void Delete(int id)
        {
        }
    }
}
