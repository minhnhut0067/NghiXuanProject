using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebTinTuc.Models;

namespace WebTinTuc.Context
{
    public class WebtintucInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<WebtintucContext>
    {
        protected override void Seed(WebtintucContext context)
        {
            var members = new List<Member>
            {
            new Member{MemberID=1,Email="minhnhut0056@gmail.com",FullName="Alexander",Birthday=DateTime.Parse("2005-09-01"),Gender="Nam",IdentityCard="321395146",Address="",PhoneNumber="0944769721"},
            new Member{MemberID=2,Email="minhnhut0056@gmail.com",FullName="Alexander",Birthday=DateTime.Parse("2005-09-01"),Gender="Nam",IdentityCard="321395146",Address="",PhoneNumber="0944769721"},
            new Member{MemberID=3,Email="minhnhut0056@gmail.com",FullName="Alexander",Birthday=DateTime.Parse("2005-09-01"),Gender="Nam",IdentityCard="321395146",Address="",PhoneNumber="0944769721"}
            };
            members.ForEach(s => context.Members.Add(s));
            context.SaveChanges();

            var courses = new List<Account>
            {
            new Account{Username="admin",Password="e10adc3949ba59abbe56e057f20f883e",MemberID=1,Decendalization="Admin",RegistrationDate=DateTime.Parse("2005-09-01"),Status="Hoạt động"},
            new Account{Username="gm1",Password="e10adc3949ba59abbe56e057f20f883e",MemberID=2,Decendalization="Quản lý chuyên mục",RegistrationDate=DateTime.Parse("2005-09-01"),Status="Hoạt động"},
            new Account{Username="gm2",Password="e10adc3949ba59abbe56e057f20f883e",MemberID=3,Decendalization="Viết bài",RegistrationDate=DateTime.Parse("2005-09-01"),Status="Hoạt động"}
            };
            courses.ForEach(s => context.Accounts.Add(s));
            context.SaveChanges();
        }
    }
}