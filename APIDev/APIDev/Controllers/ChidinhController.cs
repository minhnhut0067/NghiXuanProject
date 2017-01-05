using APIDev.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIDev.Controllers
{
    public class ChidinhController : ApiController
    {
        // GET api/chidinh
        public IEnumerable<Data.Chidinh> Get()
        {
            //return Data.Chidinh.GetAll();
            return null;
        }     
        // GET api/chidinh/5
        public IEnumerable<Data.Chidinh> Get(Data.DataPost.Chidinh chidinh)
        {
            //if (chidinh.maql != "" && chidinh.ngay != "")
            //{
            //    return new Data.ThongtinChidinh(chidinh);
            //}
            //else
            //{
            //    return null;
            //}
            return null;
        }
        //IEnumerable<JObject> lst = JObject.Parse(filterselect.Datafilter)["Rows"].Values<JObject>().Where(m => m["ma"].Value<string>().ToLower().Contains(filterselect.Value != null ? filterselect.Value.ToLower() : "") || m["ten"].Value<string>().ToLower().Contains(filterselect.Value != null ? filterselect.Value.ToLower() : "")).Take(50);

        // POST api/chidinh
        public Data.ThongtinChidinh Post([FromBody] Data.Chidinh data)
        {
            if (data.id == "" || data.id == null)
            {
                data.id = Data.Chidinh.f_get_id_seq(18);
            }

            if (data.idchidinh == "" || data.idchidinh == null)
            {
                data.idchidinh = data.maql;
            }
            if (data.mmyy == "" || data.mmyy == null)
            {
                data.mmyy = Data.funcmmyy(DateTime.Today.ToString("dd/MM/yyyy")); ;
            }
            return Data.Chidinh.InsertChidinh(data);
        }

        // PUT api/chidinh/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/chidinh/5
        public void Delete(int id)
        {

        }
    }
}
