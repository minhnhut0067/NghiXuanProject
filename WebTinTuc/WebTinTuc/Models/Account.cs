using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebTinTuc.Models
{
    public class Account
    {
        [Key]
        public int AccountID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int MemberID { get; set; }
        public string Decendalization { get; set; }
        public Nullable<System.DateTime> RegistrationDate { get; set; }
        public string Status { get; set; }

        public virtual Member Member { get; set; }
    }
}