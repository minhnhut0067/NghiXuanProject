using APIDev.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIDev.Controllers
{
    public class ChandoanhinhanhController : ApiController
    {
        // GET api/chandoanhinhanh
        //public string Get()
        //{
        //return Data.Chandoanhinhanh.f_Load_Image_Server();
        //}

        // GET api/chandoanhinhanh/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/chandoanhinhanh
        public IEnumerable<Data.Chandoanhinhanh> Post([FromBody] Data.DataPost.Chandoanhinhanh cdha)
        {
            if (cdha.check == "chitiet")
            {
                return Data.Chandoanhinhanh.Getvalue(cdha);
            }
            else
            {
                return null;
            }
        }

        // PUT api/chandoanhinhanh/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/chandoanhinhanh/5
        public void Delete(int id)
        {
        }
    }
}
