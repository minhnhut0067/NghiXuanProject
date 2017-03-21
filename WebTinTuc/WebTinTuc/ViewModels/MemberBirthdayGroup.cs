using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebTinTuc.ViewModels
{
    public class MemberBirthdayGroup
    {
        [DataType(DataType.Date)]
        public DateTime? MemberBirthday { get; set; }

        public int MemberCount { get; set; }
    }
}