//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Sanpham
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    
    public partial class Account
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int MemberID { get; set; }
        public string Decendalization { get; set; }
        public Nullable<System.DateTime> RegistrationDate { get; set; }
        public string Status { get; set; }

        [ForeignKey("MemberID")]
        public virtual Member Member { get; set; }
    }
}