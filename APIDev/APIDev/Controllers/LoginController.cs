using APIDev.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace APIDev.Controllers
{
    public class LoginController : ApiController
    {
        // GET api/values        
        public IEnumerable<Data.Login> Get()
        {            
            return Data.Login.Get();
        }
    }
}
