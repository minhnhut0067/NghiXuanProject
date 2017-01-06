using WebTinTuc.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace WebTinTuc.Context
{
    public class WebtintucContext : DbContext
    {
        public WebtintucContext()
            : base("WebtintucContext")
        {
        }
        
        public DbSet<Member> Members { get; set; }
        public DbSet<Account> Accounts { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}