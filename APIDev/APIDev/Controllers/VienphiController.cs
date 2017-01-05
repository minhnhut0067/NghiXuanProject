using APIDev.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIDev.Controllers
{
    public class VienphiController : ApiController
    {
        // GET api/vienphi
        public IEnumerable<Data.Vienphi> Get()
        {
            return Data.Vienphi.Getall("");
        }

        // GET api/vienphi/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/vienphi
        public IEnumerable<Data.Vienphi> Post([FromBody] Data.DataPostVienphi vienphi)
        {
            return Data.Vienphi.Getall(vienphi.noidung);
        }

        // PUT api/vienphi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/vienphi/5
        public void Delete(int id)
        {
        }
    }
}
