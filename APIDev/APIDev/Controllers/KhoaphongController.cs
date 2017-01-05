using APIDev.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIDev.Controllers
{
    public class KhoaphongController : ApiController
    {
        // GET api/khoaphong
        public IEnumerable<Data.Khoaphong> Get()
        {
            return Data.Khoaphong.Getall();
        }

        // GET api/khoaphong/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/khoaphong
        public IEnumerable<Data.Khoaphong> Post([FromBody] Data.DataPost.Khoaphong khoaphong)
        {
            return Data.Khoaphong.Getallbytenkp(khoaphong.makp, khoaphong.ten,khoaphong.search);
        }

        // PUT api/khoaphong/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/khoaphong/5
        public void Delete(int id)
        {
        }
    }
}
