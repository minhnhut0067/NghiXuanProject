using APIDev.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIDev.Controllers
{
    public class BacsiController : ApiController
    {
        // GET api/bacsi
        public IEnumerable<Data.Bacsi> Get()
        {
            return Data.Bacsi.Getall();
        }

        // GET api/bacsi/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/bacsi
        public IEnumerable<Data.Bacsi> Post([FromBody] Data.DataPost.Bacsi bacsi)
        {
            return Data.Bacsi.Getallbytenbs(bacsi.hoten, bacsi.mabs);
        }

        // PUT api/bacsi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/bacsi/5
        public void Delete(int id)
        {
        }
    }
}
