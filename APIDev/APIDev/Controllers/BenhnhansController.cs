using APIDev.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIDev.Controllers
{
    public class BenhnhansController : ApiController
    {
        // GET api/values
        public IEnumerable<Data.Benhnhan> Get(string makp)
        {
            return Data.Benhnhan.Get(makp);
        }

        public IEnumerable<Data.Benhnhan> Getmmyy(string s_makp, string s_tungay, string s_denngay,string s_loai, string s_mabn, string s_mabs)
        {
            return Data.Benhnhan.Getmmyy(s_makp, s_tungay, s_denngay, s_loai, s_mabn, s_mabs);
        }

        public IEnumerable<Data.Benhnhan> Post([FromBody] Data.DataPostBenhnhan data)
        {            
            return Data.Benhnhan.Getmmyy(data.s_makp, data.s_tungay, data.s_denngay, data.s_loai,data.s_mabn,data.s_mabs);
        }
    }
}
