using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebTinTuc.Models
{
    public class Member
    {
        public Member()
        {
            this.Accounts = new HashSet<Account>();
        }

        [Key]
        public int MemberID { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public Nullable<System.DateTime> Birthday { get; set; }
        public string Gender { get; set; }
        public string IdentityCard { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
        //public virtual CateMember1 CateMember1 { get; set; }
    }
}