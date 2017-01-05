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
    public class XetNghiemController : ApiController
    {
        // GET api/xetnghiem
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/xetnghiem/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/xetnghiem
        public DataTable Post([FromBody]Data.XetNghiem xetnghiem)
        {
            if (xetnghiem.check == "xetnghiem")
            {
                return Data.XetNghiem.Getvalue(xetnghiem);
            }
            else
            {
                return Data.XetNghiem.Getdsxetnghiem(xetnghiem);
            }
        }

        // PUT api/xetnghiem/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/xetnghiem/5
        public void Delete(int id)
        {
        }
    }
}
