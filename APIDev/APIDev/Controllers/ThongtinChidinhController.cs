using APIDev.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIDev.Controllers
{
    public class ThongtinChidinhController : ApiController
    {
        // GET api/thongtinchidinh
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/thongtinchidinh/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/thongtinchidinh
        public Data.ThongtinChidinh Post([FromBody]Data.DataPost.Chidinh chidinh)
        {
            return new Data.ThongtinChidinh(chidinh);
        }

        // PUT api/thongtinchidinh/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/thongtinchidinh/5
        public void Delete(int id)
        {
        }
    }
}
